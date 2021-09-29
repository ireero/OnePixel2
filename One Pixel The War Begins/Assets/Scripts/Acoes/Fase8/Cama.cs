using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks.Data;
using Steamworks;

public class Cama : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            var ach = new Achievement("NAP_TIME");
            ach.Trigger();
        }
    }
}
