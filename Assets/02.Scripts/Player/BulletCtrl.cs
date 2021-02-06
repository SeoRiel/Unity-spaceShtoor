using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;
    public float speed = 1000.0f;


    private void Start()    // Start is called before the first frame update
    {
        //AddForce(transform -> 이동 방향 * 증가 변수);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    private void Update()    // Update is called once per frame
    {
        
    }
}
