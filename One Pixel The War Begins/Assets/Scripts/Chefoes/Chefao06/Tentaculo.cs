using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
    private Animator anim;
    private float contador;
    private float tempo_pra_morrer;
    private AudioSource dano;

    void Start()
    {
        tempo_pra_morrer = 3.4f;
        contador = 0;
        anim = GetComponent<Animator>();  
        StartCoroutine("ativarFase");  
        if(Chefao06.meia_vida == true) {
            anim.SetBool("meia_vida", true);
            tempo_pra_morrer = 4f;
        } else {
            anim.SetBool("meia_vida", false);
        }
        dano = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        contador += Time.deltaTime;

        if(contador >= tempo_pra_morrer || Chefao06.camuflado_ja == false) {
            anim.SetBool("idle", false);
            Destroy(this.gameObject, 2f);
        }
    }

    IEnumerator ativarFase() {
        yield return new WaitForSeconds(1.6f);
        FaseManager7.liberado = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(Chefao06.dano_tomado < 50) {
                dano.Play();
                Chefao06.vida_restante--;
                Chefao06.dano_tomado++;
            }
        } else if (other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
