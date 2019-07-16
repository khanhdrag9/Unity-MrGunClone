using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speedX = 10;
    [SerializeField] float jump = 10;
    [SerializeField] Map map = null;

    Rigidbody2D body = null;
    Stair currentStair = null;
    GameObject curObs = null;
    int curObsIndex = 0;
    int stairIndex = -1;
    Vector2 direction = Vector2.right;
    Vector2 distanceDetect = Vector2.zero;

    public enum Status { MOVE, JUMP, IDLE }
    Status status = Status.IDLE;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        CompleteMoveStair();
        distanceDetect = map.distance * 0.75f;
        NextStair();
    }

    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if(status == Status.MOVE)
        {
            if (Math.Abs(curObs.transform.position.x - transform.position.x) > distanceDetect.x)
            {
                body.MovePosition(body.position + direction * new Vector2(speedX, 0) * Time.fixedDeltaTime);
            }
            else
            {
                status = Status.JUMP;
                body.MovePosition(new Vector2(curObs.transform.position.x + distanceDetect.x, transform.position.y));
            }
        }
        else if(status == Status.JUMP)
        {
            body.gravityScale = 0;
            if (Math.Abs(curObs.transform.position.y - transform.position.y) < distanceDetect.y)
            {
                body.MovePosition(body.position + Vector2.up * new Vector2(0, jump) * Time.fixedDeltaTime);
            }
            else
            {
                status = Status.MOVE;
                body.MovePosition(new Vector2(transform.position.x, curObs.transform.position.y + distanceDetect.y));
            }
        }
        
        
    }

    private void Jump()
    {
        
    }

    //IEnumerator MoveYTo(float y)
    //{
        
    //}

    //IEnumerator MoveXTo(float x)
    //{
        
    //}

    public void NextStair()
    {
        //body.velocity = direction * speedX;
        //numberStair = map.stairs[stairIndex].stairList.Count;
        status = Status.MOVE;
    }
    public void CompleteMoveStair()
    {
        if(stairIndex >= 0 && stairIndex <= map.stairs.Count)
        {
            map.stairs[stairIndex].SetEnableColliderWall(true);
        }
        stairIndex++;
        if (stairIndex >= 0 && stairIndex <= map.stairs.Count)
        {
            currentStair = map.stairs[stairIndex];
            currentStair.SetEnableColliderStair(true);
            curObsIndex = 0;
            curObs = currentStair.stairList[curObsIndex];
        }
        direction *= -1;
    }
}
