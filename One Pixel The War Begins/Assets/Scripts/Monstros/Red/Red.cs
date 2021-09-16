using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    private Transform target;
    private float speed;
    private Rigidbody2D corpo;
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        speed = 0.5f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 relativePosition = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(relativePosition);

        transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        corpo.MovePosition(Vector2.MoveTowards(corpo.position, target.position, speed * Time.deltaTime));

        if(transform.position.x < target.transform.position.x) {
            Vector3 vetor = transform.localScale;
            vetor *= -1;
            transform.localScale = vetor;
        }
    }
}
