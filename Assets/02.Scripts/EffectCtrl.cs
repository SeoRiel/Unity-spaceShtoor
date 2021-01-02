using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    [SerializeField]
    private float effectTime = 1.0f;

   private void Update()
    {
        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().duration + effectTime);
    }
}
