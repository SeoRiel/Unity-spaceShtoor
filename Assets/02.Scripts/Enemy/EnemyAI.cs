using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum STATE    // 적 캐릭터의 상태를 표현하기 위한 열거형 변수
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public STATE state = STATE.PATROL;    // 상태를 저장할 변수
    public float attackDistance = 5.0f;        // 공격 사정거리
    public float traceDistance = 10.0f;        // 추적 사정거리

    public bool isDie = false;                    // 사망 여부를 저장할 변수

    private Transform playerTransform;       // 주인공의 위치를 저장할 변수
    private Transform enemyTransform;      // 적 캐릭터의 위치를 저장할 변수
    private WaitForSeconds wfs;                // 코루틴에서 사용할 지연시간 변수


    private void Awake()                                                                 // 생성 시, 즉시 시작
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");        // Player GameObject 추출


        if (player != null)                                                                  // Player Transform Component 추출
        {
            playerTransform = player.GetComponent<Transform>();
        }

        enemyTransform = GetComponent<Transform>();                        // Enemy Charactor Transform Component 추출

        wfs = new WaitForSeconds(0.3f);                                              // 코루틴 지연시간 생성
    }

    private void OnEnable()                                                              // 해당 스크립트 활성 시, 해당 함수 호출 진행
    {
        StartCoroutine(CheckState());                                                   // CheckState 코루틴 함수 실행
    }

    IEnumerator CheckState()                                                            // 적 캐릭터의 상태를 검사하는 코루틴 메서드
    {

        while(!isDie)                                                                                                   // 적 캐릭터가 사망하기 전까지 작동하는 무한 루프
        {
            if(state == STATE.DIE)                                                                                  // 사망 상태일 때, 코루틴 함수 종료
            {
                yield break;
            }

            float distance = Vector3.Distance(playerTransform.position, enemyTransform.position); // 플레이어와 적 캐릭터 사이의 거리 계산
            //  float distancet = (playerTransform.position - enemyTransform.position).sqrMagnitude; 

            if (distance <= attackDistance)                                                                         // 사정거리 이내인 경우
            {
                state = STATE.ATTACK;
            }
            else if(distance <= traceDistance)                                                                      // 추적 거리 이내인 경우
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.PATROL;
            }

            yield return wfs;                                                                                             // 대기 제어권을 0.3초동안 양보
        }
    }


}
