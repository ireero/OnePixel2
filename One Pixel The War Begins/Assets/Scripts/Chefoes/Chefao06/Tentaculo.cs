using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculo : MonoBehaviour
{
    private Animator anim;
    private float contador;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();  
        StartCoroutine("ativarIdle");  
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;

        if(contador >= 3.4f || Chefao06.camuflado_ja == false) {
            anim.SetBool("idle", false);
            Destroy(this.gameObject, 2f);
        }
    }

    IEnumerator ativarIdle() {
        yield return new WaitForSeconds(1.6f);
        anim.SetBool("idle", true);
        FaseManager7.liberado = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            if(Chefao06.dano_tomado < 50) {
                Chefao06.vida_restante--;
                Chefao06.dano_tomado++;
            }
        } else if (other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
