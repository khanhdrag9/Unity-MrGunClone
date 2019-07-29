using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 100;
    public bool isLeft = true;
    public bool isOnlyDown = true;

    public float revert = 0;
    Vector2 velocity;
    Vector2 origin;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        origin = transform.position;
        if(isOnlyDown)
        {
            revert = UnityEngine.Camera.main.orthographicSize;
            velocity = Vector2.down * speed;
        }
        else
        {
            revert = UnityEngine.Camera.main.orthographicSize;
            float angle = transform.localEulerAngles.z;
            float vx = Mathf.Cos(angle * Mathf.Deg2Rad);
            float vy = Mathf.Sin(angle * Mathf.Deg2Rad);
            velocity = new Vector2(vx, vy).normalized * speed;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if(isOnlyDown)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            if(transform.position.y < -revert)transform.position = origin;
        }
        else
        {
            if(isLeft)
            {
                transform.Translate(velocity * Time.deltaTime * -1, Space.World);
                if(transform.position.x < -revert) transform.position = origin;
            }
            else
            {
                transform.Translate(velocity * Time.deltaTime, Space.World);
                if(transform.position.x > revert) transform.position = origin;
            }
        }
    }
}
