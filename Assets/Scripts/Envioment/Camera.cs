using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    [SerializeField] float speed = 5f;

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
        if(pos.y - distanceToTarget.y > transform.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if(Convert.ToInt32(transform.position.y) != Convert.ToInt32(pos.y - distanceToTarget.y))
        {
            Vector3 newpos = new Vector3(transform.position.x, pos.y - distanceToTarget.y, -10);
            transform.position = newpos;
        }
        
    }
}
