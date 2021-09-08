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
        StartCoroutine("podeIr");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("plataforma")) {
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("sumir", true);
            collider.isTrigger = true;
            Destroy(gameObject, 0.5f);
        } else if(other.gameObject.CompareTag("Chefoes")) {
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("energia", true);
            if(PixelPreto.estaNaDireita) {
                transform.Rotate(new Vector3(0, 0, -90));
            } else {
                transform.Rotate(new Vector3(0, 0, 90));
            }
            collider.isTrigger = true;
            Destroy(gameObject, 1f);
            PixelPreto.vida_pixel_preto += 5;
        }
    }

    IEnumerator podeIr() {
        yield return new WaitForSeconds(1.5f);
        corpo.bodyType = RigidbodyType2D.Dynamic;
    }
}
