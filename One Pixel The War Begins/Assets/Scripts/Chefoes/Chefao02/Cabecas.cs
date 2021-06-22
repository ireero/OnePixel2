using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabecas : MonoBehaviour
{
    public float vida;
    public bool pode_mexer;
    public bool podeAtirar;
    public float tempoDeTiro;
    public bool tomou_dano;
    public bool renascido;

    public SpriteRenderer sr;
    public Rigidbody2D corpo;
    public PolygonCollider2D collider;

    public Animator anim;

    public void Renascer() {
        anim.SetBool("renascer", true);
        podeAtirar = false;
        sr.color = Color.red;
        tempoDeTiro = 1f;
    }

    public void Locaute() { 
        anim.SetBool("locaute", true);
    } 

    public void Morrer() {
        if(vida <= 0) {
            collider.isTrigger = true;
            sr.color = Color.white;
            anim.SetBool("morreu", true);
        }
    }

    public void TomarDano(float cont) {
        if(vida == 40 || vida == 30 || vida == 20 || vida == 10) {
            anim.SetBool("tomou_dano", true);
            podeAtirar = false;
            tomou_dano = true;
        }
    }  
}
