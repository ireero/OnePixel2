using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chefao01 : MonoBehaviour
{

    private Animator anim;
    private float contador = 0;

    private BoxCollider2D collider;

    private float velocidade;

    private float vida;
    private float vidaMaxima = 100;

    private bool atacando;
    private bool atacou;

    public static bool bateu_chao;

    private SpriteRenderer sr;

    private Rigidbody2D corpo_vilao;

    public static bool metade_vida;
    public static bool vivo;

    private int contagem_danos;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    // Start is called before the first frame update
    void Start()
    {
        velocidade = 10f;
        vivo = true;
        metade_vida = false;
        PlayerControle.podeAtirar = false;
        PlayerControle.pode_mexer = false;
        vida = 100f;
        contagem_danos = 0;
        bateu_chao = false;
        atacou = false;
        atacando = false;
        contador = 0;
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        corpo_vilao = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        BarraVida();

        if(contagem_danos == 10) {
            contagem_danos = 0;
            anim.SetBool("tomou_dano", true);
            anim.SetBool("idle", false);
            StartCoroutine("voltarDoDano");
        }

        if(vida <= 50f && vida > 0) {
            BarraVidaMaior.color = Color.red;
            sr.color = Color.red;
            velocidade = 15f;
            metade_vida = true;
        }

        if(bateu_chao) {
            contador += Time.deltaTime;
            if(!metade_vida) {
                if(contador >= 5f && !atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
                if(contador >= 6.5f && !atacou) {
                    transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
                    atacando = true;
                }
            } else if(contador >= 5f && atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
               if(contador>= 6.5f && atacou) {
                    transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
                    atacando = false;
               }
            }
        } else {
            if(contador >= 2.5f && !atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
                if(contador >= 3.5f && !atacou) {
                    transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
                    atacando = true;
                }
            } else if(contador >= 2.5f && atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
               if(contador>= 3.5f && atacou) {
                    transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
                    atacando = false;
               }
            }
        }

            if(vida <= 0) {
                sr.color = Color.white;
                Destroy(BarraVidaMaior);
                FaseManager.podeSpawn = false;
                velocidade = 0;
                vivo = false;
                corpo_vilao.bodyType = RigidbodyType2D.Static;
                collider.isTrigger = true;  
                anim.SetBool("morreu", true);
                StartCoroutine("esperarMorte");
            }
        }   
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            vida--;
            contagem_danos++;
        } else if(other.gameObject.CompareTag("plataforma") || other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if(other.gameObject.CompareTag("chao") && bateu_chao == false) {
            Camera.tremer_chao = true;
            bateu_chao = true;
            anim.SetBool("apareceu", true);
            StartCoroutine("esperaAnimAparecer");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("parar_chefe")) {
            anim.SetBool("atacar", false);
            StartCoroutine("voltarDoAtaque");
            contador = 0;
            atacou = !atacou;
            sr.flipX = !sr.flipX;
        }
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("tomou_dano", false);
        anim.SetBool("idle", true);
    }

    IEnumerator esperaAnimAparecer() {
        yield return new WaitForSeconds(1f);
        anim.SetBool("idle", true);
        anim.SetBool("apareceu", false);
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;
    }

    IEnumerator voltarDoAtaque() {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("idle", true);
    }

    IEnumerator esperarMorte() {
        velocidade = 0;
        yield return new WaitForSeconds(4.5f);
        Destroy(this.gameObject);
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = vida / vidaMaxima; 
    }
}
