using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditador : MonoBehaviour
{
    public static bool podeMorrer;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        podeMorrer = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if(podeMorrer) {
            anim.SetBool("morrer", true);
            StartCoroutine("sumir");
        }
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(2.1f);
        Destroy(this.gameObject);
    } 
}
