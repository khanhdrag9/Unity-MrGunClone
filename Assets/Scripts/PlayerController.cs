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
    [SerializeField] Range lastTarget = null;

    Rigidbody2D body = null;
    BoxCollider2D box = null;
    public Shooter shooter { get; private set; }
    GameObject frontObs = null;
    Vector2 direction = Vector2.left;
    float gravityScale = 10;
    float xdetect = 0.15f;
    bool endStair = false;
    bool isJump = false;
    bool isPlay = false;
    bool wasShoot = true;
    float targetX = Constants.INFINITY;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        shooter = GetComponent<Shooter>();
        gravityScale = body.gravityScale;
    }

    void Start()
    {
        NewFontObs();
    }   

    void Update()
    {
        if(isPlay)
        {
            if(Input.GetMouseButtonDown(0) && !wasShoot)
            {
                shooter.Shoot();
                shooter.StopAim();
                wasShoot = true;
            }
        }

        if(frontObs)
        {
            if(Math.Abs(transform.position.x - frontObs.transform.position.x) <= xdetect + Mathf.Abs(transform.localScale.x / 2) && !isJump)
            {
                Vector2 cur = transform.position;
                Vector2 target = new Vector2(cur.x + direction.x * 0.5f, transform.position.y + frontObs.transform.localScale.y);
                StartCoroutine("Jump", target);
            }
        }
    } 

    void FixedUpdate()
    {
        if(!isJump && !isPlay)
        {
            if((direction.x < 0 && targetX < body.position.x) || (direction.x > 0 && targetX > body.position.x) || targetX == Constants.INFINITY)
            {
                body.MovePosition(body.position + direction * speedX * Time.fixedDeltaTime);
            }
            else
            {
                Reverse();
                StartPlay();
                isPlay = true;
            }
        }
    }

    IEnumerator Jump(Vector2 target)
    {
        isJump = true;
        body.gravityScale = 0;

        while (transform.position.y < target.y)
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
        body.gravityScale = gravityScale;
        isJump = false;
    }

    public void MoveToNextStair()
    {
        isPlay = false;
        shooter.Reset();
        targetX = Constants.INFINITY;
        NewFontObs();
    }
    void NewFontObs()
    {
        frontObs = logic.NextFontObs();
        if(!frontObs)
        {
            targetX = transform.position.x + direction.x * lastTarget.GetRandomAsInt();
        }
    }
    void Reverse()
    {
        direction.x *= -1;
        transform.localScale = transform.localScale * new Vector2(-1, 1);
    }

    void StartPlay()
    {
        shooter.StartAim();
        logic.Play();
        wasShoot = false;
    }
}
