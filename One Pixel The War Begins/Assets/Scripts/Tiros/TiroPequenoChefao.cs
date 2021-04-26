using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPequenoChefao : MonoBehaviour
{

    private float speed = -2.8f;
    private float timeDestroy;
    private Animator anim;
    private BoxCollider2D collider;
    private SpriteRenderer sr;

    public static bool modoHard;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine("idle");
        anim = GetComponent<Animator>();
        timeDestroy = 3.5f;
        Destroy(gameObject, timeDestroy);
        collider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if(modoHard) {
            sr.color = Color.red;
            speed = -3.2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("plataforma")) {
            Morrer();
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("super_bullet_inimiga")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
	}

    IEnumerator idle() {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("idle", true);
    }

    private void Morrer() {
        anim.SetBool("morreu", true);
        speed = 0;
        collider.isTrigger = true;
        StartCoroutine("morre");
    }
}
