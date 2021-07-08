using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tijolo : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D corpo;
    private BoxCollider2D collider;
    private float potenciaRot;

    void Start()
    {
        potenciaRot = 0.4f;
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        corpo = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(x: 0, y: 0, z: potenciaRot));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("chao") || other.gameObject.CompareTag("plataforma") || 
        other.gameObject.CompareTag("monstro")) {
            collider.isTrigger = true;
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("quebrar", true);
            Destroy(this.gameObject, 0.5f);
        } else if(other.gameObject.CompareTag("Chefoes")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
