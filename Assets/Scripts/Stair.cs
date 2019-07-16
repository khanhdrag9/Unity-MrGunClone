using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair
{
    public List<GameObject> stairList = null;
    public GameObject wall = null;
    public Stair()
    {
        stairList = new List<GameObject>();
    }
    public void SetEnableColliderStair(bool value)
    {
        foreach (var stair in stairList)
        {
            stair.GetComponent<BoxCollider2D>().enabled = value;
        }
    }
    public void SetEnableColliderWall(bool value)
    {
        wall.GetComponent<BoxCollider2D>().enabled = value;
    }
}
