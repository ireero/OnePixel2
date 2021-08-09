using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPreto : MonoBehaviour
{
    public float contador;
    private Animator anim;
    private int vida;
    private PolygonCollider2D collider;
    private Rigidbody2D corpo;

    private float forca_pulo;
    public static bool atirarUmaVez;

    public GameObject bola_fogo;
    public Transform spawn_tiro;

    public static int tirosDados;
    public static bool AtirouJa;

    private bool podeAtirar;
    private float delayTiro;

    private float i;

    public Transform spawn_tiro_bala;

    public GameObject bala;

    public float contador2;

    private bool atirar_normal;
    private bool pularUmaVez;

    public Transform ponto_direita, ponto_esquerda;

    private float speed;

    private bool estaNaDireita;

    private SpriteRenderer sr;

    void Start()
    {
        estaNaDireita = true;
        speed = 5f;
        pularUmaVez = false;
        atirar_normal = false;
        contador2 = 0;
        AtirouJa = false;
        i = 0;
        delayTiro = 0.4f;
        podeAtirar = false;
        tirosDados = 0;
        atirarUmaVez = false;
        forca_pulo = 410f;
        contador = 0;
        vida = 1000;
        anim = GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 5f && !atirarUmaVez) {
            AtirouJa = false;
            anim.SetBool("atirar_cima", true);
            anim.SetBool("idle", false);
            corpo.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine("Atirar");
        }

        if(contador >= 25f && !pularUmaVez) {
            anim.SetBool("rugindo", false);
            anim.SetBool("andando", true);
            atirar_normal = false;
            corpo.constraints = RigidbodyConstraints2D.FreezeRotation;
            if(estaNaDireita) {
                transform.position = Vector2.MoveTowards(transform.position, ponto_esquerda.position, speed * Time.deltaTime);
                if(transform.position.x <= ponto_esquerda.position.x) {
                    anim.SetBool("pulando", true);
                    anim.SetBool("andando", false);
                    sr.flipX = !sr.flipX;
                    pularUmaVez = true;
                    corpo.AddForce(new Vector2(0, forca_pulo));
                    estaNaDireita = false;
                }
            } else {
                transform.position = Vector2.MoveTowards(transform.position, ponto_direita.position, speed * Time.deltaTime);
                if(transform.position.x >= ponto_direita.position.x) {
                    anim.SetBool("pulando", true);
                    anim.SetBool("andando", false);
                    sr.flipX = !sr.flipX;
                    pularUmaVez = true;
                    corpo.AddForce(new Vector2(0, forca_pulo));
                    estaNaDireita = true;
                }
            }
        }

        if(podeAtirar) {
            i += Time.deltaTime;
            if(i > delayTiro && tirosDados <= 19) {
                Instantiate(bola_fogo, spawn_tiro.position, spawn_tiro.rotation);
                tirosDados++;
                i = 0;
            }
        }

        if(tirosDados >= 20) {
            AtirouJa = true;
            atirar_normal = true;
            podeAtirar = false;
            anim.SetBool("idle", true);
            anim.SetBool("atirar_cima", false);
            contador = 0;
            pularUmaVez = false;
        }

        if(AtirouJa && atirar_normal) {
            contador2 += Time.deltaTime;
            if(contador >= 3.5f) {
                anim.SetBool("rugindo", true);
                anim.SetBool("idle", false);
                StartCoroutine("Tirao");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            Camera.tremer_chao = true;
            anim.SetBool("pulando", false);
        }
    }

    IEnumerator Atirar() {
        yield return new WaitForSeconds(1f);
        podeAtirar = true;
        atirarUmaVez = true;
    }

    IEnumerator Tirao() {
        yield return new WaitForSeconds(0.8f);
        if(contador2 >= 2.5f) {
                Instantiate(bala, spawn_tiro_bala.position, spawn_tiro_bala.rotation);
                contador2 = 0;
            }
    }

}
