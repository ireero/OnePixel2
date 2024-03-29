﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTiroChefao : MonoBehaviour
{

    private float speed = -1.75f;
    private float timeDestroy;
    private Animator anim;
    private BoxCollider2D collider_tirao;
    private float contador;
    private int vida = 6;
    private SpriteRenderer sr;

    public static bool modoHard;
    // Start is called before the first frame update
    void Start()
    {   
        contador = 0;
        anim = GetComponent<Animator>();
        timeDestroy = 10f;
        Destroy(gameObject, timeDestroy);
        collider_tirao = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 1.2f) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if(modoHard) {
            speed = -2.5f;
            sr.color = Color.red;
        }

        if(vida <= 0 || Portal.atira_ae_po == 3) {
            Morrer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir")||other.gameObject.CompareTag("chao")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Player")) {
            Morrer();
        } else if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("plataforma")) {
            vida--;
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("super_bullet_inimiga")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(1.65f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        anim.SetBool("morreu", true);
        speed = 0;
        collider_tirao.isTrigger = true;
        StartCoroutine("morre");
    }
}
