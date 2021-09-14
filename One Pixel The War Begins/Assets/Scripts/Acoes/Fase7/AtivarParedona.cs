using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarParedona : MonoBehaviour
{
    public GameObject paredona;
    public Transform spawn_paredona;
    public static bool ativouParedona;

    void Start()
    {
        ativouParedona = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Instantiate(paredona, spawn_paredona.position, spawn_paredona.rotation);
            ativouParedona = true;
        }
    }
}
