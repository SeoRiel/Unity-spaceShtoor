using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{

    public Color selectColor = Color.yellow;
    public float radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = selectColor;
        Gizmos.DrawSphere(transform.position, radius);

    }
}
