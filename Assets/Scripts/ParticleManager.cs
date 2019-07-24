using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public float delayClean = 2f;
    List<ParticleSystem> list;

    void Awake()
    {
        GameQuick.particleMgr = this;
    }

    void Start()
    {
        list = new List<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var o in list)
        {
            if (o && o.particleCount == 0) Destroy(o.gameObject, delayClean);
        }
        list.RemoveAll(o => o == null);
    }

    public void Add(ParticleSystem particle) => list.Add(particle);

    public void Spawn(ParticleSystem particle, Vector3 position)
    {
        var p = Instantiate(particle);
        p.transform.position = position;
    }
}
