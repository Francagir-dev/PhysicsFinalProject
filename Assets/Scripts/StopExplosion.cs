using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StopExplosion : MonoBehaviour
{
    private float actualTime= 0f;

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime>=3f)
        {
            transform.GetChild(0).GetComponent<VisualEffect>().SendEvent("End");
        }
    }
}
