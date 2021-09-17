using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chefao01 : MonoBehaviour
{

    private Animator anim;
    private float contador = 0;

    private BoxCollider2D collider_chefao1;

    private float velocidade;

    public static float vida;

    private bool atacou;

    public static bool bateu_chao;

    private SpriteRenderer sr;

    private Rigidbody2D corpo_vilao;

    public static bool metade_vida;

    private int contagem_danos;
    public static bool morrer_de_vez;

    public AudioSource somResp;
    private bool umaVez;
    public AudioSource som_morte;
    public AudioSource dano;

    public GameObject escada;

    public Animator anim_back;

    // Start is called before the first frame update
    void Start()
    {
        umaVez = false;
        morrer_de_vez = false;
        metade_vida = false;
        velocidade = 10f;
        vida = 100f;
        contagem_danos = 0;
        bateu_chao = false;
        atacou = false;
        contador = 0;
        sr = GetComponent<SpriteRenderer>();
        collider_chefao1 = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        corpo_vilao = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(contagem_danos == 10) {
            contagem_danos = 0;
            anim.SetBool("tomou_dano", true);
            anim.SetBool("idle", false);
            StartCoroutine("voltarDoDano");
        }

        if(vida <= 50f && vida > 0) {
            if(vida == 50) {
                anim.SetBool("meia_vida", true);
                StartCoroutine("meiaVida");
            }
            sr.color = Color.red;
            velocidade = 15f;
            metade_vida = true;
        }

        if(bateu_chao) {
            contador += Time.deltaTime;
            if(!metade_vida) {
                if(!umaVez) {
                        somResp.Play();
                        umaVez = true;
                    }
                if(contador >= 5f && !atacou) {
                    anim.SetBool("atacar", true);
                    anim.SetBool("idle", false);
                if(contador >= 6.5f && !atacou) {
                    somResp.Stop();
                    umaVez = false;
                    transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
                }
            } else if(contador >= 5f && atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
               if(contador>= 6.5f && atacou) {
                   somResp.Stop();
                   umaVez = false;
                    transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
               }
            }
        } else {
            if(!umaVez) {
                    somResp.Play();
                    umaVez = true;
                }
            if(contador >= 1.5f && !atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
                if(contador >= 2.9f && !atacou) {
                    somResp.Stop();
                    umaVez = false;
                    transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
                }
            } else if(contador >= 1.5f && atacou) {
                anim.SetBool("atacar", true);
                anim.SetBool("idle", false);
               if(contador>= 2.9f && atacou) {
                    somResp.Stop();
                    umaVez = false;
                    transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
               }
            }
        }

        if(morrer_de_vez) {
            anim.SetBool("morreu", false);
            StartCoroutine("morrerDeVez");
        }

            if(vida <= 0 && vida > -10) {
                somResp.Stop();
                som_morte.Play();
                vida = -10;
                sr.color = Color.white;
                FaseManager.podeSpawn = false;
                velocidade = 0;
                corpo_vilao.bodyType = RigidbodyType2D.Static;
                collider_chefao1.isTrigger = true;  
                anim.SetBool("morreu", true);
                StartCoroutine("esperarMorte");
            }
        }   
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            dano.Play();
            vida--;
            contagem_danos++;
        } else if(other.gameObject.CompareTag("plataforma") || other.gameObject.CompareTag("monstro") || 
        other.gameObject.CompareTag("monstro_base")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if(other.gameObject.CompareTag("chao") && !bateu_chao) {
            Camera.tremer_chao = true;
            anim_back.SetBool("tremer_chao", true);
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
        FaseManager.podeSpawn = true;
        anim.SetBool("idle", true);
        anim.SetBool("apareceu", false);
        PlayerControle.conversando = false;
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;
    }

    IEnumerator voltarDoAtaque() {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("idle", true);
    }

    IEnumerator esperarMorte() {
        yield return new WaitForSeconds(2.1f);
        FaseManager.contagem_falas = 6;
        FaseManager.chefao_vivo = false;
        som_morte.Stop();
    }

    IEnumerator morrerDeVez() {
        yield return new WaitForSeconds(1.1f);
        escada.SetActive(true);
        Destroy(this.gameObject);
    }

    IEnumerator meiaVida() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("idle", true);
        anim.SetBool("meia_vida", false);
    }
}
