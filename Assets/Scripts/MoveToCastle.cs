using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCastle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject conquestPlace;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        conquestPlace = GameObject.Find(Constants.GO_ENTRANCE_CASTLE);
        if(conquestPlace==null)
            conquestPlace = GameObject.FindWithTag(Constants.TAG_ENTRANCE);
       
        target = conquestPlace.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (conquestPlace != null)
            transform.position =
                Vector3.MoveTowards(transform.position, conquestPlace.transform.position, step);
    }
}