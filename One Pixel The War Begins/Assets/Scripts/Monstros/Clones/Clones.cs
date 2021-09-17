using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clones : MonoBehaviour
{

    public Transform spawn_tiro;
    public GameObject tiro;
    private float contador;

    private float speed = 6f;
    private Animator anim;

    public Transform local_chefao;
    private BoxCollider2D collider_clone;
    private bool podeIr;
    // Start is called before the first frame update
    void Start()
    {
        podeIr = true;
        contador = 0;
        anim = GetComponent<Animator>();
        collider_clone = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(FaseManager10.pode_comecar_10) {
            if(contador >= 2f) {
                Instantiate(tiro, spawn_tiro.position, spawn_tiro.rotation);
                contador = 0;
            }

            if(PixelPreto.sugando) {
                contador = 0;
                if(podeIr) {
                    transform.position = Vector2.MoveTowards(transform.position, local_chefao.position, speed * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes")) {
            anim.SetBool("morrer", true);
            podeIr = false;
            Destroy(gameObject, 1f);
            collider_clone.isTrigger = true;
        }
    }
}
