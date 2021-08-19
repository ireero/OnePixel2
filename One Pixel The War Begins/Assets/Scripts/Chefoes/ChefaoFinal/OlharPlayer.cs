using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharPlayer : MonoBehaviour
{
    private Transform target;
    private float speed;
    void Start()
    {
        speed = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 relativePosition = target.position - transform.position;

        if(!PixelPreto.estaNaDireita) {
            transform.rotation = Quaternion.LookRotation(-relativePosition);
        } else {
            transform.rotation = Quaternion.LookRotation(relativePosition);
        }

        transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
