using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonstroOlho : MonoBehaviour
{   
    private CircleCollider2D collider_olho;
    private Animator anim;
    private Transform target;
    private float speed;
    private Rigidbody2D corpo;
    private bool achou;
    private bool morreu;
    private float contador;
    public Transform spawn_tiro;
    public GameObject tiro;
    private float cont_tiro;
    private bool pode_meter_bala;
    private float tempo_pra_morrer;
    private AudioSource som_morte;
    private bool umaVez;

    void Start()
    {
        umaVez = false;
        tempo_pra_morrer = 12f;
        pode_meter_bala = false;
        cont_tiro = 0;
        contador = 0;
        morreu = false;
        achou = false;
        if(SceneManager.GetActiveScene().name == "Fase10") {
            speed = 1.2f;
            if(!PixelPreto.estaNaDireita) {
                speed *= -1;
                Vector3 vetor = transform.localScale;
                vetor.x *= -1;
                transform.localScale = vetor;
            }
        } else {
            speed = 0.8f;
        }
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        collider_olho = GetComponent<CircleCollider2D>();
        corpo = GetComponent<Rigidbody2D>(); 
        som_morte = GetComponent<AudioSource>();
        if(Chefao06.meia_vida == true) {
            anim.SetBool("meia_vida", true);
        } else {
            anim.SetBool("meia_vida", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        contador+= Time.deltaTime;

        if(PixelPreto.pularUmaVez) {
            if(!umaVez) {
                som_morte.Play();
                umaVez = true;
            }
            morreu = true;
            speed = 0;
            collider_olho.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("morreu", true);
            Destroy(this.gameObject, 0.8f);
        }

        if(cont_tiro >= 1.85f) {
            pode_meter_bala = true;
        }

        if(Chefao06.meia_vida == true) {
            tempo_pra_morrer = 15f;
            if(contador >= 13f) {
                pode_meter_bala = false;
                speed = 3.5f;
            }
        }

        if(!achou) {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        } else {
            if(!morreu) {

                cont_tiro += Time.deltaTime;

                Vector3 relativePosition = target.position - transform.position;

                if(SceneManager.GetActiveScene().name == "Fase10") {
                    if(PixelPreto.estaNaDireita) {
                        transform.rotation = Quaternion.LookRotation(relativePosition);
                    } else {
                        transform.rotation = Quaternion.LookRotation(-relativePosition);
                    }
                } else {
                    transform.rotation = Quaternion.LookRotation(relativePosition);
                }
                transform.Rotate(new Vector3(0, 90, 0), Space.Self);

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    if(Chefao06.meia_vida == true && pode_meter_bala) {
                        Instantiate(tiro, spawn_tiro.position, spawn_tiro.rotation);
                        pode_meter_bala = false;
                        cont_tiro = 0;
                    }
            }
        }

        if((Chefao06.atacando == false || contador >= tempo_pra_morrer) && SceneManager.GetActiveScene().name != "Fase10") {
            morreu = true;
            pode_meter_bala = false;
            speed = 0;
            collider_olho.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("morreu", true);
            Destroy(this.gameObject, 0.8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            som_morte.Play();
            if(SceneManager.GetActiveScene().name == "Fase7") {
                Chefao06.vida_restante--;
                Chefao06.dano_tomado++;
            }
            morreu = true;
            speed = 0;
            collider_olho.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("morreu", true);
            Destroy(this.gameObject, 0.8f);
        } else if(other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(Chefao06.meia_vida == false) {
                speed = 1.5f;
            } else {
                if(SceneManager.GetActiveScene().name == "Fase10") {
                    if(!PixelPreto.estaNaDireita) {
                        speed = -2f;
                    } else {
                        speed = 2f;
                    }
                }
                if(contador < 13f) {
                    speed = 0;
                }
            }
            achou = true;
        }
    }
}
