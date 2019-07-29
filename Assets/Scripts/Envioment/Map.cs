using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public GameObject square = null;
    public float slope = 45;
    
    public enum Direction { LEFT, RIGHT }

    void Start()
    {
        SpawnStep(slope);
    }

    public GameObject SpawnStep(float angleSlope)
    {
        var obj = Instantiate(square, transform);
        obj.transform.localEulerAngles = Vector3.forward * angleSlope;
        return obj;
    }
}
