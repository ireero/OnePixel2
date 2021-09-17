using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeca01 : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject bala_redonda;
    public Transform Spawn_Bullet;

    public AudioSource somDano;

    private float contador;
    public static float vida_cabeca;

    Cabecas cabeca1 = new Cabecas();
    public static bool locaute;
    public static bool podeRenascer;

    private float cont_dano;

    // Start is called before the first frame update
    void Start()
    {
        cabeca1.renascido = false;
        cabeca1.tomou_dano = false;
        cont_dano = 0;
        locaute = false;
        podeRenascer = false;
        cabeca1.tempoDeTiro = 1.5f;
        cabeca1.podeAtirar = false;
        cabeca1.vida = 50f;
        contador = 0;
        cabeca1.corpo = GetComponent<Rigidbody2D>();
        cabeca1.anim = GetComponent<Animator>();
        cabeca1.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        cabeca1.valor_aleatorio_cara = Random.Range(0, 4);

        if(FaseManager2.pode_comecar && !locaute) {
            cabeca1.podeAtirar = true;
        }

        if(podeRenascer) {
           StartCoroutine("renascerIdle");
           cabeca1.Renascer();
        }

        vida_cabeca = cabeca1.vida;

        contador += Time.deltaTime;
        if(contador >= cabeca1.tempoDeTiro && cabeca1.podeAtirar && !locaute) {
            if(cabeca1.valor_aleatorio_cara == 3) {
                Instantiate(bala_redonda, Spawn_Bullet.position, Spawn_Bullet.rotation);
            } else {
                Instantiate(Bullet, Spawn_Bullet.position, Spawn_Bullet.rotation);
            }
            contador = 0;
        }

        cabeca1.TomarDano(cont_dano);
        if(cabeca1.tomou_dano) {
            cont_dano += Time.deltaTime;
            if(cont_dano > 1.2f) {
                cont_dano = 0;
                cabeca1.podeAtirar = true;
                cabeca1.anim.SetBool("tomou_dano", false);
            }
        }

        if(cabeca1.vida == 25 && !locaute && !cabeca1.renascido) {
            cabeca1.Locaute();
            cabeca1.podeAtirar = false;
            locaute = true;
        }

        if(cabeca1.vida <= 0) {
            locaute = true;
            cabeca1.podeAtirar = false;
            cabeca1.Morrer();
            StartCoroutine("morrer");
            FaseManager2.cabeca1_morta = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            somDano.Play();
            if(!podeRenascer && !locaute) {
                cabeca1.vida--;
            }
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator renascerIdle() {
        yield return new WaitForSeconds(2.2f);
        cabeca1.renascido = true;
        cabeca1.tempoDeTiro = 1f;
        podeRenascer = false;
        cabeca1.podeAtirar = true;
        cabeca1.anim.SetBool("idle_metade", true);
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(2f);
        FaseManager2.cabeca1_morta = true;
        Destroy(this.gameObject);
    }
}
