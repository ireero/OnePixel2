using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao03 : MonoBehaviour
{
    public Transform[] pontosIdaCima = new Transform[17];
    public Transform[] pontosIdaBaixo = new Transform[17];
    public Transform[] pontosIdaEsquerda = new Transform[11];
    public Transform[] pontosIdaDireita = new Transform[11];

    public GameObject particulas_de_cura;
    
    private Animator anim;

    private BoxCollider2D collider_quadrado;
    private CircleCollider2D collider_redondo;
    private Rigidbody2D corpo;

    private float potenciaRot = 6f;
    public int valor_alet;

    private float speed = 8f;
    private float contador;

    private bool rodar;

    public int cont;

    private bool umaVez = false;
    private bool umaVez2 = false;

    public Transform posicao_inicial;

    public static int vida_chefao = 3;

    private bool meia_vida = false;

    private SpriteRenderer sr;

    private bool morreu;
    public bool podeTomarDano;

    public AudioSource som_batida;

    private float mais_speed;

    void Start()
    {
        mais_speed = 0.005f;
        podeTomarDano = true;
        morreu = false;
        contador = 0;
        cont = 4;
        rodar = false;
        anim = GetComponent<Animator>();
        collider_quadrado = GetComponent<BoxCollider2D>();
        collider_redondo = GetComponent<CircleCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(FaseManager3.pode_comecar_3 == false) {
            contador = 0;
        }

        contador += Time.deltaTime;
        if(contador >= 4f && contador < 8 && (!morreu)) {
            particulas_de_cura.SetActive(false);
            anim.SetBool("transformando", true);
            podeTomarDano = false;
            StartCoroutine("transformar");
        }
            
            if(rodar && !morreu) {
                transform.Rotate(new Vector3(x: 0, y: 0, z: potenciaRot));
                potenciaRot += 0.005f;
                speed += mais_speed;
                if(corpo.velocity.y == 0f && (cont == 1 || cont == 2 || cont == 3 || cont == 4)) {
                    IrParaPosicao(valor_alet, cont);
                }
    }

    sortearValor();

        if(contador >= 10.5f && contador < 13f) {
            umaVez2 = false;
            anim.SetBool("sobrecarregando", true);
        }

        if(contador >= 13f) {
            anim.SetBool("recarregando", true);
            podeTomarDano = false;
            rodar = false;
            if(!umaVez2) {
                StartCoroutine("habilitarNovamente");
                umaVez2 = true;
            }
            cont = 4;
            potenciaRot = 0f;
            if(meia_vida) {
                mais_speed = 0.006f;
                speed = 13f;
            } else {
                speed = 8f;
            }
            this.gameObject.transform.rotation = posicao_inicial.rotation;
            collider_quadrado.enabled = true;
            collider_redondo.enabled = false;
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, posicao_inicial.position, speed * Time.deltaTime);
        }

        if(vida_chefao == 0 && !meia_vida) {
            FaseManager3.contagem_falas_3 = 9;
            podeTomarDano = false;
            sr.color = Color.red;
            anim.SetBool("meia_vida", true);
            anim.SetBool("tomou_dano", false);
            rodar = false;
            contador = 1f;
            meia_vida = true;
            StartCoroutine("metadeDaVida");
        }
    }
    public void IrParaPosicao(int i, int lados) {
        switch(lados) {
            case 1: // cima
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaCima[i].position, speed * Time.deltaTime);
                if(transform.position.x <= pontosIdaCima[i].position.x) {
                    som_batida.Play();
                    Camera.tremer_chao = true;
                    cont = 2;
                    umaVez = false;
                }
                break;
            case 2: // esquerda
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaEsquerda[i].position, speed * Time.deltaTime);
                if(transform.position.x <= pontosIdaEsquerda[i].position.x) {
                    som_batida.Play();
                    cont = 3;
                    umaVez = false;
                }
                break;  
            case 3: // baixo
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaBaixo[i].position, speed * Time.deltaTime);
                if(transform.position.x >= pontosIdaBaixo[i].position.x) {
                    som_batida.Play();
                    Camera.tremer_chao = true;
                    cont = 4;
                    umaVez = false;
                }
                break;
            case 4: // direita
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaDireita[i].position, speed * Time.deltaTime);
                if(transform.position.x >= pontosIdaDireita[i].position.x) {
                    som_batida.Play();
                    cont = 1;
                    umaVez = false;
                }
                break;       
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") && podeTomarDano) {
            podeTomarDano = false;
            vida_chefao--;
            if(vida_chefao <= -3) {
                corpo.bodyType = RigidbodyType2D.Static;
                collider_quadrado.isTrigger = true;
                collider_redondo.isTrigger = true;
                Destroy(particulas_de_cura);
                morreu = true;
                anim.SetBool("morreu", true);
                StartCoroutine("morrer");
            } else {
                anim.SetBool("transformando", false);
                anim.SetBool("tomou_dano", true);
            }
        }
    }

    IEnumerator transformar() {
        anim.SetBool("tomou_dano", false);
        yield return new WaitForSeconds(1.2f);
        potenciaRot = 6f;
        rodar = true;
        collider_quadrado.enabled = false;
        collider_redondo.enabled = true;
    }

    public void sortearValor() {
        if((cont == 1 || cont == 3) && !umaVez) {
            valor_alet = Random.Range(0, 17);
        } else if((cont == 2 || cont == 4) && !umaVez) {
            valor_alet = Random.Range(0, 11);
        }
        umaVez = true;
    }

    IEnumerator habilitarNovamente() {
        particulas_de_cura.SetActive(true);
        yield return new WaitForSeconds(0.8f);
            anim.SetBool("sobrecarregando", false);
            valor_alet = Random.Range(0, 11);
        yield return new WaitForSeconds(2.6f);
            anim.SetBool("recarregando", false);
            anim.SetBool("transformando", false);
            contador = 3f;
            cont = 4;
            podeTomarDano = true;
    }

    IEnumerator metadeDaVida() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("meia_vida", false);
    }

    IEnumerator morrer() {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
