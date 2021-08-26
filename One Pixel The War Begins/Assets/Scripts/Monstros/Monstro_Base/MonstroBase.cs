using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBase : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private AudioSource audio_morte;
    private SpriteRenderer sr;
    private float velocidade;
    private Transform player_pos;
    private bool caiu;
    private bool morreu;

    // Start is called before the first frame update
    void Start()
    {
        morreu = false;
        caiu = false;
        velocidade = 0.5f;
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio_morte = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("esperarCair");
    }

    // Update is called once per frame
    void Update()
    {

        player_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(caiu) {
            if(player_pos.position.x > transform.position.x) {
                transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
            } else if(transform.position.x > player_pos.position.x){
                transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
            }
        }

        if(Chefao01.metade_vida && caiu) {
            sr.color = Color.red;
            velocidade = 1.2f;
        } else if (Chefao01.metade_vida){
            sr.color = Color.red;
        } else {
            sr.color = Color.white;
        }

        if(FaseManager.chefao_vivo == false) {
            Morte(0);
        }

        if(morreu) {
            sr.color = Color.white;
            caiu = false;
            velocidade = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
         if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player")) {
            Morte(1);
        } else if(other.gameObject.CompareTag("chao")) {
            anim.SetBool("levantar", true);
            StartCoroutine("liberarAndada");
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator esperarCair() {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("nasceu", true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Morte(int qual) {
        morreu = true;
        collider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Static;
        if(qual == 0) {
            anim.SetBool("morte_caiu", true);
            Destroy(gameObject, 0.55f);
        } else if(qual == 1) {
            audio_morte.Play();
            anim.SetBool("morte_tiro", true);
            Destroy(gameObject, 1.2f);
        }
    }

    IEnumerator liberarAndada() {
        yield return new WaitForSeconds(1.35f);
        caiu = true;
    }
}
