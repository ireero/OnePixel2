using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager3 : MonoBehaviour
{
    // Vidas
    public Image BarraVida1;
    public Image BarraVida2;
    public Image BarraVida3;

    private int vida_chefao;

    void Start()
    {
        PlayerControle.podeAtirar = true;
    }

    // Update is called once per frame
    void Update()
    {
        vida_chefao = Chefao032.vida_chefao;
        switch(vida_chefao) {
            case 2:
                Destroy(BarraVida1);
                break;
            case 1:
                Destroy(BarraVida2);
                break;
            case 0:
                Destroy(BarraVida3);
                break;        
        }
    }
}
