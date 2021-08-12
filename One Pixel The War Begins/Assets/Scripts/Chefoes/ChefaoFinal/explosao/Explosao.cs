using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosao : MonoBehaviour
{
    private Animator anim;
    private float contador;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();    
        StartCoroutine("idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator idle() {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("sumir", true);
        Destroy(gameObject, 0.5f);
    } 
}
