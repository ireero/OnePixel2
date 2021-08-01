using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parado : MonoBehaviour
{
    public static bool escondido;

    void Start()
    {    
        escondido = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            escondido = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            escondido = false;
        }
    }
}
