using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 충돌이 시작할 때 발생하는 이벤트
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 게임 오브젝트의 태그값 비교
        if(collision.collider.tag == "BULLET")
        {
            // 조건에 맞는 태그를 가지고 있는 오브젝트 소거 처리
            Destroy(collision.gameObject);
        }
    }
}
