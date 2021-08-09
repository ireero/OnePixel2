using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBase : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private AudioSource audio_morte;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio_morte = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("esperarCair");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao") || FaseManager.chefao_vivo == false || other.gameObject.CompareTag("Chefoes")) {
            Morte(0);
        } else if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player")) {
            Morte(1);
        }
    }

    IEnumerator esperarCair() {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("nasceu", true);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Morte(int qual) {
        sr.color = Color.white;
        collider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Static;
        audio_morte.Play();
        if(qual == 0) {
            anim.SetBool("morte_caiu", true);
            Destroy(gameObject, 0.55f);
        } else if(qual == 1) {
            anim.SetBool("morte_tiro", true);
            Destroy(gameObject, 1.2f);
        }
    }
}
