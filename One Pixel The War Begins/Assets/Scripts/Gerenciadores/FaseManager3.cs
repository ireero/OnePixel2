﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FaseManager3 : MonoBehaviour
{

    private string[] falas_chefao_portugues = {"Quem diria que você chegaria até aqui", "Tenho que dizer que estou impressionado pela sua força de vontade", 
    "Tenho que lhe contar uma coisa Imperador", "Esses monstros que você está matando são nada mais, nada menos do que cidadãos do seu Reino", 
    "Graças ao imperador Pixel Preto agora temos o poder que precisávamos", "Finalmente iremos deixar de ser a minoria", "Finalmente seremos nós a tomar grandes decisões", 
    "Sua sorte acaba aqui Imperador Pixel Branco, prepare-se para morrer", " ", "Chega de brincadeira, irei-lhe mostrar a força de todo um povo diminuído, amargurado e pronto para VINGANÇA!"}; // 9

    private string[] falas_chefao_ingles = {"Who would have thought you would get this far", "I have to say that I am impressed by your willpower", 
    "I have to tell you something, Emperor", "These monsters you are killing are nothing more and nothing less than citizens of your Kingdom", 
    "Thanks to Emperor Black Pixel we now have the power we needed", "Finally we will stop being the minority", "Finally we will be the ones making the big decisions", 
    "Your luck runs out here Emperor White Pixel, prepare to die", " ", "Enough fooling around, I will show you the strength of a whole people diminished, embittered and ready for VENGEANCE!"};

    private string[] falas_chefao_chines = {
    "谁能想到你竟然能走到这一步",
    "我不得不说，你的意志力令我印象深刻",
    "我有件事要告诉你，皇帝",
    "这些你所杀的怪物无非就是你王国的公民",
    "多亏黑像素皇帝，我们现在拥有了所需的力量",
    "终于，我们将不再是少数",
    "终于，我们将成为做出重大决策的人",
    "你的好运到此为止了，白像素皇帝，准备死吧",
    " ",
    "别再胡闹了，我将让你见识一个被削弱、充满怨恨且准备复仇的民族的力量！"
};

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar_portugues = "Pressione 'Q' para avançar";
    private string text_avancar_ingles = "Press 'Q' to advance";
    private string text_avancar_chines = "按下 'Q' 键以继续";

    public static int contagem_falas_3;

    // Vidas
    public Image BarraVida1;
    public Image vidinha1;

    public Image BarraVida2;
    public Image vidinha2;

    public Image BarraVida3;
    public Image vidinha3;

    public GameObject[] vidas;

    private int vida_chefao;

    public Image imagem;
    public GameObject painel_falas;

    public static bool pode_comecar_3;

    public Sprite chefao_ameacando;
    public Sprite chefao_raiva;
    public Sprite chefao_meia_vida;

    public Sprite icon_metade_da_vida;
    public Sprite icon_vida_normal;

    public GameObject painel_derrota;
    public AudioSource backSound;
    private int podeTocar;

    public AudioSource fala_personagens;
    public AudioSource back_void;

    private bool umaVezBack;

    public GameObject chefao;
    public GameObject escada;

    private Animator anim;

    public GameObject Fiora;
    private int fiora_valor;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase3 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase3");
        }

        if(GameManager.progresso <= 3) {
            GameManager.Instance.SalvarSit(4, "Progresso");
        }

        anim = GetComponent<Animator>();

        if(GameManager.fase3 == 1 || GameManager.fase3 == 0) {
            BarraVida1.sprite = icon_vida_normal;
            BarraVida2.sprite = icon_vida_normal;
            BarraVida3.sprite = icon_vida_normal;
            BarraVida1.color = Color.white;
            BarraVida2.color = Color.white;
            BarraVida3.color = Color.white;
            if(GameManager.sem_dialogos == 0) {
                fala_personagens.Play();
                back_void.Play();
                painel_falas.SetActive(true);
                pode_comecar_3 = false;
                contagem_falas_3 = 0;
                PlayerControle.conversando = true;
            } else {
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                backSound.Play();
                pode_comecar_3 = true;
            }
            umaVezBack = false;
            podeTocar = 0;
        } else {
            if(PlayerPrefs.HasKey("ZEROU")) {
                fiora_valor = PlayerPrefs.GetInt("ZEROU");
                if(fiora_valor == 1) {
                    anim.SetBool("cair_quadro", true);
                    PlayerPrefs.SetInt("ZEROU", 2);
                } else if(fiora_valor == 2) {
                    anim.SetBool("quadro_caido", true);
                    Fiora.SetActive(true);
                }
            }
            Destroy(chefao);
            escada.SetActive(true);
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            Destroy(vidas[0]);
            Destroy(vidas[1]);
            Destroy(vidas[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_3) {
            if(contagem_falas_3 != 7 && contagem_falas_3 != 9) {
                fala_personagens.Play();
            }
            contagem_falas_3++;
        }

        if(contagem_falas_3 <= 9 && contagem_falas_3 >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txtFalas.text = falas_chefao_portugues[contagem_falas_3];
                txtAvancar.text = text_avancar_portugues;
            } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                txtFalas.text = falas_chefao_chines[contagem_falas_3];
                txtAvancar.text = text_avancar_chines;
            } 
            else {
                txtFalas.text = falas_chefao_ingles[contagem_falas_3];
                txtAvancar.text = text_avancar_ingles;
            }
        }

        switch(contagem_falas_3) {
            case 3:
                imagem.sprite = chefao_ameacando;
                break;
            case 5:
                imagem.sprite = chefao_raiva;
                break;
            case 8:
                if(podeTocar <= 0) {
                    backSound.Play();
                    back_void.Stop();
                    podeTocar++;
                }
                PlayerControle.conversando = false;
                if(!pode_comecar_3) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                pode_comecar_3 = true;
                break; 
            case 9:
                if(podeTocar <= 1) {
                    fala_personagens.Play();
                    podeTocar++;
                }
                pode_comecar_3 = false;
                imagem.sprite = chefao_meia_vida;
                PlayerControle.conversando = true;
                painel_falas.SetActive(true);
                break;  
            case 10:
                PlayerControle.conversando = false;
                if(!pode_comecar_3) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                pode_comecar_3 = true;
                painel_falas.SetActive(false);
                break;             
        }

        vida_chefao = Chefao03.vida_chefao;
        switch(vida_chefao) {
            case 2:
                vidinha1.enabled = false;
                break;
            case 1:
                vidinha2.enabled = false;
                break;
            case 0:
                BarraVida1.sprite = icon_metade_da_vida;
                BarraVida2.sprite = icon_metade_da_vida;
                BarraVida3.sprite = icon_metade_da_vida;
                vidinha1.enabled = true;
                vidinha2.enabled = true;
                BarraVida1.color = Color.red;
                BarraVida2.color = Color.red;
                BarraVida3.color = Color.red;
                break;  
            case -1:
                Destroy(vidas[0]);
                break;
            case -2:
                Destroy(vidas[1]);
                break;
            case -3:
                backSound.Stop();
                if(!umaVezBack) {
                    GameManager.Instance.SalvarSit(2, "Fase3");
                    back_void.Play();
                    umaVezBack = true;
                }
                Destroy(vidas[2]);
                break;        
        }
    }

    public void PararTremedeira() {
        anim.SetBool("tremer_chao", false);
    }

    public void UnlockFiora() {
        Fiora.SetActive(true);
    }
}
