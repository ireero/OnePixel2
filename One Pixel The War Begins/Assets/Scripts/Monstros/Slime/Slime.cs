using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Chefao04.vida_chefao == 25f || Chefao04.vida_chefao <= 0) {
            FaseManager5.slimes_vivos--;
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Player")) {
            FaseManager5.slimes_vivos--;
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.4f);
        }
    }
}
