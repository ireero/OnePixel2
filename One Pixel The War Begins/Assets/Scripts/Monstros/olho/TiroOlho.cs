using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroOlho : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D collider;
    private float velocidade;

    void Start()
    {
        velocidade = -0.025f;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(velocidade + Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("plataforma")) {
            velocidade = 0;
            collider.isTrigger = true;
            anim.SetBool("destruir", true);
            Destroy(this.gameObject, 0.7f);
        }    
    }
}
