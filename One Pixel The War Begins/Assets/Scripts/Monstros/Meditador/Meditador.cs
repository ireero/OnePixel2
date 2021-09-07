using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditador : MonoBehaviour
{
    public static bool podeMorrer;
    private Animator anim;
    public GameObject escada;

    void Start()
    {
        anim = GetComponent<Animator>();
        if(GameManager.sem_dialogos == 0) {
            podeMorrer = false;   
        } else {
            podeMorrer = true;
        }
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
        escada.SetActive(true);
        Destroy(this.gameObject);
    } 
}
