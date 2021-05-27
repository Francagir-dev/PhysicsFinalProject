using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("UI ELEMENTS")] public Text time;
    public Text windInfo;
    public Text points_txt;
    public static Slider force, angle;

    public static int points = 0;
    public static int numberEnemiesAchieved = 0;
    [SerializeField] private bool goUp;
    public static bool isDestroyed;
    public static float _windGammaAngle = 0f;
    public static float _cd = 1.2f;
    public static float _cw = 1.3f;
    public static float _vw = 3f;
    [SerializeField] private float actualTime = 10f;

    private void Awake()
    {
        //TXT
        points_txt = GameObject.Find(Constants.GO_TXT_POINTS).GetComponent<Text>();
        time = GameObject.Find(Constants.GO_TXT_TIMER).GetComponent<Text>();
        //SLIDERS
        angle = GameObject.Find(Constants.GO_SLIDER_ANGLE).GetComponent<Slider>();
        force = GameObject.Find(Constants.GO_SLIDER_FORCE).GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        setTextTimer();
        MoveSlider();
        ChangeTextWind();
    }

    void setTextTimer()
    {
        float minutes = Mathf.Floor(actualTime / 60);
        float seconds = Mathf.RoundToInt(actualTime % 60);

        if (seconds < 10)
        {
            seconds = Mathf.RoundToInt(seconds);
        }

        time.text = minutes + ": " + seconds;
    }

    void MoveSlider()
    {
        if (goUp)
        {
            force.value += Time.deltaTime * 10f;
            if (force.value >= force.maxValue)
                goUp = false;
        }
        else
        {
            force.value -= Time.deltaTime * 10f;
            if (force.value <= force.minValue)
                goUp = true;
        }
    }

    public void ChangeTextWind()
    {
        windInfo.text = "Wind Velocity: " + _vw.ToString("F4")
                                          + "\nWind Angle: " + _windGammaAngle.ToString("F4")
                                          + "\nConstant of Air Resistance: " + _cd.ToString("F4")
                                          + "\nConsant of Air Force " + _cw.ToString("F4");
    }

    public static void ChangeWind()
    {
        _vw = Random.Range(1f, 10f);
        _windGammaAngle = Random.Range(35f, 70f);
        _cd = Random.Range(1f, 1.5f);
        _cw = Random.Range(1f, 1.5f);
    }
}