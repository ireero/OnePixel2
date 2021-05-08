using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao032 : MonoBehaviour
{
    public Transform[] pontosIdaCima = new Transform[17];
    public Transform[] pontosIdaBaixo = new Transform[17];
    public Transform[] pontosIdaEsquerda = new Transform[11];
    public Transform[] pontosIdaDireita = new Transform[11];
    
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

    public Transform posicao_inicial;

    public static int vida_chefao = 3;

    void Start()
    {
        contador = 0;
        cont = 4;
        rodar = false;
        anim = GetComponent<Animator>();
        collider_quadrado = GetComponent<BoxCollider2D>();
        collider_redondo = GetComponent<CircleCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 3f) {
            StartCoroutine("transformar");
            if(rodar) {
                transform.Rotate(new Vector3(x: 0, y: 0, z: potenciaRot));
                potenciaRot += 0.005f;
                speed += 0.005f;
            if(corpo.velocity.y == 0f && (cont == 1 || cont == 2 || cont == 3 || cont == 4)) {
                IrParaPosicao(valor_alet, cont);
            }
        }
    }

        if(contador >= 12f) {
            StartCoroutine("reiniciarContador");
            StartCoroutine("habilitarNovamente");
            cont = -1;
            potenciaRot = 0;
            speed = 10f;
            this.gameObject.transform.rotation = posicao_inicial.rotation;
            collider_quadrado.enabled = true;
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, posicao_inicial.position, speed * Time.deltaTime);
        }
    }
    public void IrParaPosicao(int i, int lados) {
        switch(lados) {
            case 1: // cima
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaCima[i].position, speed * Time.deltaTime);
                if(transform.position.x <= pontosIdaCima[i].position.x) {
                    cont = 2;
                    umaVez = false;
                }
                break;
            case 2: // esquerda
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaEsquerda[i].position, speed * Time.deltaTime);
                if(transform.position.x <= pontosIdaEsquerda[i].position.x) {
                    cont = 3;
                    umaVez = false;
                }
                break;  
            case 3: // baixo
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaBaixo[i].position, speed * Time.deltaTime);
                if(transform.position.x >= pontosIdaBaixo[i].position.x) {
                    cont = 4;
                    umaVez = false;
                }
                break;
            case 4: // direita
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaDireita[i].position, speed * Time.deltaTime);
                if(transform.position.x >= pontosIdaDireita[i].position.x) {
                    cont = 1;
                    umaVez = false;
                }
                break;       
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") && !rodar) {
            vida_chefao--;
        }
    }

    IEnumerator transformar() {
        anim.SetBool("transformando", true);
        sortearValor();
        yield return new WaitForSeconds(1f);
        collider_quadrado.enabled = false;
        rodar = true;
    }

    public void sortearValor() {
        if((cont == 1 || cont == 3) && !umaVez) {
            valor_alet = Random.Range(1, 17);
        } else if((cont == 2 || cont == 4) && !umaVez) {
            valor_alet = Random.Range(1, 11);
        }
        umaVez = true;
    }

    IEnumerator reiniciarContador() {
        yield return new WaitForSeconds(3.5f);
        contador = 0;
    }

    IEnumerator habilitarNovamente() {
        yield return new WaitForSeconds(6.5f);
        valor_alet = Random.Range(1, 11);
        cont = 4;
    }
}
