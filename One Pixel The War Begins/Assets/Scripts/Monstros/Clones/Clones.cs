using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clones : MonoBehaviour
{

    public Transform spawn_tiro;
    public GameObject tiro;
    private float contador;

    private float speed = 6f;
    private Animator anim;

    public Transform local_chefao;
    private BoxCollider2D collider_clone;
    private bool podeIr;

    private int vida;
    private bool descansar;
    private float cont_descanso;

    private Image img;

    private AudioSource som_quebrando;
    public AudioSource som_dano;
    // Start is called before the first frame update
    void Start()
    {
        cont_descanso = 0;
        descansar = false;
        vida = 15;
        podeIr = true;
        contador = 0;
        anim = GetComponent<Animator>();
        collider_clone = GetComponent<BoxCollider2D>();
        img = GetComponent<Image>();
        som_quebrando = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        
            if(descansar) {
                cont_descanso += Time.deltaTime;
                if(cont_descanso >= 8.5f) {
                    anim.SetBool("descansar", false);
                    collider_clone.isTrigger = false;
                    descansar = false;
                    vida = 15;
                    img.color = Color.red;
                }
            } else {
                if(FaseManager10.pode_comecar_10) {
                if(contador >= 2f) {
                    Instantiate(tiro, spawn_tiro.position, spawn_tiro.rotation);
                    contador = 0;
                }
            }

            if(PixelPreto.sugando) {
                contador = 0;
                if(podeIr) {
                    transform.position = Vector2.MoveTowards(transform.position, local_chefao.position, speed * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes")) {
            anim.SetBool("morrer", true);
            podeIr = false;
            Destroy(gameObject, 1f);
            som_quebrando.Play();
            collider_clone.isTrigger = true;
        } else if(other.gameObject.CompareTag("bullet")) {
            som_dano.Play();
            if(PlayerControle.red_var) {
                vida -= 3;
            } else {
                vida--;
            }
            if(vida <= 0) {
                img.color = Color.white;
                collider_clone.isTrigger = true;
                anim.SetBool("descansar", true);
                descansar = true;
            }
        }
    }
}
