using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speedX = 10;
    [SerializeField] float jump = 10;
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float delayMove = 0.1f;
    [SerializeField] Map map = null;
    [SerializeField] Logic logic = null;
    [SerializeField] Range lastTarget = null;
    [SerializeField] float detectX = 0.5f;
    [SerializeField] float jumpX = 0.5f;

    BoxCollider2D box = null;
    public Shooter shooter { get; private set; }
    GameObject frontObs = null;
    Vector2 direction = Vector2.left;

    bool endStair = false;
    bool isJump = false;
    bool isPlay = false;
    bool wasShoot = true;
    float targetX = Constants.INFINITY;
    float finalY = 0;

    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        NewFontObs();
    }   

    void Update()
    {
        if (isPlay)
        {
            if(Input.GetMouseButtonDown(0) && !wasShoot)
            {
                shooter.Shoot();
                shooter.StopAim();
                wasShoot = true;
            }
        }

        if (!isJump)
        {
            if (targetX != Constants.INFINITY)
            {
                void CompleteTarget()
                {
                    transform.position = new Vector2(targetX, transform.position.y);
                    Reverse();
                    StartPlay();
                }
                if (direction.x < 0 && targetX < transform.position.x)
                {
                    transform.Translate(direction * speedX * Time.deltaTime);
                    if (targetX >= transform.position.x) CompleteTarget();
                }
                else if(direction.x > 0 && targetX > transform.position.x)
                {
                    transform.Translate(direction * speedX * Time.deltaTime);
                    if (targetX <= transform.position.x) CompleteTarget();
                }
            }
            if (transform.position.y > finalY)
            {
                transform.Translate(Vector2.down * jump * 2 * Time.deltaTime);
                if (transform.position.y < finalY) transform.position = new Vector2(transform.position.x, finalY);
            }
            else
            {
                if (frontObs)
                {
                    Vector2 obsPos = frontObs.transform.position;
                    if (Math.Abs(transform.position.x - obsPos.x) <= detectX)
                    {
                        Vector2 cur = transform.position;
                        Vector2 target = new Vector2(cur.x + direction.x * jumpX, transform.position.y + frontObs.transform.localScale.y * jumpHeight);
                        StartCoroutine("Jump", target);
                    }
                    else
                    {
                        transform.Translate(direction * speedX * Time.deltaTime);
                    }
                }
            }
        }
    } 

    IEnumerator Jump(Vector2 target)
    {
        isJump = true;
        while (transform.position.y < target.y)
        {
            transform.Translate(Vector2.up * jump * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector2(transform.position.x, target.y);
        if (target.x < transform.position.x)
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
        isJump = false;
        yield return new WaitForSeconds(delayMove);
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
        float sY = 0;
        if(frontObs) sY = frontObs.transform.localScale.y;
        frontObs = logic.NextFontObs();
        if(!frontObs)
        {
            targetX = transform.position.x + direction.x * lastTarget.GetRandomAsInt();
            finalY += sY;
        }
        else
        {
            finalY = frontObs.transform.position.y;
        }
    }
    void Reverse()
    {
        direction.x *= -1;
        transform.localScale = transform.localScale * new Vector2(-1, 1);
    }

    void StartPlay()
    {
        isPlay = true;
        shooter.StartAim();
        logic.Play();
        wasShoot = false;
    }
}
