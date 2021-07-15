using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao7 : MonoBehaviour
{
    private Animator anim;
    private bool rindo;

    void Start()
    {
        rindo = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(!rindo) {
                anim.SetBool("idle", false);
                anim.SetBool("rindo", true);
                rindo = true;
                StartCoroutine("rir");
            }
        }
    }

    IEnumerator rir() {
        yield return new WaitForSeconds(2.5f);
        rindo = false;
        anim.SetBool("idle", true);
        anim.SetBool("rindo", false);
    }
}
