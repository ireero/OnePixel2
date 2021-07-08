using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private Animator anim;
    public static bool tremer;
    public static bool tremer_chao;
    public static bool tremer_bastante;

    // Start is called before the first frame update
    void Start()
    {
        tremer_chao = false;
        tremer = false;
        tremer_bastante = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tremer) {
            anim.SetBool("tremeu", true);
            StartCoroutine("voltarIdle");
        }

        if(tremer_chao) {
            anim.SetBool("chao_tremeu", true);
            StartCoroutine("voltarIdleChao");
        }

        if(tremer_bastante) {
            anim.SetBool("tremeu_leve", true);
        } else {
            anim.SetBool("tremeu_leve", false);
        }
    }

    IEnumerator voltarIdle() {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("tremeu", false);
        tremer = false;
    }

    IEnumerator voltarIdleChao() {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("chao_tremeu", false);
        tremer_chao = false;
    }
}
