using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raio : MonoBehaviour
{
    private Animator anim;
    private float contador;
    private BoxCollider2D collider_raio;
    private AudioSource som_trovao;
    private bool umaMusica;

    private float[] tempos = {1.2f, 1.6f};

    void Start()
    {
        if(GameManager.sem_dialogos == 1) {
            tempos[0] = 0.9f;
            tempos[1] = 1.3f;
        }
        umaMusica = false;
        collider_raio = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();    
        som_trovao = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= tempos[0] && contador < tempos[1]) {
            if(!umaMusica) {
                som_trovao.Play();
                umaMusica = true;
            }
            collider_raio.enabled = true;
            anim.SetBool("cair", true);
        } else if(contador >= tempos[1]) {
            FaseManager9.soltarRaio = false;
            Destroy(gameObject);
        }
    }
}
