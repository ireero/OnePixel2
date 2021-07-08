using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
    private Animator anim;
    public static bool sumir;

    void Start()
    {
        sumir = false;
        anim = GetComponent<Animator>();  
        StartCoroutine("ativarIdle");  
    }

    // Update is called once per frame
    void Update()
    {
        if(sumir) {
            anim.SetBool("idle", false);
            Destroy(this.gameObject, 1f);
        }
    }

    IEnumerator ativarIdle() {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("idle", true);
    }
}
