using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBomba : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D collider;
    private Transform target;
    private float speed;
    private Rigidbody2D corpo;
    private bool morreu;

    // Start is called before the first frame update
    void Start()
    {
      morreu = false;
      speed = 2.8f;
      anim = GetComponent<Animator>();
      collider = GetComponent<BoxCollider2D>();  
      corpo = GetComponent<Rigidbody2D>();
      target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!morreu) {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }   
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("tijolo")) {
            morreu = true;
            corpo.gravityScale += 0.1f;
            speed = 0;
            anim.SetBool("morreu", true);
            collider.isTrigger = true;
            StartCoroutine("morre");
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("chao")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            speed = 0;
            anim.SetBool("explode", true);
            StartCoroutine("morreExplode");
        }
    }

    IEnumerator morre() {
        yield return new WaitForSeconds(2.2f);
        Destroy(this.gameObject);
    }

    IEnumerator morreExplode() {
        yield return new WaitForSeconds(1.6f);
        Destroy(this.gameObject);
    }
}