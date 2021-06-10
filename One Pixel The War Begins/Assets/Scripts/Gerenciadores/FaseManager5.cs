using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager5 : MonoBehaviour
{
    public GameObject moeda_risada;

    public Transform ponto_baixo;

    private float contador;
    private bool umaVez;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    private float vida_maxima = 50f;

    void Start()
    {
        umaVez = false;
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

        BarraVida();

        if(Chefao04.tirosDados == 0) {
            umaVez = false;
        }

        if(Chefao04.tirosDados >= 8 && !umaVez) {
                Instantiate(moeda_risada, ponto_baixo.position, ponto_baixo.rotation);
                umaVez = true;
        }

        if(Chefao04.vida_chefao == 0) {
            Destroy(BarraVidaMaior);
        } else if(Chefao04.vida_chefao <= 25 && Chefao04.vida_chefao > 0) {
            TiroPequenoChefao.modoHard = true;
            BarraVidaMaior.color = Color.red;
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao04.vida_chefao / vida_maxima;
    }
}
