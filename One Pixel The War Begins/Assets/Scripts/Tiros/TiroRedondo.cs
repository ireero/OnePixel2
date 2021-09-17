using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroRedondo : MonoBehaviour
{

    private float speed = -2.2f;
    private float timeDestroy;
    private Animator anim;
    private CircleCollider2D collider_redondo;

    public GameObject tiro_normal;
    public Transform spawn_cima;
    public Transform spawn_esquerda;
    public Transform spawn_direita;
    public Transform spawn_baixo;

    public Transform spawn_cima_diagonal_direita;
    public Transform spawn_cima_diagonal_esquerda;
    public Transform spawn_baixo_diagonal_direita;
    public Transform spawn_baixo_diagonal_esquerda;

    public static bool modoHardRedondo;

    private bool umaVez;

    private AudioSource som_partindo;
    // Start is called before the first frame update
    void Start()
    {   
        umaVez = false;
        anim = GetComponent<Animator>();
        timeDestroy = 5f;
        Destroy(gameObject, timeDestroy);
        collider_redondo = GetComponent<CircleCollider2D>();
        if(modoHardRedondo) {
            anim.SetBool("meia_vida", true);
        }
        som_partindo = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if(modoHardRedondo && !umaVez) {
            speed = -2.8f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir") || other.gameObject.CompareTag("chao")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("plataforma")) {
            som_partindo.Play();
            Morrer();
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("super_bullet_inimiga") || 
            other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("moeda_rir")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        anim.SetBool("sumir", true);
        if(!umaVez) {
            Instantiate(tiro_normal, spawn_cima.position, spawn_cima.rotation);
            Instantiate(tiro_normal, spawn_esquerda.position, spawn_esquerda.rotation);
            Instantiate(tiro_normal, spawn_direita.position, spawn_direita.rotation);
            Instantiate(tiro_normal, spawn_baixo.position, spawn_baixo.rotation);
            if(modoHardRedondo) {
                Instantiate(tiro_normal, spawn_baixo_diagonal_direita.position, spawn_baixo_diagonal_direita.rotation);
                Instantiate(tiro_normal, spawn_baixo_diagonal_esquerda.position, spawn_baixo_diagonal_esquerda.rotation);
                Instantiate(tiro_normal, spawn_cima_diagonal_direita.position, spawn_cima_diagonal_direita.rotation);
                Instantiate(tiro_normal, spawn_cima_diagonal_esquerda.position, spawn_cima_diagonal_esquerda.rotation);
            }
            umaVez = true;
        }
        speed = 0;
        collider_redondo.isTrigger = true;
        StartCoroutine("morre");
    }
}
