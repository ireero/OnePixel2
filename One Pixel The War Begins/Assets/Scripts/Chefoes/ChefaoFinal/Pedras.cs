using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedras : MonoBehaviour
{
    private Animator anim;
    private PolygonCollider2D collider;
    private Rigidbody2D corpo;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PixelPreto.atirou_adagas == 0) {
            anim.SetBool("sumir", true);
            collider.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("plataforma")) {
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("sumir", true);
            collider.isTrigger = true;
        } else if(other.gameObject.CompareTag("Chefoes")) {
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("energia", true);
            transform.Rotate(new Vector3(0, 0, 90));
            collider.isTrigger = true;
            PixelPreto.vida_pixel_preto += 5;
        }
    }


    public void podeIr() {
        corpo.bodyType = RigidbodyType2D.Dynamic;
    }

    public void podeMorrer() {
        Destroy(this.gameObject);
    }
}
