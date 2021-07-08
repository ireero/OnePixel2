using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroOlho : MonoBehaviour
{   
    private CircleCollider2D collider;
    private Animator anim;
    private Transform target;
    private float speed;
    private Rigidbody2D corpo;
    private bool achou;
    private bool morreu;
    private float contador;

    void Start()
    {
        contador = 0;
        morreu = false;
        achou = false;
        speed = 0.8f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        corpo = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        contador+= Time.deltaTime;
        if(!achou) {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        } else {
            if(!morreu) {
                transform.LookAt(target.position);
                transform.Rotate(new Vector3(0, 90, 0), Space.Self);

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }

        if(Chefao06.atacando == false || contador >= 12f) {
            morreu = true;
            speed = 0;
            collider.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("morreu", true);
            Destroy(this.gameObject, 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            morreu = true;
            speed = 0;
            collider.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("morreu", true);
            Destroy(this.gameObject, 0.8f);
        } else if(other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            speed = 1.5f;
            achou = true;
        }
    }
}
