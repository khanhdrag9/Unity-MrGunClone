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
            Debug.Log("CallDestroy");
            wasInvoke = true;
            Invoke("FunToDestroy", delayDestroy);
        }
    }

    void FunToDestroy()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        DestroyEffect();
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
