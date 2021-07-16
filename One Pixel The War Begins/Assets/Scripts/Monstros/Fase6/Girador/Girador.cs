using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girador : MonoBehaviour
{
    private float velocidade;
    private Rigidbody2D corpo;
    private Animator anim;
    private BoxCollider2D collider;
    private Transform target;
    private bool morreu;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        morreu = false;
        velocidade = 3f;
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!morreu) {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
        }

        if(Chefao05.metade_da_vida) {
            anim.SetBool("raiva", true);
            if(velocidade != 0) {
                velocidade = 3.25f;
            }
        }

        if(Portal.atira_ae_po == 3) {
            velocidade = 2.5f;
            sr.color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("chao")){
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("tijolo")) {
            morreu = true;
            Morrer();
            StartCoroutine("morre");
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(1.4f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        anim.SetBool("morreu", true);
        collider.isTrigger = true;
        corpo.gravityScale += 0.1f;
        velocidade = 0;
    }
}
