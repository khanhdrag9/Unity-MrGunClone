using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet profile = null;
    public GameObject charge = null;
    public ParticleSystem chargeParticle = null;
    public GameObject shootPoint = null;
    public GameObject aim;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public Bullet SpawnProfile()
    {
        return Instantiate(profile, transform);
    }

    public void Charge(bool isEnable)
    {
        charge.SetActive(isEnable);
        if(chargeParticle)
        {
            if(isEnable)chargeParticle.Play();
            else chargeParticle.Stop();
        }
    }

    public void Aim(bool isEnable)
    {
        if(aim)aim.SetActive(isEnable);
    }
}
