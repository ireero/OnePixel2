using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager2 : MonoBehaviour
{

    public static bool cabeca1_morta;
    public static bool cabeca2_morta;
    public static bool cabeca3_morta;

    public static float vidaMaxima = 200f;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BarraVida();

        if((Cabeca01.morto && Cabeca02.morto && Cabeca03.morto && CabecaBase.morto) == true) {
            StartCoroutine("reviverGeral");
        }

        if(cabeca1_morta && cabeca2_morta && cabeca3_morta) {
            Destroy(BarraVidaMaior);
            CabecaBase.todosMortos = true;
            TiroPequenoChefao.modoHard = false;
        }
    }

    IEnumerator reviverGeral() {
        yield return new WaitForSeconds(4.0f);
        BarraVidaMaior.color = Color.red;
        TiroPequenoChefao.modoHard = true;
        SuperTiroChefao.modoHard = true;
        Cabeca01.renascer = true;
        Cabeca02.renascer = true;
        Cabeca03.renascer = true;
        CabecaBase.renascer = true;
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = (Cabeca01.vida_cabeca + Cabeca02.vida_cabeca + Cabeca03.vida_cabeca 
        + CabecaBase.vida_cabeca) / vidaMaxima; 
    }
}
