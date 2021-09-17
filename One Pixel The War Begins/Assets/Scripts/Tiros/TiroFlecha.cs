using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroFlecha : MonoBehaviour
{

    private float speed_flecha;
    private float timeDestroy;
    private Animator anim;
    private PolygonCollider2D collider_flecha;

    // Start is called before the first frame update
    void Start()
    {
        speed_flecha = -8f;
        anim = GetComponent<Animator>();
        timeDestroy = 2f;
        Destroy(gameObject, timeDestroy);
        collider_flecha = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed_flecha * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir") || other.gameObject.CompareTag("chao")) {
            Destroy(this.gameObject);
        } else if(other.gameObject.CompareTag("Player")) {
            Morrer();
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("super_bullet_inimiga") || 
            other.gameObject.CompareTag("monstro") || other.gameObject.CompareTag("moeda_rir")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator morre() {
		yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
	}

    private void Morrer() {
        anim.SetBool("morrer", true);
        speed_flecha = 0;
        collider_flecha.isTrigger = true;
        StartCoroutine("morre");
    }
}
