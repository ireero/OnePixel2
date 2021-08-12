using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBomba : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D collider;
    private CircleCollider2D rastreador_collider;
    private Transform target;
    private float speed;
    private Rigidbody2D corpo;
    private bool morreu;
    private SpriteRenderer sr;
    private bool explodindo;

    // Start is called before the first frame update
    void Start()
    {
      explodindo = false;
      morreu = false;
      speed = 2.8f;
      anim = GetComponent<Animator>();
      collider = GetComponent<BoxCollider2D>();  
      rastreador_collider = GetComponent<CircleCollider2D>();
      corpo = GetComponent<Rigidbody2D>();
      target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!morreu) {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } else {
            speed = 0;
        }

        if(FaseManager6.pode_comecar_6 == false) {
            Morte();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if((other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("tijolo")) && !explodindo) {
            Morte();
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("chao")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !morreu) {
            explodindo = true;
            rastreador_collider.enabled = false;
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

    private void Morte() {
        sr.color = Color.white;
        morreu = true;
        corpo.gravityScale += 0.1f;
        speed = 0;
        anim.SetBool("morreu", true);
        collider.isTrigger = true;
        StartCoroutine("morre");
    }
}
