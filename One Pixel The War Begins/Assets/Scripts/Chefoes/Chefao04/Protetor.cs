using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protetor : MonoBehaviour
{
    // Pontos de onde irão sair os tiros
        public Transform[] pontosDeTiro = new Transform[16];

        private SpriteRenderer sr;

        // Variaveis controladoras dos movimentos
        private float potenciaRot;

        // Tiros
        public GameObject tiros;

        private Animator anim;
        private CircleCollider2D collider;

        public static bool pode_atirar;

        public static float tempo_de_tiro;

        public static bool chefao_vermelho;

    void Start()
    {
        chefao_vermelho = false;
        tempo_de_tiro = 1.5f;
        pode_atirar = true;
        potenciaRot = 1f;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        anim.SetBool("nascer", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(MoedaRisada.moeda_ativou == false) {
            collider.isTrigger = false;
            if(!chefao_vermelho) {
                anim.SetBool("nascer", true);
            } else {
                anim.SetBool("meia_vida_nascer", true);
            }
            transform.Rotate(new Vector3(x: 0, y: 0, z: potenciaRot));
            if(Chefao04.contador >= tempo_de_tiro) {
                collider.enabled = true;
                for (int i = 0; i < 16; i++)
                {
                    if(pode_atirar) {
                        Instantiate(tiros, pontosDeTiro[i].position, pontosDeTiro[i].rotation);
                        if(i >= 15) {
                            pode_atirar = false;
                            }
                    }
                }
            }
        } else {
            if(!chefao_vermelho) {
                anim.SetBool("sumir", true);
            } else {
                anim.SetBool("meia_vida_sumir", true);
            }
            StartCoroutine("protetorSumindo");
        }

        if(chefao_vermelho) {
            tempo_de_tiro = 0.92f;
            sr.color = Color.red;
            potenciaRot = 1.25f;
        } 
    }

    IEnumerator protetorSumindo() {
        yield return new WaitForSeconds(0.85f);
        collider.isTrigger = true;
        if(!chefao_vermelho) {
            anim.SetBool("sumir", false);
            anim.SetBool("nascer", false);
        } else {
            anim.SetBool("meia_vida_sumir", false);
            anim.SetBool("meia_vida_nascer", false);
        }
    }
}
