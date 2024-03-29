using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girador : MonoBehaviour
{
    private float velocidade;
    private Rigidbody2D corpo;
    private Animator anim;
    private BoxCollider2D collider_girador;
    private Transform target;
    private bool morreu;
    private SpriteRenderer sr;
    private AudioSource som_morte;

    // Start is called before the first frame update
    void Start()
    {
        morreu = false;
        velocidade = 3f;
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider_girador = GetComponent<BoxCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        som_morte = GetComponent<AudioSource>();
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

        if(FaseManager6.pode_comecar_6 == false) {
            Morrer();
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
            som_morte.Play();
            Morrer();
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(1.4f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        morreu = true;
        sr.color = Color.white;
        anim.SetBool("morreu", true);
        collider_girador.isTrigger = true;
        corpo.gravityScale += 0.1f;
        velocidade = 0;
        StartCoroutine("morre");
    }
}
