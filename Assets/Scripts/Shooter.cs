using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject gun = null;
    [SerializeField] GameObject bullet = null;
    [SerializeField] Range aimRanger = null;
    [SerializeField] GameObject aim = null;
    [SerializeField] GameObject shootPoint = null;
    [SerializeField] float speed = 1f;
    [SerializeField] float bulletSpeed = 50f;

    bool isAim = false;
    float direction = 1;

    void Awake()
    {
        aim.SetActive(false);
    }
    public void StartAim()
    {
        isAim = true;
        direction = 1;
        aim.SetActive(true);
        gun.transform.eulerAngles = new Vector3(0, 0, aimRanger.min + 1);
    }

    void Update()
    {
        if(isAim)
        {
            gun.transform.Rotate(Vector3.forward * direction * speed * Time.deltaTime);
            float angle = Convert.ToInt32(gun.transform.eulerAngles.z);
            if(angle >= aimRanger.max)
                direction = -1;
            else if(angle <= aimRanger.min|| angle >= 180)
                direction = 1;
        }
    }

    public void Shoot()
    {
        float angle = gun.transform.eulerAngles.z;
        float x = 10 * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = 10 * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 offset = new Vector2(x, y).normalized;
        
        var obj = Instantiate(bullet, shootPoint.transform.position, gun.transform.rotation);
        var body = obj.GetComponent<Rigidbody2D>();
        body.velocity = offset * bulletSpeed;
    }
}

