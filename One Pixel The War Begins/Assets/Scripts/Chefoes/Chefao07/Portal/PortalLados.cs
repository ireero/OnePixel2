using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLados : MonoBehaviour
{
    public GameObject tiro;
    private Animator anim;
    private float contador;
    private Transform target;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        StartCoroutine("focarIdle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(Portal.atira_ae_po == true) {
            if(contador >= 1f) {
                Instantiate(tiro, this.gameObject.transform.position, this.gameObject.transform.rotation);
                contador = 0;
            }
        }
    }

    IEnumerator focarIdle() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("idle", true);
    }
}
