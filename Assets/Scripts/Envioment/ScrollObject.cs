using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 100;
    public float customAngle = 0;
    public bool isLeft = true;
    public bool isOnlyDown = true;

    public float revert = 0;
    protected Vector2 velocity;
    protected Vector2 origin;

    void Start()
    {
        // Init();
    }

    public void Init()
    {
        origin = transform.position;
        if(isOnlyDown)
        {
            if(revert==0)revert = UnityEngine.Camera.main.orthographicSize;
            velocity = Vector2.down;
        }
        else
        {
            if(revert==0)revert = UnityEngine.Camera.main.orthographicSize;
            float angle = 0;
            if(customAngle == 0) angle = transform.localEulerAngles.z;
            else angle = customAngle;
            float vx = Mathf.Cos(angle * Mathf.Deg2Rad);
            float vy = Mathf.Sin(angle * Mathf.Deg2Rad);
            velocity = new Vector2(vx, vy).normalized;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if(isOnlyDown)
        {
            transform.Translate(velocity * Time.deltaTime * speed, Space.World);
            if(transform.position.y < -revert)transform.position = origin;
        }
        else
        {
            if(isLeft)
            {
                transform.Translate(velocity * Time.deltaTime * -1 * speed, Space.World);
                if(transform.position.x < -revert) transform.position = origin;
            }
            else
            {
                transform.Translate(velocity * Time.deltaTime * speed, Space.World);
                if(transform.position.x > revert) transform.position = origin;
            }
        }
    }
}
