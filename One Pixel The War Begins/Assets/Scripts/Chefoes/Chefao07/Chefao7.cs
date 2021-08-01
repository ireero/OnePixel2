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

    void Start()
    {
        bateu_nele = false;
        possuido = false;
        umaVez = false;
        rindo = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Portal.atira_ae_po == 5 && !umaVez) {
            umaVez = true;
            anim.SetBool("transformar", true);
            StartCoroutine("idleMeiaVida");
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

    IEnumerator idleMeiaVida() {
        yield return new WaitForSeconds(1.35f);
        anim.SetBool("transformar", false);
    }
}
