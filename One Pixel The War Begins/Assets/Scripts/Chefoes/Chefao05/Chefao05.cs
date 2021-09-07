using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao05 : MonoBehaviour
{
    private float contador;
    private BoxCollider2D collider;
    private Rigidbody2D corpo;
    private Animator anim;

    private SpriteRenderer sr;

    public Transform spawn_tiro;
    public Transform spawn_tiro_2;
    public Transform spawn_tiro_3;
    public Transform spawn_tiro_4;
    public Transform spawn_tiro_5;

    public Transform spawn_tiro_1_meia;
    public Transform spawn_tiro_2_meia;
    public Transform spawn_tiro_3_meia;
    public Transform spawn_tiro_4_meia;

    public GameObject bomba;
    public GameObject girador;
    public GameObject monstro_base;

    public SpriteRenderer girador_sr;
    public SpriteRenderer base_sr;

    private bool pode_atirar;
    private int tirosDados;

    private float nextFire;
    private bool morto;

    public static float vida;
    private int contagem_danos;

    private bool umaVez;

    public static bool metade_da_vida;

    public int valor_alet;

    private float tempo_para_atirar;
    private float pausa_de_tiro;

    public AudioSource somTerremoto;
    public AudioSource somDano;
    public AudioSource desintegrando;

    private bool umTerremoto;

    public GameObject escada;

    public bool umaMorte;

    void Start()
    {
        umaMorte = false;
        umTerremoto = false;
        pausa_de_tiro = 2f;
        tempo_para_atirar = 7f;
        valor_alet = 0;
        metade_da_vida = false;
        umaVez = false;
        contagem_danos = 0;
        vida = 600f;
        nextFire = 0;
        morto = false;
        tirosDados = 0;
        pode_atirar = false;
        contador = 0;
        collider = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        base_sr.color = Color.white;
        girador_sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        nextFire += Time.deltaTime;

        if(FaseManager6.pode_comecar_6 == false) {
            somTerremoto.Stop();
            contador = 0;
            nextFire = 0;
        }


        if(contagem_danos >= 50) {
            contagem_danos = 0;
            anim.SetBool("tomou_dano", true);
            StartCoroutine("voltarDoDano");
        } 

        if((contador >= tempo_para_atirar) && FaseManager6.pode_comecar_6 == true) {
            contador = -20f;
            anim.SetBool("idle", false);
            if(valor_alet == 1) {
                anim.SetBool("atacando", true);
                StartCoroutine("atirandoIdle");
            } else if(valor_alet == 2) {
                anim.SetBool("atacando2", true);
                StartCoroutine("atirandoIdle2");
            } else if(valor_alet == 3) {
                anim.SetBool("atacando3", true);
                StartCoroutine("atirandoIdle3");
            }
        }

        if(contador >= 0 && contador < 7f) {
            if(!metade_da_vida) {
                valor_alet = Random.Range(1, 3);
            } else {
                valor_alet = Random.Range(1, 4);
            }
        }

        if((contador >= -5f && contador < 0) && pode_atirar) {
            somTerremoto.Stop();
            anim.SetBool("idle_atacando", false);
            anim.SetBool("idle_atacando2", false);
            anim.SetBool("idle_atacando3", false);
            FaseManager6.podeCair = false;
            Camera.tremer_bastante = false;
            pode_atirar = false;
            StartCoroutine("voltarDoAtaque");
        }

        if((nextFire >= pausa_de_tiro) && pode_atirar) {
            Fire();
            nextFire = 0;
        }

        if(vida <= 0) {
            if(!umaMorte) {
                desintegrando.Play();
                umaMorte = true;
            }
            somTerremoto.Stop();
            collider.isTrigger = true;
            FaseManager6.podeCair = false;
            Camera.tremer_bastante = false;
            morto = true;
            sr.color = Color.white;
            base_sr.color = Color.white;
            girador_sr.color = Color.white;
            anim.SetBool("morreu", true);
            StartCoroutine("morrendo");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            somDano.Play();
            vida--;
            contagem_danos++;
            if(vida <= 300f && !umaVez) {
                if(GameManager.sem_dialogos == 0) {
                    FaseManager6.contagem_falas_6 = 3;
                }
                somTerremoto.Stop();
                umTerremoto = false;
                pausa_de_tiro = 1.65f;
                tempo_para_atirar = 3.5f;
                girador_sr.color = Color.red;
                base_sr.color = Color.red;
                umaVez = true;
                FaseManager6.podeCair = false;
                Camera.tremer_bastante = false;
                metade_da_vida = true;
                contador = -5f;
                if(pode_atirar) {
                    pode_atirar = false;
                    StartCoroutine("meiaVidaAtirando");
                } else {
                    pode_atirar = false;
                    StartCoroutine("meiaVida");
                }
                anim.SetBool("meia_vida", true);
                sr.color = Color.red;
            }
        } else if(other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("tijolo")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } 
    }

    IEnumerator morrendo() {
        yield return new WaitForSeconds(3.5f);
        escada.SetActive(true);
        desintegrando.Stop();
        if(GameManager.sem_dialogos == 0) {
            FaseManager6.contagem_falas_6 = 5;
        }
        Destroy(this.gameObject);
    }

    IEnumerator atirandoIdle() {
        yield return new WaitForSeconds(3.35f);
        atirandoresIdle(1);
    }

    IEnumerator atirandoIdle2() {
        yield return new WaitForSeconds(3.35f);
        atirandoresIdle(2);
    }

    IEnumerator atirandoIdle3() {
        yield return new WaitForSeconds(3.35f);
        atirandoresIdle(3);
    }

    IEnumerator voltarDoAtaque() {
        yield return new WaitForSeconds(2.1f);
        umTerremoto = false;
        anim.SetBool("idle", true);
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(1.7f);
        anim.SetBool("tomou_dano", false);
    }

    IEnumerator meiaVida() {
        yield return new WaitForSeconds(1.5f);
        contador = 0;
        anim.SetBool("meia_vida", false);
        anim.SetBool("idle", true);
    }

    IEnumerator meiaVidaAtirando() {
        yield return new WaitForSeconds(2.6f);
        contador = 0;
        anim.SetBool("meia_vida", false);
        anim.SetBool("idle", true);
    }

    private void atirandoresIdle(int valor) {
        if(!umTerremoto) {
            somTerremoto.Play();
            umTerremoto = true;
        }
        FaseManager6.podeCair = true;
        Camera.tremer_bastante = true;
        if(valor == 1) {
            anim.SetBool("atacando", false);
            anim.SetBool("idle_atacando", true);
        } else if(valor == 2) {
            anim.SetBool("atacando2", false);
            anim.SetBool("idle_atacando2", true);
        } else if(valor == 3) {
            anim.SetBool("atacando3", false);
            anim.SetBool("idle_atacando3", true);
        }
        pode_atirar = true;
    }

    void Fire() {
		if(!morto) {
            if(!metade_da_vida && pode_atirar) {
                vida = vida - 2.5f;
                if(valor_alet == 1) {
                    NascerTiro(girador);
                } else if(valor_alet == 2){
                    NascerTiro(monstro_base);
                }
            } else if(metade_da_vida && pode_atirar){
                vida = vida - 5f;
                if(valor_alet == 1) {
                    NascerTiro(girador);
                } else if(valor_alet == 2) {
                    NascerTiro(monstro_base);
                } else if(valor_alet == 3) {
                    NascerTiro(bomba);
                }
            }
        }
    }

    void NascerTiro(GameObject objeto) {
        GameObject cloneBullet = Instantiate(objeto, spawn_tiro.position, spawn_tiro.rotation);
        GameObject cloneBullet2 = Instantiate(objeto, spawn_tiro_2.position, spawn_tiro_2.rotation);
        GameObject cloneBullet3 = Instantiate(objeto, spawn_tiro_3.position, spawn_tiro_3.rotation);
        GameObject cloneBullet4 = Instantiate(objeto, spawn_tiro_4.position, spawn_tiro_4.rotation);
        GameObject cloneBullet5 = Instantiate(objeto, spawn_tiro_5.position, spawn_tiro_5.rotation);

        if(metade_da_vida) {
            GameObject cloneBullet6 = Instantiate(objeto, spawn_tiro_1_meia.position, spawn_tiro_1_meia.rotation);
            GameObject cloneBullet7 = Instantiate(objeto, spawn_tiro_2_meia.position, spawn_tiro_2_meia.rotation);
            GameObject cloneBullet8 = Instantiate(objeto, spawn_tiro_3_meia.position, spawn_tiro_3_meia.rotation);
            GameObject cloneBullet9 = Instantiate(objeto, spawn_tiro_4_meia.position, spawn_tiro_4_meia.rotation);
        }
    }
}
