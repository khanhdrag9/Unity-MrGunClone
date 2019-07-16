using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speedX = 10;
    [SerializeField] float jump = 10;
    [SerializeField] Map map = null;
    [SerializeField] Logic logic = null;

    Rigidbody2D body = null;
    GameObject frontObs = null;
    Vector2 direction = Vector2.left;
    float xdetect = 0.75f;
    bool endStair = false;
    bool isJump = false;
    float targetX = Constants.INFINITY;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        NewFontObs();
    }   

    void Update()
    {
        if(frontObs)
        {
            if(Math.Abs(transform.position.x - frontObs.transform.position.x) <= xdetect && !isJump)
            {
                Vector2 cur = transform.position;
                Vector2 target = new Vector2(cur.x + direction.x * 0.5f, cur.y + frontObs.transform.localScale.y);
                Debug.Log("Target : " + target);
                StartCoroutine("Jump", target);
            }
        }
        else
        {
            
        }
    } 

    void FixedUpdate()
    {
        if(!isJump)
        {
            if((direction.x < 0 && targetX < body.position.x) || (direction.x > 0 && targetX > body.position.x) || targetX == Constants.INFINITY)
            {
                body.MovePosition(body.position + direction * speedX * Time.fixedDeltaTime);
            }
        }
    }

    IEnumerator Jump(Vector2 target)
    {
        Debug.Log("Jump");
        isJump = true;
        body.gravityScale = 0;
        while(transform.position.y < target.y)
        {
            transform.Translate(Vector2.up * jump * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector2(transform.position.x, target.y);
        if(target.x < transform.position.x)
        {
            while(transform.position.x > target.x)
            {
                transform.Translate(Vector2.left * speedX * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        else if(target.x > transform.position.y)
        {
            while(transform.position.x < target.x)
            {
                transform.Translate(Vector2.right * speedX * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        transform.position = new Vector2(target.x, transform.position.y);
        NewFontObs();
        body.gravityScale = 1;
        isJump = false;
    }

    void NewFontObs()
    {
        frontObs = logic.NextFontObs();
        if(frontObs)
        {
            frontObs.GetComponent<BoxCollider2D>().enabled = true;
        }
        else 
        {
            targetX = transform.position.x + direction.x * 1.5f;
            // logic.NextStair();
            // direction *= -1;
            // NewFontObs();
        }
        
    }
}
