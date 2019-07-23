using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public ParticleSystem effect;

    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collisiom At Par : " + other.name);
        CheckCollision(other);
    }

    void CheckCollision(GameObject other)
    {
        switch(other.tag)
        {
            case Constants.BULLET_DELETER_TAG:
                //Debug.Log("Bullet delete");
                //Destroy(gameObject);
                GameQuick.logic.CheckShoot();
                break;
            case Constants.ENEMY_TAG:
                other.GetComponent<Enemy>().isDied = true;
                GameQuick.logic.CheckShoot(0.25f);
                break;
            default:
                GameQuick.logic.CheckShoot();
                break;
        }
            
    }
}
