using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeca03 : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawn_Bullet;
    private Animator anim;
    private Rigidbody2D corpo;
    private PolygonCollider2D collider;

    private float contador;
    public static float vida_cabeca;
    private bool podeAtirar;

    public static bool morto;
    public static bool renascer;

    private SpriteRenderer sr;

    private float tempoDeTiro;

    private bool renascido;

    // Start is called before the first frame update
    void Start()
    {
        renascido = false;
        tempoDeTiro = 1.5f;
        renascer = false;
        morto = false;
        podeAtirar = true;
        vida_cabeca = 50f;
        contador = 0;
        collider = GetComponent<PolygonCollider2D>();
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
            tempoDeTiro = 1.2f;
            StartCoroutine("renascerIdle");
        }

        contador += Time.deltaTime;
        if(contador >= tempoDeTiro && podeAtirar) {
            Instantiate(Bullet, Spawn_Bullet.position, Spawn_Bullet.rotation);
            contador = 0;
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
            collider.isTrigger = true;
            podeAtirar = false;
            sr.color = Color.white;
            anim.SetBool("morreu", true);
            StartCoroutine("morrer");
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

    IEnumerator renascerIdle() {
        yield return new WaitForSeconds(4.5f);
        renascido = true;
        podeAtirar = true;
        anim.SetBool("idle_metade", true);
        renascer = false;
        morto = false;
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(4.1f);
        FaseManager2.cabeca3_morta = true;
        Destroy(this.gameObject);
    }
}
