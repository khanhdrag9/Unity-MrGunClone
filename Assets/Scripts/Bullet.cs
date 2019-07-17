using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsEnd = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.ENEMY_TAG)
        {
            collision.gameObject.GetComponent<Enemy>().isDied = true;
        }
        IsEnd = true;
    }
}
