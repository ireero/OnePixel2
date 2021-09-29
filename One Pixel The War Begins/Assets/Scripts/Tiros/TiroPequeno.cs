using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPequeno : MonoBehaviour
{

    private float speed = 4;
    private float timeDestroy;
    private Animator anim;
    private BoxCollider2D collider_tirinho;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine("ativarIdle");
        anim = GetComponent<Animator>();
        timeDestroy = 3.5f;
        Destroy(gameObject, timeDestroy);
        collider_tirinho = GetComponent<BoxCollider2D>();
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
        other.gameObject.CompareTag("bullet_inimiga") || other.gameObject.CompareTag("super_bullet_inimiga") || other.gameObject.CompareTag("pedras")) {
            anim.SetBool("morreu", true);
        } else if(other.gameObject.CompareTag("plataforma") || other.gameObject.CompareTag("moeda_rir")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public void MorreBala() {
        Destroy(this.gameObject);
    }

    public void PrepararMorrer() {
        rb.bodyType = RigidbodyType2D.Static;
        speed = 0;
        collider_tirinho.isTrigger = true;
    }

    IEnumerator ativarIdle() {
        yield return new WaitForSeconds(0.25f);
        if(PlayerControle.red_var) {
            anim.SetBool("red", true);
            anim.SetBool("idle", false);
        } else {
            anim.SetBool("idle", true);
            anim.SetBool("red", false);
        }
    }
}
