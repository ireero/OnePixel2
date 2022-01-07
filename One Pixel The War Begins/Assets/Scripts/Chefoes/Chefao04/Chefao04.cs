using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao04 : MonoBehaviour
{

    // Variaveis relacionadas a componentes do Chefão 
    private SpriteRenderer sr;
    private BoxCollider2D collider_chefao4;
    private Rigidbody2D corpo;
    private Animator anim;

    // variaveis relacionadas a valores do Chefão
    public static float vida_chefao;
    private int contagem_danos;

    // Variaveis de controle
    public static float contador;
    public static float contador_voltar;

    // Variaveis que controlam o Protetor
    public GameObject protetor;

    public static int tirosDados;

    private bool umaVez;
    private bool umaVezMeiaVida;

    private float valor_para_voltar;

    public GameObject escada;

    void Start()
    {
        umaVezMeiaVida = false;
        umaVez = false;
        PlayerControle.podeAtirar = true;
        tirosDados = 0;
        contador = 0;
        contador_voltar = 0;
        valor_para_voltar = 7f;
        vida_chefao = 80f;
        sr = GetComponent<SpriteRenderer>();
        collider_chefao4 = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FaseManager5.pode_comecar_5 == false) {
            contador = 0;
        }

        if(MoedaRisada.moeda_ativou == false && FaseManager5.pode_comecar_5 == true) {
            umaVez = false;
            contador += Time.deltaTime;
            anim.SetBool("atacando", true);
            anim.SetBool("idle", false);
            anim.SetBool("tomou_dano", false);
            anim.SetBool("rir", false);
            collider_chefao4.isTrigger = false;

            if(Protetor.pode_atirar == false) {
                tirosDados++;
                Protetor.pode_atirar = true;
                contador = 0;
            }
        } else if(MoedaRisada.moeda_ativou == true && FaseManager5.pode_comecar_5 == true) {
            contador_voltar += Time.deltaTime;
            if(!umaVez) {
                anim.SetBool("atacando", false);
                anim.SetBool("rir", true);
                umaVez = true;
            }

            if(contador_voltar >= valor_para_voltar) {
                tirosDados = 0;
                MoedaRisada.moeda_ativou = false;
                contador_voltar = 0;
            }
        }

        if(contagem_danos == 10) {
            contagem_danos = 0;
            anim.SetBool("tomou_dano", true);
            anim.SetBool("rir", false);
            anim.SetBool("idle", false);
            StartCoroutine("voltarDoDano");
        }

        if(vida_chefao <= 0) {
            corpo.gravityScale += 0.1f;
            corpo.bodyType = RigidbodyType2D.Dynamic;
            Destroy(protetor);
            anim.SetBool("morreu", true);
            sr.color = Color.white;
        } else if(vida_chefao <= 40 && vida_chefao > 0) {
            FaseManager5.valor_tiros_dados = 12;
            Protetor.chefao_vermelho = true;
            sr.color = Color.red;
            valor_para_voltar = 6f;
            if(vida_chefao == 40 && !umaVezMeiaVida) {
                if(GameManager.sem_dialogos == 0) {
                    FaseManager5.contagem_falas_5 = 8;
                }
                umaVezMeiaVida = true;
                anim.SetBool("meia_vida", true);
                StartCoroutine("meiaVida");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            vida_chefao--;
            contagem_danos++;
        }else if(other.gameObject.CompareTag("chao")) {
        } else if(other.gameObject.CompareTag("fora")) {
            GameManager.Instance.SalvarSit(2, "Fase5");
            Destroy(this.gameObject);
        }
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("tomou_dano", false);
        anim.SetBool("idle", true);
    }

    IEnumerator meiaVida() {
        yield return new WaitForSeconds(0.65f);
        anim.SetBool("idle", true);
        anim.SetBool("meia_vida", false);
    }

    public void morrer() {
        escada.SetActive(true);
        Destroy(this.gameObject);
    }
}
