using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.VFX;

public class BallActions : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosion;
    [SerializeField] private VisualEffect explosionVFX;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_GROUND))
        {
            if (explosionPrefab != null)
            {
                explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.transform.GetChild(0).GetComponent<VisualEffect>().SendEvent("Start");
                Destroy(gameObject, 3f);
                ChangeCannonBallVariables();
                GameManager.ChangeWind();
            }
        }

        if (other.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            if (explosionPrefab != null)
            {
                explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.transform.GetChild(0).GetComponent<VisualEffect>().SendEvent("Start");
                Destroy(gameObject, 3f);
                ChangeCannonBallVariables();
                GameManager.ChangeWind();
                
            }
        }

        if (other.gameObject.CompareTag("VFX"))
        {
            Debug.Log("explosion");
        }

        Debug.Log(other.gameObject.name);
    }

    void ChangeCannonBallVariables()
    {
        LanzamientoBola.ball = null;
        LanzamientoBola.CanChangeValuesToBall = true;
    }
}