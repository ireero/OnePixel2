using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoadorBase : MonoBehaviour
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
        speed = 2.5f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(!morreu) {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if(Chefao05.metade_da_vida) {
            anim.SetBool("raiva", true);
            if(speed != 0) {
                speed = 2.75f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("tijolo")) {
            morreu = true;
            speed = 0;
            anim.SetBool("morreu", true);
            collider.isTrigger = true;
            corpo.gravityScale += 0.1f;
            StartCoroutine("morre");
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("chao")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
        yield return new WaitForSeconds(1.45f);
        Destroy(this.gameObject);
    }
}
