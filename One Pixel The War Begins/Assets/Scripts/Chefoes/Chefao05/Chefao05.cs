using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao05 : MonoBehaviour
{
    private float contador;
    private BoxCollider2D collider;
    private Rigidbody2D corpo;
    private Animator anim;

    public Transform spawn_tiro;
    public Transform spawn_tiro_2;
    public Transform spawn_tiro_3;
    public Transform spawn_tiro_4;
    public Transform spawn_tiro_5;

    public GameObject bomba;

    private bool pode_atirar;
    private int tirosDados;

    private float nextFire;
    private bool morto;

    public static float vida;
    private int contagem_danos;

    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        nextFire += Time.deltaTime;

        if(contagem_danos >= 50) {
            contagem_danos = 0;
            anim.SetBool("tomou_dano", true);
            StartCoroutine("voltarDoDano");
        } 

        if(contador >= 7f && !pode_atirar) {
            contador = -20f;
            anim.SetBool("idle", false);
            anim.SetBool("atacando", true);
            StartCoroutine("atirandoIdle");
        }

        if(contador >= -5f && contador < 0) {
            anim.SetBool("idle_atacando", false);
            StartCoroutine("voltarDoAtaque");
            pode_atirar = false;
        }

        if(nextFire >= 1.1f && pode_atirar) {
            Fire();
            nextFire = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            vida--;
            contagem_danos++;
        }
    }

    IEnumerator atirandoIdle() {
        yield return new WaitForSeconds(3.35f);
        anim.SetBool("atacando", false);
        anim.SetBool("idle_atacando", true);
        pode_atirar = true;
    }

    IEnumerator voltarDoAtaque() {
        yield return new WaitForSeconds(2.25f);
        anim.SetBool("idle", true);
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(1.7f);
        anim.SetBool("tomou_dano", false);
    }

    void Fire() {
		if(!morto) {
            vida = vida - 5f;
            GameObject cloneBullet = Instantiate(bomba, spawn_tiro.position, spawn_tiro.rotation);
            GameObject cloneBullet2 = Instantiate(bomba, spawn_tiro_2.position, spawn_tiro_2.rotation);
            GameObject cloneBullet3 = Instantiate(bomba, spawn_tiro_3.position, spawn_tiro_3.rotation);
            GameObject cloneBullet4 = Instantiate(bomba, spawn_tiro_4.position, spawn_tiro_4.rotation);
            GameObject cloneBullet5 = Instantiate(bomba, spawn_tiro_5.position, spawn_tiro_5.rotation);
        }
    }
}
