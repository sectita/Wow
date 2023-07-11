using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    private readonly float moveablSpeed = 1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Cos(Time.time) * moveablSpeed;
        float y = Mathf.Sin(Time.time) * moveablSpeed;
        float z = transform.position.z;

        Vector3 movpos = new Vector3(x, y, z);
        transform.position = startPos + movpos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("you entered in triggered zone");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("you exited in triggered zone");
        }
    }
}
