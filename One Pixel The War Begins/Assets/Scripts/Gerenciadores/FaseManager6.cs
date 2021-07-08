using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager6 : MonoBehaviour
{
    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public Sprite icon_meia_vida_ne;

    private float vida_maxima = 600f;

    public Transform spawn_1;
    public Transform spawn_2;
    public Transform spawn_3;
    public Transform spawn_4;
    public Transform spawn_5;
    public Transform spawn_6;

    private int valor_alet;

    public static bool podeCair;

    public GameObject tijolo_caindo;

    private float contador;

    private float tempo_de_cair;

    void Start()
    {
        tempo_de_cair = 1.5f;
        contador = 0;
        podeCair = false;
        valor_alet = 0;
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;
    }

    // Update is called once per frame
    void Update()
    {
       BarraVida(); 

       contador += Time.deltaTime;

       if(Chefao05.vida <= 0) {
           Destroy(BarraVidaMaior);
       } else if(Chefao05.vida <= 300 && Chefao05.vida > 0) {
           tempo_de_cair = 0.85f;
           BarraVidaMaior.sprite = icon_meia_vida_ne;
           BarraVidaMaior.color = Color.red;
       }

       valor_alet = Random.Range(1, 7);

       if(podeCair && contador >= tempo_de_cair) {
           contador = 0;
            switch(valor_alet) {
                case 1:
                    Instantiate(tijolo_caindo, spawn_1.position, spawn_1.rotation);
                    break;
                case 2:
                    Instantiate(tijolo_caindo, spawn_2.position, spawn_2.rotation);
                    break;
                case 3:
                    Instantiate(tijolo_caindo, spawn_3.position, spawn_3.rotation);
                    break;    
                case 4:
                    Instantiate(tijolo_caindo, spawn_4.position, spawn_4.rotation);
                    break;    
                case 5:
                    Instantiate(tijolo_caindo, spawn_5.position, spawn_5.rotation);
                    break;    
                case 6:
                    Instantiate(tijolo_caindo, spawn_6.position, spawn_6.rotation);
                    break;    
            }
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao05.vida / vida_maxima;
    }
}
