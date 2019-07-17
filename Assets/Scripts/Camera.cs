using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject target = null;

    Vector2 distanceToTarget = Vector2.zero;
    void Start()
    {
        distanceToTarget = target.transform.position - transform.position;
        distanceToTarget.x = 0;
        Debug.Log("distance : " + distanceToTarget);
    }

    void Update()
    {
        Vector2 pos = target.transform.position;
        Vector3 newpos = new Vector3(transform.position.x, pos.y - distanceToTarget.y, -10);
        transform.position = newpos;
    }
}
