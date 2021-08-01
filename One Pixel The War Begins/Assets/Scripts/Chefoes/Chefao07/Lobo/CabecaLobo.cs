using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabecaLobo : MonoBehaviour
{
    private Animator anim;
    public Transform spawn_tiro;
    public GameObject tiro;
    private bool podeAtirar;
    private float contador;
    private int tirosDados;

    void Start()
    {
        tirosDados = 0;
        podeAtirar = false;
        contador = 0;
        StartCoroutine("atirar");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       contador += Time.deltaTime;

       if(podeAtirar) {
           if(contador >= 0.5f) {
               Instantiate(tiro, spawn_tiro.position, spawn_tiro.rotation);
               tirosDados++;
               contador = 0;
           }
       } 

       if(tirosDados >= 3) {
           podeAtirar = false;
           anim.SetBool("sumir", true);
           Destroy(gameObject, 0.5f);
       }
    }

    IEnumerator atirar() {
        yield return new WaitForSeconds(0.5f);
        podeAtirar = true;
    }
}
