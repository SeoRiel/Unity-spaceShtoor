using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{

    public GameObject sparkEffect;                         // 스파크 프리팹을 저장할 변수


    private void OnCollisionEnter(Collision collision)    // 충돌이 시작할 때 발생하는 이벤트
    {
        if(collision.collider.tag == "BULLET")                // 충돌한 게임 오브젝트의 태그값 비교
        {
            ShowEffect(collision);

            Destroy(collision.gameObject);                   // 조건에 맞는 태그를 가지고 있는 오브젝트 소거 처리
        }
    }


    private void ShowEffect(Collision collision)                                                                    // 피격 위치에 이펙트 출력
    {
        ContactPoint contact = collision.contacts[0];                                                              // 충돌 위치 저장

        Quaternion extractRotate = Quaternion.FromToRotation(-Vector3.forward, contact.normal);  // 법선 벡터가 이루는 회전 각도 추출

        Instantiate(sparkEffect, contact.point, extractRotate);                                                     // 스파크 이펙트 생성
    }
}
