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
    private BoxCollider2D collider;

    void Start()
    {
        bateu_nele = false;
        possuido = false;
        umaVez = false;
        rindo = false;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Portal.atira_ae_po == 5 && !umaVez) {
            umaVez = true;
            anim.SetBool("transformar", true);
            collider.isTrigger = true;
        }

        if(FaseManager9.acabou == true) {
            Camera.tremer_bastante = true;
            anim.SetBool("transformar", false);
            StartCoroutine("voltarNormal");
        }

        if(FaseManager9.cabo_tudo) {
            Destroy(gameObject);
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
        FaseManager9.acabou = false;
        Camera.tremer_bastante = false;
        anim.SetBool("sumir", true);
        collider.isTrigger = false;
    }
}
