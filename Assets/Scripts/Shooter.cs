using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Bullet bullet = null;
    [SerializeField] Range aimRanger = null;
    [SerializeField] float speed = 1f;
    [SerializeField] float bulletSpeed = 50f;
    public Gun gun;

    List<Bullet> bullets;

    bool isAim = false;
    float direction = 1;

    void Start()
    {
        bullets = new List<Bullet>();
        gun.Aim(false);
    }

    public void StartAim()
    {
        isAim = true;
        direction = 1;
        gun.Aim(true);
        gun.transform.localEulerAngles = new Vector3(0, 0, aimRanger.min + 1);
    }

    void Update()
    {
        if (isAim)
        {
            gun.transform.Rotate(Vector3.forward * direction * speed * Time.deltaTime);
            float angle = Convert.ToInt32(gun.transform.localEulerAngles.z);
            if (angle >= aimRanger.max)
            {
                gun.transform.localEulerAngles = Vector3.forward * aimRanger.max;
                direction = -1;
            }
            else if (angle <= aimRanger.min || angle >= 180)
            {
                gun.transform.localEulerAngles = Vector3.forward * aimRanger.min;
                direction = 1;
            }
                
        }
    }

    public void ShootTo(Vector2 position)
    {
        StartCoroutine(AimTo(position));
    }

    IEnumerator AimTo(Vector2 position)
    {
        float angle = Vector2.Angle(transform.position, position);
        if(angle < gun.transform.localEulerAngles.z)
        {
            while(angle < gun.transform.localEulerAngles.z)
            {
                gun.transform.Rotate(Vector3.forward * -1 * speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (angle > gun.transform.localEulerAngles.z)
            {
                gun.transform.Rotate(Vector3.forward * 1 * speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        gun.transform.localEulerAngles = new Vector3(0, 0, angle);
        Shoot(position);
    }

    public void Shoot()
    {
        Shoot(Vector3.zero);
    }

    private void Shoot(Vector3 to)
    {
        float angle = gun.transform.localEulerAngles.z;
        float x = 10 * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = 10 * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 offset = new Vector2(x, y).normalized;
        var obj = gun.SpawnProfile();
        bullets.Add(obj);
        obj.transform.position = gun.transform.position;
        obj.gameObject.layer = gameObject.layer;
        //offset.y = 0;
        //obj.velocity = offset * bulletSpeed;
        offset.x *= transform.localScale.x;
        //obj.GetComponent<Rigidbody2D>().velocity = offset * bulletSpeed;
        obj.GetComponent<Rigidbody2D>().AddForce(offset * bulletSpeed);
    }

    public void StopAim()
    {
        //gun.transform.localEulerAngles = new Vector3(0, 0, aimRanger.min + 1);
        gun.Aim(false);
        isAim = false;
        direction = 1;
    }

    public void Reset()
    {
        foreach(var bullet in bullets)
        {
            bullet.isCheck = true;
        }
        StopAim();
        StopAllCoroutines();
    }
}

