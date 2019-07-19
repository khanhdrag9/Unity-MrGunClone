using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == Constants.ENEMY_TAG)
        {
            collision.gameObject.GetComponent<Enemy>().isDied = true;
            GameQuick.logic.CheckShoot(0.25f);
        }
        else
            GameQuick.logic.CheckShoot();
    }
}
