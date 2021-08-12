using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRaposa : MonoBehaviour
{
    public SliderJoint2D slider;
    public JointMotor2D aux;
    public static bool ataqueRaposa;
    public static bool voltar;
    private Animator anim;
    private PolygonCollider2D collider;

    void Start()
    {
        voltar = false;
        ataqueRaposa = false;
        aux = slider.motor;
        anim = GetComponent<Animator>();
        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Portal.atira_ae_po == 5) {
            anim.SetBool("sumir", true);
            StartCoroutine("sumir");
        }

        if(ataqueRaposa) {
            anim.SetBool("correndo", true);
            if(!voltar) {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                aux.motorSpeed = 3.5f;
                slider.motor = aux;
            }
            
            if(slider.limitState == JointLimitState2D.LowerLimit) {
            voltar = false;
        }

        if(slider.limitState == JointLimitState2D.UpperLimit) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            voltar = true;
            aux.motorSpeed = -3.5f;
            slider.motor = aux;
            }
        } else {
            anim.SetBool("correndo", false);
            aux.motorSpeed = 0;
            slider.motor = aux;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes") || other.gameObject.CompareTag("monstro")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else if(other.gameObject.CompareTag("Player")) {
            collider.isTrigger = true;
            StartCoroutine("voltarDano");
        }
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator voltarDano() {
        yield return new WaitForSeconds(3f);
        collider.isTrigger = false;
    }
}
