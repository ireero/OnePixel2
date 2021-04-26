using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBase : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("esperarCair");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao") || Chefao01.vivo == false) {
            anim.SetBool("morte_caiu", true);
            Morte();
            Destroy(this.gameObject, 2f);
        } else if(other.gameObject.CompareTag("bullet")) {
            anim.SetBool("morte_tiro", true);
            Morte();
            Destroy(this.gameObject, 2f);
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    IEnumerator esperarCair() {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("nasceu", true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Morte() {
        collider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
