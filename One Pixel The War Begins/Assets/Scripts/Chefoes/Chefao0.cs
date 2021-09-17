using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao0 : MonoBehaviour
{
    private BoxCollider2D collider_gosma;
    private Rigidbody2D corpo;
    private float velocidade;
    private Animator anim;
    private float forca_pulo;
    private Transform target;
    private bool lookingRight;
    public Animator anim_back;
    private AudioSource som_batida;
    public AudioSource som_pulo;
    public AudioSource som_dano;
    private int vida_boss0;
    private bool tomandoDano;

    void Start()
    {
        tomandoDano = false;
        vida_boss0 = 40;
        lookingRight = false;
        collider_gosma = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocidade = 0.5f;
        forca_pulo = 500f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        som_batida = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < target.transform.position.x) {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
            if(!lookingRight) {
                Flip();
            }
        } else {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
            if(lookingRight) {
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            som_batida.Play();
            Camera.tremer_chao = true;
            anim_back.SetBool("tremer_chao", true);
            anim.SetBool("pular", false);
        } else if(other.gameObject.CompareTag("bullet")) {
            if(!tomandoDano) {
                vida_boss0--;
            }
            if(vida_boss0 == 30 || vida_boss0 == 20 || vida_boss0 == 10) {
                som_dano.Play();
                velocidade = 0;
                tomandoDano = true;
                anim.SetBool("dano", true);
                StartCoroutine("voltarDano");
            }
        }
    }

    void Flip(){
      lookingRight = !lookingRight;
      Vector3 myScale = transform.localScale;
      myScale.x *= -1;
      transform.localScale = myScale;
   }

    public void Pular() {
        if(!tomandoDano) {
            som_pulo.Play();
            corpo.AddForce(new Vector2(0, forca_pulo));
        }
    }

    public void Cair() {
        anim.SetBool("pular", true);
    }

    IEnumerator voltarDano() {
        yield return new WaitForSeconds(2f);
        som_dano.Stop();
        velocidade = 0.5f;
        anim.SetBool("dano", false);
        anim.SetBool("pular", true);
        tomandoDano = false;
    }
}
