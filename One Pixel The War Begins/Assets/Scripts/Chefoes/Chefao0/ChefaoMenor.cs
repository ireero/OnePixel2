using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefaoMenor : MonoBehaviour
{
    private Rigidbody2D corpo;
    private float velocidade;
    private Animator anim;
    private float forca_pulo;
    private Transform target;
    private bool lookingRight;
    private AudioSource som_batida;
    public AudioSource som_pulo;
    public AudioSource som_explodi;
    private int vida_mob;
    private float valor;
    private float valor2;

    void Start()
    {
        vida_mob = 10;
        lookingRight = false;
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        valor = Random.Range(-500, 501);
        valor2 = Random.Range(-100, 300);
        velocidade = Random.Range(0.5f, 1.5f);
        forca_pulo = Random.Range(150, 300);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        som_batida = GetComponent<AudioSource>();
        corpo.AddForce(new Vector2(valor, valor2));
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < target.transform.position.x) {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
            if(!lookingRight) {
                Flip();
            }
        } else {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
            if(lookingRight) {
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            som_batida.Play();
            anim.SetBool("pular", false);
        } else if(other.gameObject.CompareTag("bullet")) {
            vida_mob--;
            if(vida_mob <= 0) {
                velocidade = 0;
                corpo.bodyType = RigidbodyType2D.Static;  
                anim.SetBool("morrer", true);
                StartCoroutine("morrerCara");
            }
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else if(other.gameObject.CompareTag("fora")) {
            FaseManager0.horaDePassar++;
            Destroy(this.gameObject);
        }
    }

    void Flip(){
      lookingRight = !lookingRight;
      Vector3 myScale = transform.localScale;
      myScale.x *= -1;
      transform.localScale = myScale;
   }

    public void Pular() {
        som_pulo.Play();
        corpo.AddForce(new Vector2(0, forca_pulo));
    }

    public void Filhotes() {
        som_explodi.Play();
        FaseManager0.horaDePassar++;
    }

    public void Cair() {
        anim.SetBool("pular", true);
    }

    IEnumerator morrerCara() {
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }
}
