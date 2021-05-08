﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabecaBase : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawn_Bullet;
    private Animator anim;
    private Rigidbody2D corpo;
    private BoxCollider2D collider;

    private float contador;
    public static float vida_cabeca;
    private bool podeAtirar;

    public static bool morto;
    public static bool renascer;
    private bool renascido;

    public static bool todosMortos;

    private SpriteRenderer sr;

    private float tempoDeTiro;

    // Start is called before the first frame update
    void Start()
    {
        renascido = false;
        tempoDeTiro = 4.5f;
        renascer = false;
        morto = false;
        podeAtirar = true;
        vida_cabeca = 50f;
        contador = 0;
        collider = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(renascer) {
            anim.SetBool("renascer", true);
            sr.color = Color.red;
            tempoDeTiro = 3.4f;
            StartCoroutine("renascerIdle");
        }

        contador += Time.deltaTime;
        if(contador >= tempoDeTiro && podeAtirar) {
            Instantiate(Bullet, Spawn_Bullet.position, Spawn_Bullet.rotation);
            anim.SetBool("atirar", true);
            contador = 0;
            StartCoroutine("posTiro");
        }

        if(vida_cabeca == 40 || vida_cabeca == 30 || vida_cabeca == 20 || vida_cabeca == 10) {
            anim.SetBool("tomou_dano", true);
            podeAtirar = false;
            StartCoroutine("voltarDoDano");
        }

        if(vida_cabeca == 25 && !renascido) {
            morto = true;
            podeAtirar = false;
            anim.SetBool("locaute", true);
        }

        if(vida_cabeca <= 0) {
            FaseManager2.cabeca4_morta = true;
            morto = true;
            podeAtirar = false;
            sr.color = Color.white;
            SuperTiroChefao.modoHard = false;
            if(todosMortos) {
                collider.isTrigger = true;
                anim.SetBool("morreu", true);
                StartCoroutine("morrer");
            } else {
                anim.SetBool("semi_morto", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(!renascer && !morto) {
                vida_cabeca--;
            }
        }else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator voltarDoDano() {
        yield return new WaitForSeconds(1.2f);
        if((vida_cabeca != 40) && (vida_cabeca != 30)) {
            anim.SetBool("tomou_dano", false);
            podeAtirar = true;
        }
    }

    IEnumerator posTiro() {
        if(!renascido) {
            yield return new WaitForSeconds(2.8f);
            anim.SetBool("atirar", false);
        } else {
            yield return new WaitForSeconds(1f);
            anim.SetBool("atirar", false);
        }
    }

    IEnumerator renascerIdle() {
        yield return new WaitForSeconds(5.8f);
        renascido = true;
        podeAtirar = true;
        anim.SetBool("idle_metade", true);
        renascer = false;
        morto = false;
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
