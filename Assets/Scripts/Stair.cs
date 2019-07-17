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
    public void SetColor(Color stair, Color wall, float duration)
    {
        foreach (var s in stairList)
        {
            //s.GetComponent<SpriteRenderer>().color = stair;
            s.GetComponent<Wall>().ColorTo(stair, duration);
        }
        this.wall.GetComponent<Wall>().ColorTo(wall, duration);
    }

}
