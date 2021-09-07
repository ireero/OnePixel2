using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager8 : MonoBehaviour
{
    private string[] falas_cientista = {"O... Oi Imperador", "Sinceramente não imaginei que você fosse chegar até aqui", "Faziam meses que você não pisava no castelo", 
    "Me diga como foi sua expedição em busca de novas terras?", "Não está muito bem-humorado não é mesmo?", "Entendo...", "Serei bem sincero com você imperador, eu não gosto de brigas", 
    "Então faça ai o que você quiser e vá para onde você quiser que eu não ligo", "Tome esse presente por me deixar ir, tenho certeza que vai-lhe ajudar bastante com os inimigos a frente"};

    private string[] falas_cientista_ingles = {"H... Hi Emperor", "I honestly did not imagine that you would get this far", "It has been months since you set foot in the castle", 
    "Tell me how was your expedition in search of new lands?", "Not in a very good mood are you?", "I see...", "I will be very honest with you Emperor, I do not like fighting", 
    "So do there what you want and go where you want and I don't care", "Take this gift for letting me go, I'm sure it will help you a lot with the enemies ahead"};

    public Text txtFalas;

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

        if(GameManager.progresso <= 7) {
            GameManager.Instance.SalvarSit(8, "Progresso");
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
            Destroy(cientista);
            Destroy(gatilho);
            escada.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(pode_comecar_8) {
            PlayerControle.conversando = true;
            if(tocarMusica == 0) {
                falaSom.Play();
                tocarMusica++;
            }
            painel_falas.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && pode_comecar_8) {
            contagem_falas_8++;
            if(contagem_falas_8 != 8) {
                falaSom.Play();
            }
        }

        if(contagem_falas_8 <= 8 && contagem_falas_8 >= 0) {
            txtFalas.text = falas_cientista[contagem_falas_8];
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
