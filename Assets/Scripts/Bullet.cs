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

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case Constants.BULLET_DELETER_TAG:
                //Debug.Log("Bullet delete");
                //Destroy(gameObject);
                GameQuick.logic.CheckShoot();
                break;
            case Constants.ENEMY_TAG:
                collision.gameObject.GetComponent<Enemy>().isDied = true;
                GameQuick.logic.CheckShoot(0.25f);
                break;
            default:
                GameQuick.logic.CheckShoot();
                break;
        }
            
    }
}
