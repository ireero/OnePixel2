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
    public static bool meia_vida;
    public static bool ta_mortin;
    private AudioSource som_dano;
    public AudioSource terremoto;

    public GameObject escada;

    private bool umTerremoto;

    void Start()
    {
        umTerremoto = false;
        meia_vida = false;
        ta_mortin = false;
        dano_tomado = 0;
        vida_restante = 300;
        camuflado_ja = false;
        umaVez = false;
        umaVez2 = false;
        atacando = false;
        contador = 0;
        anim = GetComponent<Animator>();
        som_dano = GetComponent<AudioSource>();
        StartCoroutine("idleAposNascer");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 5f && !camuflado_ja) {
            Camera.tremer_bastante = true;
            if(!umTerremoto) {
                terremoto.Play();
                umTerremoto = true;
            }
            anim.SetBool("atacar", true);
            anim.SetBool("idle", false);
            StartCoroutine("horaDoAtaque");
        }

        if(atacando && !umaVez && contador>= 10f) {
            camuflado_ja = true;
            umaVez = true;
        }

        if(vida_restante <= 0) {
            meia_vida = false;
            ta_mortin = true;
            anim.SetBool("morreu", true);
            StartCoroutine("morrer");
        }

        if(dano_tomado >= 50) {
            som_dano.Play();
            atacando = false;
            camuflado_ja = false;
            contador = -5f;
            dano_tomado = 0;
            anim.SetBool("tomou_dano", true);
            StartCoroutine("voltarDoDano");
        }

        if(vida_restante <= 150 && !umaVez2) {
            meia_vida = true;
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
        umTerremoto = false;
        terremoto.Stop();
        Camera.tremer_bastante = false;
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

    IEnumerator morrer() {
        yield return new WaitForSeconds(2f);
        escada.SetActive(true);
        Destroy(this.gameObject);
    }
}
