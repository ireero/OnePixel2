using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedras : MonoBehaviour
{
    private Animator anim;
    private PolygonCollider2D collider_pedra;
    private Rigidbody2D corpo;
    private AudioSource som_pedra_nascer;
    public AudioClip som_explode;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        collider_pedra = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
        som_pedra_nascer = GetComponent<AudioSource>();
        som_pedra_nascer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(PixelPreto.atirou_adagas == 0) {
            anim.SetBool("sumir", true);
            collider_pedra.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("plataforma")) {
            som_pedra_nascer.clip = som_explode;
            som_pedra_nascer.Play();
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("sumir", true);
            collider_pedra.isTrigger = true;
        } else if(other.gameObject.CompareTag("Chefoes")) {
            som_pedra_nascer.clip = som_explode;
            som_pedra_nascer.Play();
            corpo.bodyType = RigidbodyType2D.Static;
            anim.SetBool("energia", true);
            transform.Rotate(new Vector3(0, 0, 90));
            collider_pedra.isTrigger = true;
            if(PixelPreto.vida_pixel_preto <= 500) {
                PixelPreto.vida_pixel_preto += 4;
            }
        }
    }


    public void podeIr() {
        corpo.bodyType = RigidbodyType2D.Dynamic;
    }

    public void podeMorrer() {
        Destroy(this.gameObject);
    }

}
