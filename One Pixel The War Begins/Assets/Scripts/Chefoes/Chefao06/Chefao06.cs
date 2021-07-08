using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao06 : MonoBehaviour
{
    private Animator anim;
    private float contador;
    public static bool atacando;

    void Start()
    {
        atacando = false;
        Camera.tremer_bastante = true;
        contador = 0;
        anim = GetComponent<Animator>();
        StartCoroutine("idleAposNascer");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 4f) {
            anim.SetBool("atacar", true);
            anim.SetBool("idle", false);
            StartCoroutine("horaDoAtaque");
        }
    }

    IEnumerator idleAposNascer() {
        yield return new WaitForSeconds(1.5f);
        Camera.tremer_bastante = false;
        anim.SetBool("idle", true);
    }

    IEnumerator horaDoAtaque() {
        yield return new WaitForSeconds(3f);
        atacando = true;
        anim.SetBool("atacou", true);
    }
}
