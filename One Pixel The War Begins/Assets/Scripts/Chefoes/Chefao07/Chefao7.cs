using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao7 : MonoBehaviour
{
    private Animator anim;
    private bool rindo;
    private bool umaVez;
    public static bool possuido;
    public static bool bateu_nele;
    private BoxCollider2D colisor;
    public Animator do_background;
    public AudioSource som_terremoto;
    private bool som_umaVez;
    public static bool red_sair;
    private bool redUmaVez;

    void Start()
    {
        redUmaVez = true;
        som_umaVez = false;
        bateu_nele = false;
        possuido = false;
        umaVez = false;
        rindo = false;
        red_sair = false;
        anim = GetComponent<Animator>();
        colisor = GetComponent<BoxCollider2D>();
        if(PlayerControle.red_var) {
            anim.SetBool("legal", true);
            anim.SetBool("idle", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Portal.atira_ae_po == 5 && !umaVez) {
            umaVez = true;
            anim.SetBool("transformar", true);
            colisor.isTrigger = true;
        }

        if(red_sair) {
            if(redUmaVez) {
                anim.SetBool("idle", true);
                anim.SetBool("legal", false);
                redUmaVez = false;
            }
        }

        if(FaseManager9.acabou == true) {
            Camera.tremer_bastante = true;
            if(!som_umaVez) {
                som_terremoto.Play();
                som_umaVez = true;
            }
            do_background.SetBool("mexer", true);
            anim.SetBool("transformar", false);
            StartCoroutine("voltarNormal");
        }

        if(FaseManager9.cabo_tudo) {
            anim.SetBool("embora", true);
            Destroy(gameObject, 0.8f);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(!rindo) {
                anim.SetBool("idle", false);
                anim.SetBool("rindo", true);
                rindo = true;
                StartCoroutine("rir");
            }

            if(possuido) {
                bateu_nele = true;
            }
        }
    }

    IEnumerator rir() {
        yield return new WaitForSeconds(2.5f);
        rindo = false;
        anim.SetBool("idle", true);
        anim.SetBool("rindo", false);
    }

    IEnumerator voltarNormal() {
        yield return new WaitForSeconds(5f);
        possuido = false;
        som_terremoto.Stop();
        FaseManager9.acabou = false;
        Camera.tremer_bastante = false;
        do_background.SetBool("mexer", false);
        anim.SetBool("sumir", true);
        colisor.isTrigger = false;
    }
}
