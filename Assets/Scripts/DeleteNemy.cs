using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constants.TAG_ENEMY))
        {
            GameManager.points += 10;
            Destroy(other.gameObject);
        }
    }
}
