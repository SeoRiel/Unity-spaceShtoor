using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 벡터 크기와 정규화
// 벡터의 크기(Magnitude)는 Vector3.Magnitude 함수를 이용해 가져올 수 있다.

public class VectorMagnitude : MonoBehaviour
{

   private void Start()    // Start is called before the first frame update
    {
        float vec1 = Vector3.Magnitude(Vector3.forward);
        float vec2 = Vector3.Magnitude(Vector3.forward + Vector3.right);
        float vec3 = Vector3.Magnitude((Vector3.forward + Vector3.right).normalized);

        Debug.Log(vec1);
        Debug.Log(vec2);
        Debug.Log(vec3);
    }
}
