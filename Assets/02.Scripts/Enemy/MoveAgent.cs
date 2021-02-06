using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;                                                           // 네비게이션 기능 호출에 필요한 네임스페이스 호출

[RequireComponent(typeof(NavMeshAgent))]                            // Prefabs에 해당 스크립트 적용 시, NavMeshAgent Component 자동 추가
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;                                       // 순찰 지점들을 지정하기 위한 List Type Variable
    public int nextIndex;                                                         // 다음 순찰 지점 배열의 Index

    private NavMeshAgent agent;                                             // NavMeshAgent Component를 저장할 변수

    private void Start()                                                            // Start is called before the first frame update
    {
        agent = GetComponent<NavMeshAgent>();                       // NavMeshAgent Component 추출 후, 저장
        agent.autoBraking = false;                                              // 목적지에 가까울수록 속도 감소 옵션 비활성


        var group = GameObject.Find("WayPointGroup");                 // Hierarchy view의 WayPointGroup GameObejct 추출
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);  // WayPointGroup 하위에 있는 모든 Transform Component 추출 후, List Type의 WayPoints Array에 추가

            wayPoints.RemoveAt(0);                                              // Array의 첫번째 항목 삭제
        }

    }

    private void MoveWayPoint()                                               // 다음 목적지까지 이동을 진행하는 함수
    {
        if(agent.isPathStale)                                                       // 최단거리 경로 계산이 끝나지 않으면 아래 코드 실행 취소
                                                                                        // isPathStale == true -> 더 이상 이동할 경로가 없음
        {
            return;
        }

        agent.destination = wayPoints[nextIndex].position;               // wayPoints Array에서 추출한 위치를 다음 목적지로 지정
        agent.isStopped = false;                                                 // Navgation 기능 활성화 후, 이동 진행

    }


    private void Update()                                                         // Update is called once per frame
    {
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f 
            && agent.remainingDistance <= 0.5f)                            // NavMeshAgent 이동 및 목적지 도착 여부 계산 진행
        {
            nextIndex = ++nextIndex % wayPoints.Count;                  // 다음 목적지의 배열 첨자 계산
            MoveWayPoint();                                                       // 다음 목적지 이동 진행
        }
    }
}