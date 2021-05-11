using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager2 : MonoBehaviour
{

    public static bool cabeca1_morta;
    public static bool cabeca2_morta;
    public static bool cabeca3_morta;
    public static bool cabeca4_morta;

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

        if((Cabeca01.locaute && Cabeca02.locaute && Cabeca03.locaute && CabecaBase.morto) == true) {
            StartCoroutine("reviverGeral");
        }

        if(cabeca1_morta && cabeca2_morta && cabeca3_morta & cabeca4_morta) {
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

        Cabeca01.podeRenascer = true;
        Cabeca01.locaute = false;

        Cabeca02.podeRenascer = true;
        Cabeca02.locaute = false;
        
        Cabeca03.podeRenascer = true;
        Cabeca03.locaute = false;

        CabecaBase.renascer = true;
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = (Cabeca01.vida_cabeca + Cabeca02.vida_cabeca + Cabeca03.vida_cabeca 
        + CabecaBase.vida_cabeca) / vidaMaxima; 
    }
}
