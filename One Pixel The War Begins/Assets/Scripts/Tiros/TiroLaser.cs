using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroLaser : MonoBehaviour
{

    private float speed_laser;
    private float timeDestroy;
    private Animator anim;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        speed_laser = -6f;
        anim = GetComponent<Animator>();
        timeDestroy = 2.5f;
        Destroy(gameObject, timeDestroy);
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Chefao7.bateu_nele == true && speed_laser > 0) {
            speed_laser  = -8f;
        }

        transform.Translate(Vector2.right * speed_laser * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir") || other.gameObject.CompareTag("chao")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("plataforma")) {
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
        speed_laser = 0;
        collider.isTrigger = true;
        StartCoroutine("morre");
    }
}
