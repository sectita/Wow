using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float Speed/* = .3f*/;
    
    void LateUpdate()
    {
        if(target.position.x > transform.position.x)
        {
            Vector3 newPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = newPos + transform.position * Time.deltaTime * Speed;
        }
    }
}
