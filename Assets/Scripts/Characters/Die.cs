using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public ParticleSystem effect;
    public Animator aniDie;
    public bool useParticle = false;
    public Transform destroyPoint;
    public void PlayEffect()
    {
        if(useParticle)
        {
            var e = Instantiate(effect);
            GameQuick.particleMgr.Add(e);
            e.transform.position = destroyPoint.position;
            effect.Play(true);
        }
        else
        {
            var e = Instantiate(aniDie);
            e.GetComponent<DestroyAnimation>().node = gameObject;
            e.transform.position = destroyPoint.position;
        }
        
    }
}
