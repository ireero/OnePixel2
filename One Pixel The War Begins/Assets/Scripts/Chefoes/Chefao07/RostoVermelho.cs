using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RostoVermelho : MonoBehaviour
{
    public static bool apareceu;
    private float contador;
    private Animator anim;
    private AudioSource som_risada;

    void Start()
    {
        anim = GetComponent<Animator>();
        contador = 0;
        som_risada = GetComponent<AudioSource>();
        som_risada.Play();
    }

    // Update is called once per frame
    void Update()
    {
            contador += Time.deltaTime;
            if(contador >= 2f) {
                anim.SetBool("sumir", true);
                anim.SetBool("aparecer", false);
                StartCoroutine("sumir");
            }
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
