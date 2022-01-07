using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject painel_alerta_iniciar;
    public AudioSource som_click;

    public Text txt_jogar;
    public Text txt_continuar;
    public Text txt_fases;
    public Text txt_creditos;
    public Text txt_sair;
    public Text txt_mostra_tempo;
    public Text txt_fala_red;

    public Text txt_tempo_jogo;

    private string text_jogar = "Jogar";
    private string text_continuar = "Continuar";
    private string text_fases = "Fases";
    private string text_creditos = "Créditos";
    private string text_sair = "Sair";
    private string text_mostra_tempo = "Tempo de Jogo:";
    private string text_fala_red = "Você conseguiu aprender um novo poder interessante!, tente apertar na tecla 'C' para ativar e desativar este poder que lhe fará dar o dobro de dano quando estiver ativado!";
    public static string segundos;
    private int conferindo = 0;

    public GameObject painel_pause;
    public GameObject painel_vermelho;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_jogar.text = text_jogar;
            txt_continuar.text = text_continuar;
            txt_fases.text = text_fases;
            txt_creditos.text = text_creditos;
            txt_sair.text = text_sair;
            txt_mostra_tempo.text = text_mostra_tempo;
            txt_fala_red.text = text_fala_red;
            segundos = " Minutos";
        } else {
            segundos = " Minutes";
        }

        if(PlayerPrefs.HasKey("TEMPO")) {
            GameManager.tempo_min = PlayerPrefs.GetInt("TEMPO");
        } else {
            GameManager.tempo_min = 0;
            PlayerPrefs.SetInt("TEMPO", 0);
        }

        txt_tempo_jogo.text = GameManager.tempo_min.ToString("F0") + segundos;

        if(PlayerPrefs.HasKey("REDVAR")) {
            conferindo = PlayerPrefs.GetInt("REDVAR");
            if(conferindo == 1) {
                painel_vermelho.SetActive(true);
                PlayerPrefs.SetInt("REDVAR", 2);
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey ("VOLUME")) {
            AudioListener.volume = PlayerPrefs.GetFloat ("VOLUME");
        }
    }

    public void Sair() {
        Application.Quit();
    }

    public void Jogar() {
        if(GameManager.comecou_game == 1) {
            painel_alerta_iniciar.SetActive(true);
        } else {
            SceneLoader.Instance.LoadSceneAsync("Tutorial");
        }
    }

    public void IrMenuFases() {
        SceneLoader.Instance.LoadSceneAsync("MenuFases");
    }

    public void IrCreditos() {
        SceneLoader.Instance.LoadSceneAsync("Creditos");
    }

    public void CarregarJogo() {
        switch(GameManager.progresso) {
            case 1:
                SceneLoader.Instance.LoadSceneAsync("Fase0");
                break;
            case 2:
                SceneLoader.Instance.LoadSceneAsync("Fase1");
                break;
            case 3:
                SceneLoader.Instance.LoadSceneAsync("Fase2");
                break;
            case 4:
                SceneLoader.Instance.LoadSceneAsync("Fase3");
                break;
            case 5:
                SceneLoader.Instance.LoadSceneAsync("Fase4");
                break;
            case 6:
                SceneLoader.Instance.LoadSceneAsync("Fase5");
                break;
            case 7:
                SceneLoader.Instance.LoadSceneAsync("Fase6");
                break;
            case 8:
                SceneLoader.Instance.LoadSceneAsync("Fase7");
                break;
            case 9:
                SceneLoader.Instance.LoadSceneAsync("Fase8");
                break;
            case 10:
                SceneLoader.Instance.LoadSceneAsync("Fase9");
                break;
            case 11:
                SceneLoader.Instance.LoadSceneAsync("Fase10");
                break;                                    
        }
    }

    public void ReiniciarTudo() {
        GameManager.Instance.SalvarSit(0, "Fase0");
        GameManager.Instance.SalvarSit(0, "Fase1");
        GameManager.Instance.SalvarSit(0, "Fase2");
        GameManager.Instance.SalvarSit(0, "Fase3");
        GameManager.Instance.SalvarSit(0, "Fase4");
        GameManager.Instance.SalvarSit(0, "Fase5");
        GameManager.Instance.SalvarSit(0, "Fase6");
        GameManager.Instance.SalvarSit(0, "Fase7");
        GameManager.Instance.SalvarSit(0, "Fase8");
        GameManager.Instance.SalvarSit(0, "Fase9");
        GameManager.Instance.SalvarSit(0, "Fase10");
        GameManager.Instance.SalvarSit(0, "Comecou");
        GameManager.Instance.SalvarSit(0, "Progresso");
        GameManager.Instance.SalvarSit(0, "Pet");
        GameManager.Instance.SalvarSit(0, "PularDuas");
        GameManager.Instance.CarregarDados();
        SceneLoader.Instance.LoadSceneAsync("Menu");
        GameManager.Instance.SalvarSit(0, "PONTO");
        GameManager.Instance.SalvarSit(0, "TEMPO");
        if(PlayerPrefs.HasKey("RED")) {
            GameManager.Instance.SalvarSit(0, "RED");
            PlayerPrefs.SetFloat("CONT_RED", 0);
            PlayerPrefs.SetFloat("CONT_VOLT_RED", 0);
        }
        if(PlayerPrefs.HasKey("REDVAR")) {
            GameManager.Instance.SalvarSit(0, "REDVAR");
        }
        GameManager.Instance.SalvarSit(0, "Fase7_5");
        GameManager.Instance.SalvarSit(0, "TEMPO_JOGO");
        PlayerPrefs.SetInt("RED_PAUSADO", 0);
        if(PlayerPrefs.HasKey("ZEROU")) {
            PlayerPrefs.SetInt("ZEROU", 0);
        }
    }

    public void AtivarOpcoes() {
        painel_pause.SetActive(true);
    }

    public void Cancelar() {
        painel_alerta_iniciar.SetActive(false);
    }

    public void CancelarOK() {
        painel_vermelho.SetActive(false);
    }
}
