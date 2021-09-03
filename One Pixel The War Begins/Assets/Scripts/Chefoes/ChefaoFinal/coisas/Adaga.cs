using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adaga : MonoBehaviour
{
    private PolygonCollider2D collider;
    private Rigidbody2D corpo;
    private Animator anim;

    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("poderCair");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player")) {
            collider.isTrigger = true;
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.6f);
        } else if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else if(other.gameObject.CompareTag("plataforma")) {
            Destroy(gameObject);
        }
    }

    IEnumerator poderCair() {
        yield return new WaitForSeconds(0.8f);
        corpo.bodyType = RigidbodyType2D.Dynamic;
    }
}
