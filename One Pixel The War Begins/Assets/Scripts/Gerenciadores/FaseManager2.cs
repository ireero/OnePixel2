﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager2 : MonoBehaviour
{

    private string[] falas_chefao = {"Vá em bora agora!", "Aproveite em quanto nosso Pai dorme e suma!", "Você vai morrer!", " ", 
    "Meus filhos morreram porque fui fraco de mais...", "Meu senhor este castelo é o seu túmulo, VOCÊ VAI MORRER!"};

    private string[] falas_chefao_ingles = {"Go away now!", "Enjoy while our Father sleeps and be gone!", "You are going to die!", " ", 
    "My children died because I was too weak...", "My lord, this castle is your tomb, YOU WILL DIE!"};

    public Text txtFalas;

    public static int contagem_falas_2;

    public static bool cabeca1_morta;
    public static bool cabeca2_morta;
    public static bool cabeca3_morta;
    public static bool cabeca4_morta;

    public static float vidaMaxima = 200f;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public Image imagem;
    public GameObject painel_falas;

    public static bool pode_comecar;

    public Sprite cara_cabeca2;
    public Sprite cara_cabeca3;

    public Sprite cabeca_base_normal;
    public Sprite cabeca_base_vingativa;

    public Sprite icon_meia_vida;

    public GameObject painel_derrota;

    public AudioSource back;
    private int tocaSom;
    public AudioSource som_fala;

    public AudioSource back_void;

    public GameObject chefao;
    public GameObject escada;

    public GameObject vida_chefao;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase2 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase2");
        }

        if(GameManager.progresso <= 1) {
            GameManager.Instance.SalvarSit(2, "Progresso");
        }

        if(GameManager.fase2 == 1 || GameManager.fase2 == 0) {
            som_fala.Play();
            back_void.Play();
            tocaSom = 0;
            pode_comecar = false;
            contagem_falas_2 = 0;
            painel_falas.SetActive(true);
            TiroPequenoChefao.modoHard = false;
            TiroRedondo.modoHardRedondo = false;
        } else {
            escada.SetActive(true);
            Destroy(chefao);
            BarraVidaMaior.enabled = false;
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(contagem_falas_2 <= 5 && contagem_falas_2 >= 0) {
            txtFalas.text = falas_chefao[contagem_falas_2];
            }

            switch(contagem_falas_2) {
                case 1:
                    imagem.sprite = cara_cabeca2;
                    break;
                case 2:
                     imagem.sprite = cara_cabeca3;
                     break;
                case 3:
                    if(tocaSom <= 0) {
                        back_void.Stop();
                        back.Play();
                        tocaSom += 1;
                    }
                    PlayerControle.conversando = false;
                    if(!pode_comecar) {
                        PlayerControle.pode_mexer = true;
                        PlayerControle.podeAtirar = true;
                    }
                    pode_comecar = true;
                    painel_falas.SetActive(false);
                    break;
                case 4:
                    back.Stop();
                    back_void.Play();
                    if(tocaSom <= 1) {
                        som_fala.Play();
                        tocaSom++;
                    }
                    PlayerControle.conversando = true;
                    imagem.sprite = cabeca_base_normal;
                    painel_falas.SetActive(true); 
                    break; 
                case 5:
                    imagem.sprite = cabeca_base_vingativa; 
                    break;
                case 6:
                    PlayerControle.conversando = false;
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                    CabecaBase.todosMortos = true;
                    painel_falas.SetActive(false);
                    break;                  
            }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar) {
            if(contagem_falas_2 != 2 && contagem_falas_2 != 5) {
                som_fala.Play();
            }
            contagem_falas_2++;
        }

        BarraVida();

        if((Cabeca01.locaute && Cabeca02.locaute && Cabeca03.locaute && CabecaBase.morto) == true) {
            BarraVidaMaior.sprite = icon_meia_vida;
            StartCoroutine("reviverGeral");
        }

        if((cabeca1_morta && cabeca2_morta && cabeca3_morta & cabeca4_morta) && pode_comecar) {
            Destroy(vida_chefao);
            GameManager.Instance.SalvarSit(2, "Fase2");
            contagem_falas_2 = 4;
            TiroRedondo.modoHardRedondo = false;
            TiroPequenoChefao.modoHard = false;
            pode_comecar = false;
        }
    }

    IEnumerator reviverGeral() {
        yield return new WaitForSeconds(4.0f);
        BarraVidaMaior.color = Color.red;
        TiroPequenoChefao.modoHard = true;
        SuperTiroChefao.modoHard = true;
        TiroRedondo.modoHardRedondo = true;

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
