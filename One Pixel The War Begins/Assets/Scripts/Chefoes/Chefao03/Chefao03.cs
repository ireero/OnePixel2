using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao03 : MonoBehaviour
{
    public Transform[] pontosIdaCima = new Transform[17];
    public Transform[] pontosIdaBaixo = new Transform[17];
    public Transform[] pontosIdaEsquerda = new Transform[11];
    public Transform[] pontosIdaDireita = new Transform[11];

    private Animator anim;
    private bool rodar;

    private float contador;

    private BoxCollider2D collider_quadrado;
    private CircleCollider2D collider_redondo;
    private Rigidbody2D corpo;

    private float potenciaRot = 2;

    private bool umaVez, umaVezt;

    private int valor_alet;

    private bool[] chegouPontos = new bool[4];

    private float speed = 15f;

    private Vector3 v;

    private bool cima, baixo, esquerda, direita;
    private int cont;

    void Start()
    {

        cont = 0;
        cima = true; 
        baixo = false; 
        esquerda = false;
        direita = false;
        umaVez = false;
        contador = 0;
        rodar = false;
        anim = GetComponent<Animator>();
        collider_quadrado = GetComponent<BoxCollider2D>();
        collider_redondo = GetComponent<CircleCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        StartCoroutine("primeiraTransformacao");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(rodar) {
            transform.Rotate(new Vector3(x: 0, y: 0, z: potenciaRot));
            potenciaRot += 0.01f;
            if(potenciaRot >= 10) {
                rodar = false;
                contador = 0;
                potenciaRot = 0;
                umaVez = false;
                anim.SetBool("transformando", false);
            }
            if(cima) {
                switch(valor_alet) {
                case 1:
                    IrParaPosicao(0, 1);
                    break;
                case 2:
                    IrParaPosicao(1, 1);
                    break;
                case 3:
                    IrParaPosicao(2, 1);
                    break;
                case 4:
                    IrParaPosicao(3, 1);
                    break;  
                case 5:
                    IrParaPosicao(4, 1);
                    break; 
                case 6:
                    IrParaPosicao(5, 1);
                    break;  
                case 7:
                    IrParaPosicao(6, 1);
                    break; 
                case 8:
                    IrParaPosicao(7, 1);
                    break;
                case 9:
                    IrParaPosicao(8, 1);
                    break;  
                case 10:
                    IrParaPosicao(9, 3);
                    break;
                case 11:
                    IrParaPosicao(10, 1);
                    break;
                case 12:
                    IrParaPosicao(11, 1);
                    break; 
                case 13:
                    IrParaPosicao(12, 1);
                    break; 
                case 14:
                    IrParaPosicao(13, 1);
                    break; 
                case 15:
                    IrParaPosicao(14, 1);
                    break; 
                case 16:
                    IrParaPosicao(15, 1);
                    break;   
                case 17:
                    IrParaPosicao(16, 1);
                    break;                                 
            }
            } else if(esquerda) {
                switch(valor_alet) {
                case 1:
                    IrParaPosicao(0, 2);
                    break;
                case 2:
                    IrParaPosicao(1, 2);
                    break;
                case 3:
                    IrParaPosicao(2, 2);
                    break;
                case 4:
                    IrParaPosicao(3, 2);
                    break;  
                case 5:
                    IrParaPosicao(4, 2);
                    break; 
                case 6:
                    IrParaPosicao(5, 2);
                    break;  
                case 7:
                    IrParaPosicao(6, 2);
                    break; 
                case 8:
                    IrParaPosicao(7, 2);
                    break;
                case 9:
                    IrParaPosicao(8, 2);
                    break;  
                case 10:
                    IrParaPosicao(9, 2);
                    break;
                case 11:
                    IrParaPosicao(10, 2);
                    break;                                
            }
            } else if(baixo) {
                switch(valor_alet) {
                case 1:
                    IrParaPosicao(0, 3);
                    break;
                case 2:
                    IrParaPosicao(1, 3);
                    break;
                case 3:
                    IrParaPosicao(2, 3);
                    break;
                case 4:
                    IrParaPosicao(3, 3);
                    break;  
                case 5:
                    IrParaPosicao(4, 3);
                    break; 
                case 6:
                    IrParaPosicao(5, 3);
                    break;  
                case 7:
                    IrParaPosicao(6, 3);
                    break; 
                case 8:
                    IrParaPosicao(7, 3);
                    break;
                case 9:
                    IrParaPosicao(8, 3);
                    break;  
                case 10:
                    IrParaPosicao(9, 3);
                    break;
                case 11:
                    IrParaPosicao(10, 3);
                    break;
                case 12:
                    IrParaPosicao(11, 3);
                    break; 
                case 13:
                    IrParaPosicao(12, 3);
                    break;
                case 14:
                    IrParaPosicao(13, 3);
                    break;  
                case 15:
                    IrParaPosicao(14, 3);
                    break;
                case 16:
                    IrParaPosicao(15, 3);
                    break; 
                case 17:
                    IrParaPosicao(16, 3);
                    break;                                       
            }
            }
        } 

        if(contador >= 2) {
            sortearValor();
        }

        if(cont == 1) {
            cima = false;
            esquerda = true;
        } else if(cont == 2) {
            esquerda = false;
            baixo = true;
        }
    }
    

    IEnumerator primeiraTransformacao() {
        collider_redondo.enabled = true;
        anim.SetBool("transformando", true);
        collider_quadrado.enabled = false;
        yield return new WaitForSeconds(1.15f);
        corpo.bodyType = RigidbodyType2D.Kinematic;
        rodar = true;
    }

    public void IrParaPosicao(int i, int lados) {
        switch(lados) {
            case 1: // cima
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaCima[i].position, speed * Time.deltaTime);
                StartCoroutine("contar");
                break;
            case 2: // esquerda
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaEsquerda[i].position, speed * Time.deltaTime);
                StartCoroutine("contar");
                break;  
            case 3: // baixo
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaBaixo[i].position, speed * Time.deltaTime);
                StartCoroutine("contar");
                break;
            case 4: // direita
                this.gameObject.transform.position = Vector2.MoveTowards(transform.position, pontosIdaDireita[i].position, speed * Time.deltaTime);
                StartCoroutine("contar");
                break;       
        }
    }

    IEnumerator contar() {
        yield return new WaitForSeconds(1.2f);
        cont++;
    }

    public void sortearValor() {
        if(!umaVez) {
            valor_alet = Random.Range(1, 18);
            umaVez = true;
        }
    }
}
