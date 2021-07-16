using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject tiro;
    public GameObject tirao;

    public static int atira_ae_po;
    private Animator anim;
    private float contador;
    private bool umaVez;

    void Start()
    {
        umaVez = false;
        contador = 0;
        atira_ae_po = 0;   
        anim = GetComponent<Animator>(); 
        StartCoroutine("focarIdle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 0.85f && atira_ae_po == 1) {
            MoveRaposa.ataqueRaposa = true;
            Instantiate(tiro, this.gameObject.transform.position, this.gameObject.transform.rotation);
            contador = 0;
        } else if(contador >= 0.75f && atira_ae_po == 2) {
            MoveRaposa.ataqueRaposa = false;
            Instantiate(tirao, this.gameObject.transform.position, this.gameObject.transform.rotation);
            contador = 0;
        } else if(contador >= 1.2f && atira_ae_po == 3) {
            MoveRaposa.ataqueRaposa = true;
            if(!umaVez) {
                MoveRaposa.voltar = false;
                umaVez = true;
            }
            Instantiate(tiro, this.gameObject.transform.position, this.gameObject.transform.rotation);
            contador = 0;
        } else if(atira_ae_po == 5) {
            MoveRaposa.ataqueRaposa = false;
            anim.SetBool("idle", false);
            StartCoroutine("sumir");
        }
    }

    IEnumerator focarIdle() {
        yield return new WaitForSeconds(1.2f);
        atira_ae_po = 1;
        anim.SetBool("idle", true);
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
