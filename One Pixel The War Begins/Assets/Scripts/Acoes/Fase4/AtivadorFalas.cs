using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorFalas : MonoBehaviour
{
    public GameObject conquista;
    public AudioSource conquista_som;
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
            } else if(FaseManager4.jaConversou) {
                conquista_som.Play();
                conquista.SetActive(true);
                PlayerControle.jaPodePularDuas = true;
                PlayerPrefs.SetInt("PularDuas", 1);
                GameManager.Instance.SalvarSit(2, "Fase4");
                Destroy(gameObject);
            }
        }
    }
}
