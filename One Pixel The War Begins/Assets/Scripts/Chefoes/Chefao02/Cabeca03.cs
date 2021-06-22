using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeca03 : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawn_Bullet;

    private float contador;
    public static float vida_cabeca;

    Cabecas cabeca3 = new Cabecas();
    public static bool locaute;
    public static bool podeRenascer;

    private float cont_dano;

    // Start is called before the first frame update
    void Start()
    {
        cabeca3.renascido = false;
        cabeca3.tomou_dano = false;
        cont_dano = 0;
        locaute = false;
        podeRenascer = false;
        cabeca3.tempoDeTiro = 1f;
        cabeca3.podeAtirar = false;
        cabeca3.vida = 50f;
        contador = 0;
        cabeca3.collider = GetComponent<PolygonCollider2D>();
        cabeca3.corpo = GetComponent<Rigidbody2D>();
        cabeca3.anim = GetComponent<Animator>();
        cabeca3.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(FaseManager2.pode_comecar && !locaute) {
            cabeca3.podeAtirar = true;
        }

        if(podeRenascer) {
           StartCoroutine("renascerIdle");
           cabeca3.Renascer();
        }

        vida_cabeca = cabeca3.vida;

        contador += Time.deltaTime;
        if(contador >= cabeca3.tempoDeTiro && cabeca3.podeAtirar && !locaute) {
            Instantiate(Bullet, Spawn_Bullet.position, Spawn_Bullet.rotation);
            contador = 0;
        }

        cabeca3.TomarDano(cont_dano);
        if(cabeca3.tomou_dano) {
            cont_dano += Time.deltaTime;
            if(cont_dano > 1.2f) {
                cont_dano = 0;
                cabeca3.podeAtirar = true;
                cabeca3.anim.SetBool("tomou_dano", false);
            }
        }

        if(cabeca3.vida == 25 && !locaute && !cabeca3.renascido) {
            cabeca3.Locaute();
            locaute = true;
        }

        if(cabeca3.vida <= 0) {
            cabeca3.podeAtirar = false;
            cabeca3.Morrer();
            StartCoroutine("morrer");
            FaseManager2.cabeca3_morta = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(!podeRenascer && !locaute) {
                cabeca3.vida--;
            }
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator renascerIdle() {
        yield return new WaitForSeconds(5.5f);
        cabeca3.renascido = true;
        cabeca3.tempoDeTiro = 0.75f;
        podeRenascer = false;
        cabeca3.podeAtirar = true;
        cabeca3.anim.SetBool("idle_metade", true);
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(4.1f);
        FaseManager2.cabeca3_morta = true;
        Destroy(this.gameObject);
    }
}
