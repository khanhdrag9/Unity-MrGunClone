using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public ScrollObject foreground = null;
    public Wall square = null;
    public float slope = 45;
    public float speed = 3;
    
    public enum Direction { LEFT, RIGHT }

    Wall current = null;
    Direction currentDirect;

    void Start()
    {
        currentDirect = Direction.RIGHT;    //change to left at the next
        // SpawnStep(slope, currentDirect);
        SpawnNextStep();
        Invoke("SpawnNextStep", 7);
    }

    public Wall SpawnNextStep()
    {
        float slopeValue = slope;
        if(currentDirect == Direction.LEFT)
        {
            currentDirect = Direction.RIGHT;
            slopeValue = -slope;
        }
        else if(currentDirect == Direction.RIGHT)
        {
            currentDirect = Direction.LEFT;
            slopeValue = slope;
        }
        
        Wall wall = SpawnStep(slopeValue, currentDirect);
        if(current)
        {
            for(int i = 0; i < current.transform.childCount; i++)
            {
                var child = current.transform.GetChild(i);
                if(child.gameObject.tag == Constants.NEXT_STEP_TAG)
                {
                    wall.transform.position = child.position;
                    wall.Init();
                    break;
                }
            }
        }
        current = wall;
        UpdateForeground(currentDirect);
        return wall;
    }

    public void UpdateForeground(Direction direction)
    {
        if(foreground)
        {
            foreground.isLeft = direction == Direction.LEFT ? true : false;
            foreground.customAngle = direction == Direction.LEFT ? slope : -slope;
            foreground.Init();
        }
    }

    public Wall SpawnStep(float slopeAngle, Direction direction)
    {
        var obj = Instantiate(square, transform);
        obj.transform.localEulerAngles = Vector3.forward * slopeAngle;
        ScrollObject scroll = obj.GetComponent<ScrollObject>();
        if(scroll)
        {
            scroll.isOnlyDown = false;
            scroll.speed = speed;
            scroll.isLeft = direction == Direction.LEFT ? true : false;
        }
        
        return obj;
    }
}
