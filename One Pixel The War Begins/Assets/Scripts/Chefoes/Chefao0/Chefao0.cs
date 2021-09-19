using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chefao0 : MonoBehaviour
{
    private BoxCollider2D collider_gosma;
    private Rigidbody2D corpo;
    private float velocidade;
    private Animator anim;
    private float forca_pulo;
    private Transform target;
    private bool lookingRight;
    public Animator anim_back;
    private AudioSource som_batida;
    public AudioSource som_pulo;
    public AudioSource som_dano;
    public AudioSource som_explod;
    public static int vida_boss0;
    private bool tomandoDano;
    public GameObject[] bichos;
    public Transform[] spawns;
    public GameObject vida_chefao;
    public Image BarraDeVida;
    private float vida_maxima = 40f;

    void Start()
    {
        tomandoDano = false;
        vida_boss0 = 40;
        lookingRight = false;
        collider_gosma = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocidade = 0.5f;
        forca_pulo = 500f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        som_batida = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(vida_boss0 >= 0) {
            VidaBoss();
        }

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
            Camera.tremer_chao = true;
            anim_back.SetBool("tremer_chao", true);
            anim.SetBool("pular", false);
        } else if(other.gameObject.CompareTag("bullet")) {
            if(!tomandoDano) {
                vida_boss0--;
            }
            if(vida_boss0 == 30 || vida_boss0 == 20 || vida_boss0 == 10) {
                som_dano.Play();
                velocidade = 0;
                tomandoDano = true;
                anim.SetBool("dano", true);
                StartCoroutine("voltarDano");
            } else if(vida_boss0 <= 0) {
                anim.SetBool("morrer", true);
                corpo.bodyType = RigidbodyType2D.Static;    
                StartCoroutine("morrerCara");
            }
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
        if(!tomandoDano) {
            som_pulo.Play();
            corpo.AddForce(new Vector2(0, forca_pulo));
        }
    }

    public void Cair() {
        anim.SetBool("pular", true);
    }

    public void VidaBoss() {
        BarraDeVida.fillAmount = Chefao0.vida_boss0 / vida_maxima;
    }

    public void Filhotes() {

        som_explod.Play();
        FaseManager0.horaDePassar++;

        bichos[0].transform.position = spawns[0].position;
        bichos[0].SetActive(true);

        bichos[1].transform.position = spawns[1].position;
        bichos[1].SetActive(true);

        bichos[2].transform.position = spawns[2].position;
        bichos[2].SetActive(true);

        bichos[3].transform.position = spawns[3].position;
        bichos[3].SetActive(true);
    }

    IEnumerator voltarDano() {
        yield return new WaitForSeconds(2f);
        som_dano.Stop();
        velocidade = 0.5f;
        anim.SetBool("dano", false);
        anim.SetBool("pular", true);
        tomandoDano = false;
    }

    IEnumerator morrerCara() {
        yield return new WaitForSeconds(0.9f);
        Destroy(vida_chefao);
        Destroy(this.gameObject);
    }
}
