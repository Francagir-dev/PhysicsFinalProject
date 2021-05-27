using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCanons : MonoBehaviour
{
    [SerializeField] private GameObject cannonToRotate;
    [SerializeField] private Slider slider;
    private float prevValue;

    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
        prevValue = slider.value;
    }

    void OnSliderChanged(float value)
    {
        float delta = value - prevValue;
        cannonToRotate.transform.Rotate(Vector3.up*delta);
        prevValue = value;
    }

   
}