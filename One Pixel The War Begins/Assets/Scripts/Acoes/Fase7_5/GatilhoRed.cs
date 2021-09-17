using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoRed : MonoBehaviour
{
    public GameObject Red;
    private BoxCollider2D collider_fase7;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Red.SetActive(true);
        }
    }
}
