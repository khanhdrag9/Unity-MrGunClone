using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

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
    [SerializeField] UnityArmatureComponent bonesAnimation = null;


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

    public void Start()
    {
        Animate(Constants.PA_IDLE, -1);
        shooter.gun.Charge(false);
    }

    public void StartGame()
    {
        Animate(Constants.PA_RUN, -1);
        shooter.gun.Charge(true);
    }

    void Reverse()
    {
        direction.x *= -1;
        transform.localScale = transform.localScale * new Vector2(-1, 1);
    }

    void StartPlay()
    {
        isPlay = true;
        Animate(Constants.PA_HOLD, -1);
        shooter.StartAim();
        wasShoot = false;
    }

    //Animation
    public void Animate(string animation, int number)
    {
        bonesAnimation.animation.Play(animation, number);
    }
}
