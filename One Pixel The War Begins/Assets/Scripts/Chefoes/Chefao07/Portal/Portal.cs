using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject tiro;
    public static bool atira_ae_po;
    private Animator anim;
    private float contador;

    void Start()
    {
        contador = 0;
        atira_ae_po = false;   
        anim = GetComponent<Animator>(); 
        StartCoroutine("focarIdle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 0.5f && atira_ae_po) {
            Instantiate(tiro, this.gameObject.transform.position, this.gameObject.transform.rotation);
            contador = 0;
        }
    }

    IEnumerator focarIdle() {
        yield return new WaitForSeconds(1.2f);
        atira_ae_po = true;
        anim.SetBool("idle", true);
    }
}
