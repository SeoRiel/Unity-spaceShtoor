using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{

    private int hitCount = 0;                  // 총알 피격 횟수 측정
    private Rigidbody barrelRigidbody;    // Rigidbody 컴포넌트를 추출해 저장
    private MeshFilter meshFilter;           // MeshFilter Component를 저장 변수
    private MeshRenderer mRenderer;     // MeshRenderer Component 저장 변수
    private AudioSource barrelAudio;      // AudioSource Component 저장 변수

    public GameObject expEffect;           // 폭발 효과 프리팹을 저장할 변수
    public Mesh[] meshList;                   // 찌그러진 드럼통 메쉬 저장
    public Texture[] textures;                 // 드럼통의 텍스쳐 저장 배열
    public float expRadius = 10.0f;         // 폭발 반경 저장 변수
    public AudioClip barrelSFX;             // 폭발음 Audio Clip

    // Start is called before the first frame update
    void Start()
    {
        barrelRigidbody = GetComponent<Rigidbody>();                                                // Rigidbody Component 추출 후, 저장
        meshFilter = GetComponent<MeshFilter>();                                                       // MeshFilter Component 추출 후, 저장
        mRenderer = GetComponent<MeshRenderer>();                                                 // MeshRenderer Component 추출 후, 저장
        barrelAudio = GetComponent<AudioSource>();                                                  // AudioSource Component 추출 후, 저장
        mRenderer.material.mainTexture = textures[Random.Range(0, textures.Length)];         // 난수를 이용한 불규칙적인 텍스쳐 출력
    }

    // 충돌 (다른 Collider와 접촉) 발생 시, 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 게임 오브젝트가 BULLET일 때
        if(collision.collider.CompareTag("BULLET"))
        {
            if(++hitCount == 2)
            {
                ExpBarrel();
            }
        }
    }

    // 폭발 효과 처리 함수
   private void ExpBarrel()
    {
        // 폭발 효과 프리팹 동적 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);


        Instantiate(expEffect, transform.position, Quaternion.identity);        // 폭발 효과 프리팹을 동적으로 생성

        barrelRigidbody.mass = 1.0f;                                                 // Rigidbody Component의 mass를 1.0을 수정해 무게 감소

        // barrelRigidbody.AddForce(Vector3.up * 1000.0f);                    // Y축 이동

        IndirectDamage(transform.position);                                       // 폭발력 생성

        int Index = Random.Range(0, meshList.Length);                        // 난수 생성

        meshFilter.sharedMesh = meshList[Index];                              // 폭발 메쉬 변환
        barrelAudio.PlayOneShot(barrelSFX, 1.0f);                               // 폭발음 실행

    }


    // 폭발력 전달 함수
    private void IndirectDamage(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, expRadius, 1 << 8);        // 근처에 있는 드럼통 탐색

        foreach (var coll in colliders)
        {
            var barrelsRB = coll.GetComponent<Rigidbody>();                           // 폭발 범위에 포함된 드럼통의 RigidBody Component 추출
            barrelsRB.mass = 1.0f;                                                                // 드럼통의 무게 감소
            barrelsRB.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);           // 폭발력 전달
        }
    }
}
