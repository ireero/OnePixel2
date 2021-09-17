using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    private Animator anim;
    private float tempo;
    private BoxCollider2D collider_plataforma;

    // Start is called before the first frame update
    void Start()
    {   
        collider_plataforma = GetComponent<BoxCollider2D>();
        tempo = 0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if(tempo >= 2.0f) {
            anim.SetBool("sumir", true);
            StartCoroutine("sumir");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || 
        other.gameObject.CompareTag("bullet_inimiga") || other.gameObject.CompareTag("super_bullet_inimiga")) {
            collider_plataforma.isTrigger = true;
            anim.SetBool("destruida", true);
            StartCoroutine("destruir");
        }
    }

    IEnumerator destruir() {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
}
