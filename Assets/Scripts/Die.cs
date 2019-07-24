using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public ParticleSystem effect;
    public Transform destroyPoint;
    public void PlayEffect()
    {
        var e = Instantiate(effect);
        GameQuick.particleMgr.Add(e);
        e.transform.position = destroyPoint.position;
        effect.Play(true);
    }
}
