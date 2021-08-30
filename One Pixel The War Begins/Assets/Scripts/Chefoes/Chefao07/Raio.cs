using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raio : MonoBehaviour
{
    private Animator anim;
    private float contador;
    private BoxCollider2D collider;
    private AudioSource som_trovao;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();    
        som_trovao = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 1.2f && contador < 1.6f) {
            som_trovao.Play();
            collider.enabled = true;
            anim.SetBool("cair", true);
        } else if(contador >= 1.6f) {
            FaseManager9.soltarRaio = false;
            Destroy(gameObject);
        }
    }
}
