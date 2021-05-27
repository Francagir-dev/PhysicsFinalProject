using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;
using Debug = UnityEngine.Debug;

public class CountEnemiesAchieved : MonoBehaviour
{
    public GameObject fireCastle;

  

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            Destroy(other.gameObject);
            GameManager.numberEnemiesAchieved += 1;

            fireCastle.GetComponent<VisualEffect>().SetFloat("Wind_Angle", GameManager._windGammaAngle);

            fireCastle.GetComponent<VisualEffect>().SetFloat("Wind_Force", GameManager._vw);
            switch (GameManager.numberEnemiesAchieved)
            {
                case 20:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 100);
                    break;
                case 40:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 200);
                    break;
                case 60:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 400);
                    break;
                case 80:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 600);
                    break;
                case 100:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 800);
                    break;
                case 120:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 1600);
                    break;
                case 140:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 3200);
                    break;
                case 160:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 6400);
                    break;
                case 180:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 12800);
                    break;
                case 200:
                    fireCastle.GetComponent<VisualEffect>().SetFloat("ConstantSpawn", 25600);
                    Application.Quit(3);
                    break;
            }
        }
    }
}