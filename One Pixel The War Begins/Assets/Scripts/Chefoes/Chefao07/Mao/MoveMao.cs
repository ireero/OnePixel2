using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMao : MonoBehaviour
{
    public SliderJoint2D slider;
    public JointMotor2D aux;
    private Rigidbody2D corpo;
    private float contador;
    public Transform ponto_principal;
    private float speed;
    private bool umaVez;
    private Animator anim;
    private float temp_movendo;

    private BoxCollider2D collider_quadrado;
    private PolygonCollider2D collider;

    public GameObject explosao;

    public int valor_alet;
    private bool sortearUmaVez;

    private AudioSource som_batida;

    public Animator anim_back;

    void Start()
    {
        sortearUmaVez = false;
        valor_alet = 0;
        if(GameManager.sem_dialogos == 1) {
            temp_movendo = 1.8f;
        } else {
            temp_movendo = 2.2f;
        }
        umaVez = false;
        speed = 4f;
        contador = 0;
        aux = slider.motor;
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
        collider_quadrado = GetComponent<BoxCollider2D>();
        som_batida = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        if(!sortearUmaVez) {
            valor_alet = Random.Range(0, 5);
            sortearUmaVez = true;
        }

        if(FaseManager9.tempo_sobreviver <= 25f) {
            speed = 0;
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.8f);
        }

        if(contador >= 53.5f && !umaVez) {
            sortearUmaVez = false;
            corpo.bodyType = RigidbodyType2D.Dynamic;
            slider.enabled = true;
            contador = 0;
            umaVez = true;
            SortearValor();
        }

        if(slider.limitState == JointLimitState2D.LowerLimit) {
            sortearUmaVez = false;
            SortearValor();
        }

        if(slider.limitState == JointLimitState2D.UpperLimit) {
            sortearUmaVez = false;
            SortearValorNegativo();
        }

        if(contador > temp_movendo) {
            anim.SetBool("fechar", true);
            collider_quadrado.enabled = true;
            collider.enabled = false;
            aux.motorSpeed = 0;
            slider.motor = aux;
            if(contador >= (temp_movendo + 0.5f)) {
                slider.enabled = false;
            }
        }

        if(contador >= 52f) {
            this.gameObject.transform.position = Vector2.MoveTowards(transform.position, ponto_principal.position, speed * Time.deltaTime);
            anim.SetBool("fechar", false);
            collider_quadrado.enabled = false;
            collider.enabled = true;
        }
    }

    void SortearValor() {
        switch(valor_alet) {
            case 0:
                aux.motorSpeed = 5;
                slider.motor = aux;
                break;
            case 1:
                aux.motorSpeed = 6;
                slider.motor = aux; 
                break;
            case 2:
                aux.motorSpeed = 7;
                slider.motor = aux;
                break;
            case 3:
                aux.motorSpeed = 8;
                slider.motor = aux;
                break;
            case 4:
                aux.motorSpeed = 9;
                slider.motor = aux; 
                break;              
        }
    }

    void SortearValorNegativo() {
        switch(valor_alet) {
            case 0:
                aux.motorSpeed = -5;
                slider.motor = aux;
                break;
            case 1:
                aux.motorSpeed = -6;
                slider.motor = aux; 
                break;
            case 2:
                aux.motorSpeed = -7;
                slider.motor = aux;
                break;
            case 3:
                aux.motorSpeed = -8;
                slider.motor = aux;
                break;
            case 4:
                aux.motorSpeed = -9;
                slider.motor = aux; 
                break;              
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            som_batida.Play();
            explosao.SetActive(true);
            StartCoroutine("explosaoParar");
            Camera.tremer_chao = true;
            anim_back.SetBool("tremer_chao", true);
            umaVez = false;
            corpo.bodyType = RigidbodyType2D.Kinematic;
            contador = 50f;
        }
    }

    IEnumerator explosaoParar() {
        yield return new WaitForSeconds(0.65f);
        explosao.SetActive(false);
    }
}
