using System;
using UnityEngine;

[Serializable]
public class Range
{
    [SerializeField]public float min;
    
    [SerializeField]public float max;
    
    private float minOrigin = 0;
    private float maxOrigin = 0;

    public Range()
    {
        min = 0;
        max = 0;
    }
    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;

        minOrigin = this.min;
        maxOrigin = this.max;
    }
    public int GetRandomAsInt()
    {
        return UnityEngine.Random.Range(Convert.ToInt32(min), Convert.ToInt32(max));
    }
    public float GetRandomAsFloat()
    {
        return UnityEngine.Random.Range(min, max);
    }
    public void Reset()
    {
        this.min = minOrigin;
        this.max = maxOrigin;
    }
    public void ResetWithNew(float min, float max)
    {
        minOrigin = min;
        maxOrigin = max;
        Reset();
    }
    public void SynWithCurent()
    {
        minOrigin = min;
        maxOrigin = max;
    }
}
