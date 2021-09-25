using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paredona : MonoBehaviour
{
    public static bool sumir;
    private Animator anim;
    private int vida;

    void Start()
    {
        vida = 10;
        sumir = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sumir) {
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.6f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(SceneManager.GetActiveScene().name == "Fase7_5") {
                PlayerPrefs.SetInt("Fase7_5", 1);
            }
        } else if(other.gameObject.CompareTag("bullet")) {
            if(PlayerControle.red_var) {
                vida--;
                if(vida <= 0) {
                    sumir = true;
                }
            }
        }
    }
}
