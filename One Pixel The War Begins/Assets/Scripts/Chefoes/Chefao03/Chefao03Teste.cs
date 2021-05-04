using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao03Teste : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    private Animator anim;

    private bool rodar;
    private bool umaVez, umaVezt;

    private BoxCollider2D collider_quadrado;
    private CircleCollider2D collider_redondo;

    private float contador;

    public Transform Player;

    void Start()
    {
        umaVezt = false;
        umaVez = false;
        rodar = false;
        collider_quadrado = GetComponent<BoxCollider2D>();
        collider_redondo = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine("rodarValendo");
    }

    // Update is called once per frame
    void Update()
    {
        if(rodar && !umaVez) {
            rb.AddForce(new Vector2(9.8f * 45f, 9.8f * 45f));
            umaVez = true;
        }
        if(rodar) {
            contador += Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 10));
        }

        if(contador >= 3f && contador <= 5f) {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, Player.position, 3f * Time.deltaTime);
        }

        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 10f);
    }

    IEnumerator rodarValendo() {
        collider_redondo.enabled = true;
        anim.SetBool("transformando", true);
        yield return new WaitForSeconds(2.0f);
        collider_quadrado.enabled = false;
        rodar = true;
    }
}
