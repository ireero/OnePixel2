using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorFalas8 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(FaseManager8.jabateuUmPapo == false) {
                PlayerControle.podeAtirar = false;
                PlayerControle.pode_mexer = false;
                FaseManager8.pode_comecar_8 = true;
            }
        }
    }
}
