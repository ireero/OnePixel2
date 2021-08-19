using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager4 : MonoBehaviour
{

    private string[] falas_meditador = {"Sinto muito imperador....", "Eramos aproximadamente 50 soldados de alto nível e mesmo assim fomos tods derrotados", 
    "Graças ao sacrificio de todos nós ainda conseguimos eliminar 1 deles", "Eles se revoltaram do nada.....", "Quando notamos o perigo já era tarde de mais, estavámos sercados",
    "Sinto muito meu senhor, espero que nossas mortes não sejam em vão", "O monstro que matamos deixou isso cair, espero que lhe ajude em algo", "Por favor meu imperador, salve nosso povo......"};

    public Text txtFalas;

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

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase4 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase4");
        }

        if(GameManager.progresso <= 3) {
            GameManager.Instance.SalvarSit(4, "Progresso");
        }

        if(GameManager.fase4 == 1 || GameManager.fase4 == 0) {
            umaVezGanho  = false;
            umaVez = false; 
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            pode_comecar_4 = false;
            jaConversou = false;
            contagem_falas_4 = 0;
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

        if(pode_comecar_4) {
            PlayerControle.conversando = true;
            painel_falas.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && pode_comecar_4) {
            falaSom.Play();
            contagem_falas_4++;
        }

        if(contagem_falas_4 <= 7 && contagem_falas_4 >= 0) {
            txtFalas.text = falas_meditador[contagem_falas_4];
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
                    PlayerControle.jaPodePularDuas = true;
                    PlayerPrefs.SetInt("PularDuas", 1);
                    achievemente.Play();
                    umaVezGanho = true;
                }
                PlayerControle.jaPodePularDuas = true;
                break;   
            case 7:
                imagem.sprite= meditador_chorando;
                break;     
            case 8:
                if(!umaVez) {
                    GameManager.Instance.SalvarSit(2, "Fase4");
                    som_morte.Play();
                    umaVez = true;
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                Meditador.podeMorrer = true;
                pode_comecar_4 = false;
                painel_falas.SetActive(false);
                PlayerControle.conversando = false;
                break;            
        }
    }
}
