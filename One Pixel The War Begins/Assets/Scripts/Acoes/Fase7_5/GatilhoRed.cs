using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            PlayerPrefs.SetInt("Fase7_5", 2);
            PlayerPrefs.SetInt("RED", 1);
            Red.SetActive(true);
        }
    }
}
