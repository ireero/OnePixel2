using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPequeno : MonoBehaviour
{

    private float speed = 4;
    private float timeDestroy;
    private Animator anim;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine("ativarIdle");
        anim = GetComponent<Animator>();
        timeDestroy = 3.5f;
        Destroy(gameObject, timeDestroy);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro") || 
        other.gameObject.CompareTag("bullet_inimiga") || other.gameObject.CompareTag("super_bullet_inimiga")) {
            Morrer();
        } else if(other.gameObject.CompareTag("plataforma") || other.gameObject.CompareTag("moeda_rir")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(0.75f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("morreu", true);
        speed = 0;
        collider.isTrigger = true;
        StartCoroutine("morre");
    }

    IEnumerator ativarIdle() {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("idle", true);
    }
}
