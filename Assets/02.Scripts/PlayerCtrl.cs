using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnime
{
    // Create is Animation Clip
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
}

// [SerializeField] : private 설정 상태의 변수를 Inspector에 노출시켜주는 명령어
// [HideInInspector] : public 설정 상태의 변수를 Inspector에 표시하지 않는 명령어

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    private Transform tr;                                                        // 접근해야 하는 컴포넌트는 반드시 변수에 할당한 후 사용
    // [SerializeField] private Transform tr; -> [SerializeField]를 이용하여 private 선언된 변수 및 함수에 접근 가능

    public float moveSpeed = 10.0f;                                         // 이동 속도 변수(public으로 선언되어 Inspector에 표시됨)
    public float rotSpeed = 80.0f;


    // 인스펙터 뷰에 표시할 애니메이션 클래스 변수
    [SerializeField]
    public PlayerAnime playerAnime;

    // This parameter is animation component for save
    public Animation anime;

    // Start is called before the first frame update
    void Start()
    {
        // 스크립트 처음 실행 후, Start 함수에서 Transform 컴포넌트 할당
        tr = GetComponent<Transform>();
        // tr = GetComponent("Transform") as Transform;
        // tr = (Transform)GetComponent(typeof(Transform));
        // -> tr = this.gameObject.GetComponent<Transform>();

        // Animation Component를 변수에 할당
        anime = GetComponent<Animation>();

        // Animation Component의 Animation Clip을 지정하고 실행
        anime.clip = playerAnime.idle;
        anime.Play();

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Debug.Log("h = " + h.ToString());
        Debug.Log("v = " + v.ToString());

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Tanslate(이동 방향 * 속도 * 변위값 * Time.deltaTime, 기준좌표)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        // 키보드 입력값을 기준으로 동작할 애니메이션 수행
        if( v >= 0.1f )
        {
            // 전진 애니메이션
            anime.CrossFade(playerAnime.runF.name, 0.3f);
        }
        else if( v <= -0.1f )
        {
            // 후진 애니메이션
            anime.CrossFade(playerAnime.runB.name, 0.3f);
        }
        else if( h >= 0.1f )
        {
            // 오른쪽 이동 애니메이션
            anime.CrossFade(playerAnime.runR.name, 0.3f);
        }
        else if( h <= -0.1f )
        {
            // 왼쪽 이동 애니메이션
            anime.CrossFade(playerAnime.runL.name, 0.3f);
        }
        else
        {
            // 정지 시, idle 애니메이션
            anime.CrossFade(playerAnime.idle.name, 0.3f);
        }
    }
}
