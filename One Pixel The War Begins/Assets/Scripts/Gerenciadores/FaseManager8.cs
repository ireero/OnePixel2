using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager8 : MonoBehaviour
{
    private string[] falas_cientista_portugues = {"O... Oi Imperador", "Sinceramente não imaginei que você fosse chegar até aqui", "Faziam meses que você não pisava no castelo", 
    "Me diga como foi sua expedição em busca de novas terras?", "Não está muito bem-humorado não é mesmo?", "Entendo...", "Serei bem sincero com você imperador, eu não gosto de brigas", 
    "Então faça ai o que você quiser e vá para onde você quiser que eu não ligo", "Tome esse presente por me deixar ir, tenho certeza que vai-lhe ajudar bastante com os inimigos a frente"};

    private string[] falas_cientista_ingles = {"H... Hi Emperor", "I honestly did not imagine that you would get this far", "It has been months since you set foot in the castle", 
    "Tell me how was your expedition in search of new lands?", "Not in a very good mood are you?", "I see...", "I will be very honest with you Emperor, I do not like fighting", 
    "So do there what you want and go where you want and I don't care", "Take this gift for letting me go, I'm sure it will help you a lot with the enemies ahead"};

    private string[] falas_cientista_chines = {"陛...陛下", "说实话我真没料到你能走到这一步", "自从你踏入城堡已经过去数月了", 
    "告诉我你在寻找新土地的远征中有什么收获吗？", "看来心情不太好啊？", "我明白了...", "陛下恕我直言，我向来不喜争斗", 
    "所以您尽管按自己心意行动，我不会有任何干涉", "承蒙放行之谊，这件礼物定能助您应对前方强敌"};
    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar_portugues = "Pressione 'Q' para avançar";
    private string text_avancar_ingles = "Press 'Q' to advance";
    private string text_avancar_chines = "按下 'Q' 键以继续";

    public static int contagem_falas_8;

    public GameObject painel_falas;

    public static bool pode_comecar_8;
    public static bool jabateuUmPapo;

    private bool umaVezGanho;

    public GameObject cientista;
    public GameObject gatilho;
    public GameObject escada;

    public GameObject painel_instrucao;

    public AudioSource falaSom;
    public AudioSource achievemente;

    private int tocarMusica;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase8 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase8");
        }

        if(GameManager.progresso <= 8) {
            GameManager.Instance.SalvarSit(9, "Progresso");
        }

        if(GameManager.fase8 == 0 || GameManager.fase8 == 1) {
            tocarMusica = 0;
            umaVezGanho = false;
            pode_comecar_8 = false;
            if(GameManager.sem_dialogos == 0) {
                jabateuUmPapo = false;
                contagem_falas_8 = 0;
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
            } else {
                painel_instrucao.SetActive(true);
                PlayerControle.pet_ativado = true;
                PlayerPrefs.SetInt("Pet", 1);
                achievemente.Play();
                PlayerControle.conversando = true;
                jabateuUmPapo = true;
            }
        } else {
            PlayerControle.conversando = false;
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
            Destroy(cientista);
            Destroy(gatilho);
            escada.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }

        if(pode_comecar_8) {
            PlayerControle.conversando = true;
            if(tocarMusica == 0) {
                falaSom.Play();
                tocarMusica++;
            }
            painel_falas.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && pode_comecar_8) {
            if(contagem_falas_8 != 8) {
                falaSom.Play();
            }
            contagem_falas_8++;
        }

        if(contagem_falas_8 <= 8 && contagem_falas_8 >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txtFalas.text = falas_cientista_portugues[contagem_falas_8];
                txtAvancar.text = text_avancar_portugues;
            } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                txtFalas.text = falas_cientista_chines[contagem_falas_8];
                txtAvancar.text = text_avancar_chines;
            } 
            else {
                txtFalas.text = falas_cientista_ingles[contagem_falas_8];
                txtAvancar.text = text_avancar_ingles;
            }
        }

        if(contagem_falas_8 == 8) {
            if(!umaVezGanho) {
                painel_instrucao.SetActive(true);
                PlayerControle.pet_ativado = true;
                PlayerPrefs.SetInt("Pet", 1);
                achievemente.Play();
                umaVezGanho = true;
            }
        }

        if(contagem_falas_8 == 9) {
            painel_instrucao.SetActive(false);
            GameManager.Instance.SalvarSit(2, "Fase8");
            jabateuUmPapo = true;
            pode_comecar_8 = false;
            painel_falas.SetActive(false);
        }
    }
}
