using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject followTarget = null;
    Vector3 distance;
    void Start()
    {
        distance = followTarget.transform.position - transform.position;
    }

    void Update()
    {
        transform.position = followTarget.transform.position - distance;
    }
}
