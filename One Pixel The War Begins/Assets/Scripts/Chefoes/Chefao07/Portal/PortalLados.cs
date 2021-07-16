using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLados : MonoBehaviour
{
    public GameObject tiro;
    public GameObject tirao;
    public GameObject girador;

    private Animator anim;
    private float contador;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();
        StartCoroutine("focarIdle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(Portal.atira_ae_po == 1) {
            if(contador >= 1.2f) {
                Instantiate(tiro, this.gameObject.transform.position, this.gameObject.transform.rotation);
                contador = 0;
            }
        } else if(Portal.atira_ae_po == 2) {
            if(contador >= 1.25f) {
                Instantiate(tirao, this.gameObject.transform.position, this.gameObject.transform.rotation);
                contador = 0;
            }
        } else if(Portal.atira_ae_po == 3) {
            if(contador >= 2.2f) {
                Instantiate(girador, this.gameObject.transform.position, this.gameObject.transform.rotation);
                contador = 0;
            }
        } else if(Portal.atira_ae_po == 5) {
            anim.SetBool("idle", false);
            StartCoroutine("sumir");
        }
    }

    IEnumerator focarIdle() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("idle", true);
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
