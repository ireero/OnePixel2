using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager3 : MonoBehaviour
{
    // Vidas
    public Image BarraVida1;
    public Image vidinha1;

    public Image BarraVida2;
    public Image vidinha2;

    public Image BarraVida3;
    public Image vidinha3;

    private int vida_chefao;

    void Start()
    {
        PlayerControle.podeAtirar = true;
    }

    // Update is called once per frame
    void Update()
    {
        vida_chefao = Chefao03.vida_chefao;
        switch(vida_chefao) {
            case 2:
                vidinha1.enabled = false;
                break;
            case 1:
                vidinha2.enabled = false;
                break;
            case 0:
                vidinha1.enabled = true;
                vidinha2.enabled = true;
                BarraVida1.color = Color.red;
                BarraVida2.color = Color.red;
                BarraVida3.color = Color.red;
                break;  
            case -1:
                Destroy(BarraVida1);
                Destroy(vidinha1);
                break;
            case -2:
                Destroy(BarraVida2);
                Destroy(vidinha2);
                break;
            case -3:
                Destroy(BarraVida3);
                Destroy(vidinha3);
                break;        
        }
    }
}
