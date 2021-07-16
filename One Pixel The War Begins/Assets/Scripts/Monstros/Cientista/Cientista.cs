using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cientista : MonoBehaviour
{
    private float velocidade;
    
    void Start()
    {
        velocidade = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(FaseManager8.pode_comecar_8 == true) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if(FaseManager8.jabateuUmPapo == true) {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("paredesSumir")) {
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            Destroy(gameObject);
        }
    }
}
