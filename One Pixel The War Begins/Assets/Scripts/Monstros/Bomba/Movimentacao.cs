using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Transform target;
    private float speed;

    void Start()
    {
        speed = 1.5f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }
}
