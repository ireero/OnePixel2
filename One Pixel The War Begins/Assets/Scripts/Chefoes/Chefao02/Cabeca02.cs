using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeca02 : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawn_Bullet;
    public AudioSource somDano;

    private float contador;
    public static float vida_cabeca;

    Cabecas cabeca2 = new Cabecas();
    public static bool locaute;
    public static bool podeRenascer;

    private float cont_dano;

    // Start is called before the first frame update
    void Start()
    {
        cabeca2.renascido = false;
        cabeca2.tomou_dano = false;
        cont_dano = 0;
        locaute = false;
        podeRenascer = false;
        cabeca2.tempoDeTiro = 1f;
        cabeca2.podeAtirar = false;
        cabeca2.vida = 50f;
        contador = 0;
        cabeca2.collider = GetComponent<PolygonCollider2D>();
        cabeca2.corpo = GetComponent<Rigidbody2D>();
        cabeca2.anim = GetComponent<Animator>();
        cabeca2.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(FaseManager2.pode_comecar && !locaute) {
            cabeca2.podeAtirar = true;
        }

        if(podeRenascer) {
           StartCoroutine("renascerIdle");
           cabeca2.Renascer();
        }

        vida_cabeca = cabeca2.vida;

        contador += Time.deltaTime;
        if(contador >= cabeca2.tempoDeTiro && cabeca2.podeAtirar && !locaute) {
            Instantiate(Bullet, Spawn_Bullet.position, Spawn_Bullet.rotation);
            contador = 0;
        }

        cabeca2.TomarDano(cont_dano);
        if(cabeca2.tomou_dano) {
            cont_dano += Time.deltaTime;
            if(cont_dano > 1.2f) {
                cont_dano = 0;
                cabeca2.podeAtirar = true;
                cabeca2.anim.SetBool("tomou_dano", false);
            }
        }

        if(cabeca2.vida == 25 && !locaute && !cabeca2.renascido) {
            cabeca2.podeAtirar = false;
            cabeca2.Locaute();
            locaute = true;
        }

        if(cabeca2.vida <= 0) {
            locaute = true;
            cabeca2.podeAtirar = false;
            cabeca2.Morrer();
            StartCoroutine("morrer");
            FaseManager2.cabeca2_morta = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            somDano.Play();
            if(!podeRenascer && !locaute) {
                cabeca2.vida--;
            }
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator renascerIdle() {
        yield return new WaitForSeconds(2.2f);
        cabeca2.renascido = true;
        cabeca2.tempoDeTiro = 0.75f;
        podeRenascer = false;
        cabeca2.podeAtirar = true;
        cabeca2.anim.SetBool("idle_metade", true);
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(2.05f);
        FaseManager2.cabeca2_morta = true;
        Destroy(this.gameObject);
    }
}
