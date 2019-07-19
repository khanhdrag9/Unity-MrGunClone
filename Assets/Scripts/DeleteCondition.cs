using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCondition : MonoBehaviour
{
    [SerializeField] Range x = null;
    void Update()
    {
        if (transform.position.x < x.min || transform.position.x > x.max)
            Destroy(gameObject);
    }
}
