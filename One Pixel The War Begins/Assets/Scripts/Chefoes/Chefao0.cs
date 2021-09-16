using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao0 : MonoBehaviour
{
    private BoxCollider2D collider;
    private Rigidbody2D corpo;
    private float velocidade;
    private Animator anim;
    private float forca_pulo;
    private Transform target;
    private bool lookingRight;
    public Animator anim_back;

    void Start()
    {
        lookingRight = false;
        collider = GetComponent<BoxCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        velocidade = 0.5f;
        forca_pulo = 500f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x < target.transform.position.x) {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
            if(!lookingRight) {
                Flip();
            }
        } else {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
            if(lookingRight) {
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            Camera.tremer_chao = true;
            anim_back.SetBool("tremer_chao", true);
            anim.SetBool("pular", false);
        }
    }

    void Flip(){
      lookingRight = !lookingRight;
      Vector3 myScale = transform.localScale;
      myScale.x *= -1;
      transform.localScale = myScale;
   }

    public void Pular() {
        corpo.AddForce(new Vector2(0, forca_pulo));
    }

    public void Cair() {
        anim.SetBool("pular", true);
    }
}
