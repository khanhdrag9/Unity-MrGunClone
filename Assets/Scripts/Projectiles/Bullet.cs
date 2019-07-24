using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public ParticleSystem effect;
    public ParticleSystem destroyEffect;
    public float delayDestroy = 0.5f;

    public bool isCheck = false;
    bool wasInvoke = false;


    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //transform.Translate(velocity * Time.deltaTime);
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
        if (isCheck) return;
        switch (other.tag)
        {
            case Constants.BULLET_DELETER_TAG:
                GameQuick.logic.CheckShoot();
                break;
            case Constants.ENEMY_TAG:
                other.GetComponent<Enemy>().isDied = true;
                other.GetComponent<Die>().PlayEffect();
                Rigidbody2D bulletBody = GetComponent<Rigidbody2D>();
                Vector2 curVec = GetComponent<Rigidbody2D>().velocity;
                // other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                // other.GetComponent<Rigidbody2D>().AddForce(new Vector2(curVec.x / Mathf.Abs(curVec.x) * 20, 1000));
                other.GetComponent<Rigidbody2D>().AddForce(curVec * 30);
                // other.GetComponent<BoxCollider2D>().enabled = false;
                GameQuick.logic.CheckShoot(0.25f);
                CallDestroy();
                break;
            default:
                GameQuick.logic.CheckShoot();
                CallDestroy();
                break;

        }
    }

    void CallDestroy()
    {
        if (!wasInvoke)
        {
            wasInvoke = true;
            Invoke("FunToDestroy", delayDestroy);
        }
    }

    void FunToDestroy()
    {
        DestroyEffect();
        Destroy(gameObject);
    }

    void DestroyEffect()
    {
        if(destroyEffect)
        {
            var effect = Instantiate(destroyEffect);
            GameQuick.particleMgr.Add(effect);
            effect.transform.position = transform.position;
        }
    }
}
