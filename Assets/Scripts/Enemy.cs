using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speedX = 0;
    [SerializeField] Logic logic = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.BULLET_TAG)
        {
            logic.NextEnemy();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
