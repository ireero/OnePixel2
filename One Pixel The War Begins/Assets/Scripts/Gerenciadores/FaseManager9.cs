using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager9 : MonoBehaviour
{

    public Transform[] pontos_direita;

    public static float tempo_sobreviver;

    public Text txt_tempo;

    public GameObject painel_derrota;

    public GameObject portal_1;
    public GameObject portal_2;
    public GameObject portal_3;
    public GameObject portal_4;

    private bool segunda_parte;
    private bool umaVez;
    private bool umaVezTempo;

    public GameObject mao_1;
    public GameObject mao_2;

    public GameObject raio;
    public Transform parado_esquerdo;
    public Transform parado_direito;

    public static bool soltarRaio;

    private float cont_lobo;
    private int valor_aleatorio;
    private int valor_provisorio;

    public GameObject cabeca_lobo;

    public Image BarraVidaMaior;
    public Sprite chefao_meia_vida;

    void Start()
    {
        valor_provisorio = 0;
        valor_aleatorio = 0;
        cont_lobo = 0;
        soltarRaio = false;
        umaVezTempo = false;
        umaVez = false;
        segunda_parte = false;
        tempo_sobreviver = 75f;
    }

    // Update is called once per frame
    void Update()
    {

        txt_tempo.text = tempo_sobreviver.ToString("F0");

        if(tempo_sobreviver > 0) {
                tempo_sobreviver -= Time.deltaTime;
            }

        if(Parado.escondido == true && PlayerControle.parado == true && !soltarRaio) {
            Instantiate(raio, parado_direito.position, parado_direito.rotation);
            Instantiate(raio, parado_esquerdo.position, parado_esquerdo.rotation);
            soltarRaio = true;
        }

        if(segunda_parte) {

            if(!umaVezTempo) {
                BarraVidaMaior.sprite = chefao_meia_vida;
                BarraVidaMaior.color = Color.red;
                tempo_sobreviver = 75f;
                umaVezTempo = true;
            }

            if(tempo_sobreviver <= 50f && tempo_sobreviver > 25f) {
                mao_1.SetActive(true);
                mao_2.SetActive(true);
            } else if(tempo_sobreviver <= 25f && tempo_sobreviver > 0) {
                valor_aleatorio = Random.Range(0, 9);
                cont_lobo += Time.deltaTime;
                if(cont_lobo > 1f) {
                        if(valor_aleatorio == valor_provisorio) {
                            if(valor_aleatorio == 8) {
                                valor_aleatorio--;
                            } else if(valor_aleatorio == 0) {
                                valor_aleatorio++;
                            }
                        }
                        Instantiate(cabeca_lobo, pontos_direita[valor_aleatorio].position, pontos_direita[valor_aleatorio].rotation);
                        valor_provisorio = valor_aleatorio;
                        cont_lobo = 0;
                    }
                }

        } else {
            if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

            if(tempo_sobreviver <= 50f && tempo_sobreviver > 25f) {
                Portal.atira_ae_po = 2;
            } else if(tempo_sobreviver <= 25f && tempo_sobreviver >= 0f) {
                Portal.atira_ae_po = 3;
            } else if(tempo_sobreviver <= 0) {
                Portal.atira_ae_po = 5;
                if(!umaVez) {
                    umaVez = true;
                    StartCoroutine("segundaParte");
                }
            }
        }
    }

    IEnumerator segundaParte() {
        yield return new WaitForSeconds(2f);
        Chefao7.possuido = true;
        segunda_parte = true;
        portal_1.SetActive(true);
        portal_2.SetActive(true);
        portal_3.SetActive(true);
        portal_4.SetActive(true);
    }
}
