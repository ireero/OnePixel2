using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager6 : MonoBehaviour
{
    public Image BarraDeVida;
    public Image BarraVidaMaior;

    private float vida_maxima = 600f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       BarraVida(); 
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao05.vida / vida_maxima;
    }
}
