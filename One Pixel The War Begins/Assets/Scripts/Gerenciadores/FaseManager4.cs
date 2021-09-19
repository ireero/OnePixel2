using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager4 : MonoBehaviour
{

    private string[] falas_meditador = {"Sinto muito imperador...", "Éramos aproximadamente 50 soldados de alto nível neste andar e mesmo assim fomos todos derrotados", 
    "Graças ao sacrifício de todos, nós ainda conseguimos eliminar 1 deles", "Eles se revoltaram do nada...", "Quando notamos o perigo já era tarde de mais, estávamos cercados",
    "Sinto muito meu senhor, espero que nossas mortes não sejam em vão", "O monstro que matamos deixou isso cair, espero que lhe ajude em algo", "Por favor meu imperador, salve nosso povo..."};

    private string[] falas_meditador_ingles = {"I'm sorry Emperor...", "There were approximately 50 high-level soldiers on this floor and yet we were all defeated", 
    "Thanks to the sacrifice of all of us, we still managed to eliminate 1 of them", "They revolted out of nowhere.", "When we noticed the danger it was already too late, we were surrounded",
    "I am sorry my lord, I hope our deaths are not in vain", "The monster we killed dropped this, I hope it helps you", "Please my emperor, save our people..."};

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar = "Pressione 'Q' para avançar";

    public static int contagem_falas_4;

    public Image imagem;
    public GameObject painel_falas;

    public Sprite meditador_olhao;
    public Sprite meditador_chorando;
    public Sprite meditador_raiva;

    public static bool pode_comecar_4;
    public static bool jaConversou;

    public AudioSource falaSom;
    public AudioSource som_morte;
    public AudioSource achievemente;

    private bool umaVez;
    private bool umaVezGanho;

    public GameObject meditador;
    public GameObject gatilho;
    public GameObject escada;

    public GameObject painel_instrucao;

    private int tocaSom;

    public Sprite fundo_ingles;
    public Image fundo;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase4 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase4");
        }

        if(GameManager.progresso <= 4) {
            GameManager.Instance.SalvarSit(5, "Progresso");
        }

        if(Application.systemLanguage == SystemLanguage.English) {
            fundo.sprite = fundo_ingles;
        }

        if(GameManager.fase4 == 1 || GameManager.fase4 == 0) {
            tocaSom = 0;
            umaVezGanho  = false;
            umaVez = false; 
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            pode_comecar_4 = false;
            contagem_falas_4 = 0;
            if(GameManager.sem_dialogos == 0) {
                jaConversou = false;
            } else {
                som_morte.Play();
                jaConversou = true;
            }
        } else {
            Destroy(gatilho);
            Destroy(meditador);
            escada.SetActive(true);
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(pode_comecar_4) {
            PlayerControle.conversando = true;
            if(tocaSom <= 0) {
                falaSom.Play();
                tocaSom++;
            }
            painel_falas.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && pode_comecar_4) {
            if(contagem_falas_4 != 5 && contagem_falas_4 != 7) {
                falaSom.Play();
            }
            contagem_falas_4++;
        }

        if(contagem_falas_4 <= 7 && contagem_falas_4 >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txtFalas.text = falas_meditador[contagem_falas_4];
                txtAvancar.text = text_avancar;
            } else {
                txtFalas.text = falas_meditador_ingles[contagem_falas_4];
            }
        }

        switch(contagem_falas_4) {
            case 1:
                imagem.sprite = meditador_olhao;
                break;
            case 3:
                imagem.sprite = meditador_raiva;
                break;
            case 5:
                imagem.sprite = meditador_chorando;
                break;
            case 6:
                imagem.sprite = meditador_olhao;
                if(!umaVezGanho) {
                    painel_instrucao.SetActive(true);
                    PlayerControle.jaPodePularDuas = true;
                    PlayerPrefs.SetInt("PularDuas", 1);
                    achievemente.Play();
                    umaVezGanho = true;
                }
                break;   
            case 7:
            painel_instrucao.SetActive(false);
                imagem.sprite= meditador_chorando;
                break;     
            case 8:
                if(!umaVez) {
                    GameManager.Instance.SalvarSit(2, "Fase4");
                    som_morte.Play();
                    umaVez = true;
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                    Destroy(gatilho);
                }
                Meditador.podeMorrer = true;
                pode_comecar_4 = false;
                painel_falas.SetActive(false);
                PlayerControle.conversando = false;
                break;            
        }
    }
}
