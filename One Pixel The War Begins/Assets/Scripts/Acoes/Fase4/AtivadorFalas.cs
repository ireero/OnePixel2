using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorFalas : MonoBehaviour
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
            if(FaseManager4.jaConversou == false) {
                PlayerControle.podeAtirar = false;
                PlayerControle.pode_mexer = false;
                FaseManager4.pode_comecar_4 = true;
            }
        }
    }
}
