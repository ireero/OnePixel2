using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFogoCaindo : MonoBehaviour
{

    private float speed_bola_fogo;
    private float timeDestroy;
    private Animator anim;
    private CapsuleCollider2D collider;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        speed_bola_fogo = -6.5f;
        anim = GetComponent<Animator>();
        timeDestroy = 2f;
        Destroy(gameObject, timeDestroy);
        collider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up* speed_bola_fogo * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir") || other.gameObject.CompareTag("chao") || other.gameObject.CompareTag("Player")) {
            Morrer();
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("super_bullet_inimiga") || 
            other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("moeda_rir")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        anim.SetBool("sumir", true);
        speed_bola_fogo = 0;
        collider.isTrigger = true;
        StartCoroutine("morre");
    }
}
