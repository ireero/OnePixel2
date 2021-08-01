using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarChefao : MonoBehaviour
{
    public static bool ativarOlhao;

    void Start()
    {
        ativarOlhao = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            ativarOlhao = true;
            Destroy(this.gameObject);
        }
    }
}
