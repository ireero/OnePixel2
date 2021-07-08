using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao06 : MonoBehaviour
{
    private Animator anim;
    public float contador;
    public static bool atacando;
    private bool umaVez;
    private bool umaVez2;
    public static bool camuflado_ja;
    public static int vida_restante;
    public static int dano_tomado;

    void Start()
    {
        dano_tomado = 0;
        vida_restante = 300;
        camuflado_ja = false;
        umaVez = false;
        umaVez2 = false;
        atacando = false;
        contador = 0;
        anim = GetComponent<Animator>();
        StartCoroutine("idleAposNascer");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 6f && !camuflado_ja) {
            anim.SetBool("atacar", true);
            anim.SetBool("idle", false);
            StartCoroutine("horaDoAtaque");
        }

        if(atacando && !umaVez && contador>= 10f) {
            camuflado_ja = true;
            umaVez = true;
        }

        if(vida_restante <= 0) {
            Destroy(this.gameObject);
        }

        if(dano_tomado >= 50) {
            atacando = false;
            camuflado_ja = false;
            contador = -5f;
            dano_tomado = 0;
            anim.SetBool("tomou_dano", true);
            StartCoroutine("voltarDoDano");
        }

        if(vida_restante <= 150 && !umaVez2) {
            anim.SetBool("meia_vida", true);
            umaVez2 = true;
            StartCoroutine("idleMeiaVida");
        }
    }

    IEnumerator idleAposNascer() {
        yield return new WaitForSeconds(1.5f);
        Camera.tremer_bastante = false;
        anim.SetBool("idle", true);
    }

    IEnumerator horaDoAtaque() {
        yield return new WaitForSeconds(5f);
        umaVez = false;
        atacando = true;
        anim.SetBool("atacar", false);
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(3.5f);
        anim.SetBool("idle", true);
        anim.SetBool("tomou_dano", false);
    }

    IEnumerator idleMeiaVida() {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("meia_vida", false);
    }
}
