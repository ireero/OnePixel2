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

    // Português (original)
    private string text_jogar_pt = "Jogar";
    private string text_continuar_pt = "Continuar";
    private string text_fases_pt = "Fases";
    private string text_creditos_pt = "Créditos";
    private string text_sair_pt = "Sair";
    private string text_mostra_tempo_pt = "Tempo de Jogo:";
    private string text_fala_red_pt = "Você conseguiu aprender um novo poder interessante!, tente apertar na tecla 'C' para ativar e desativar este poder que lhe fará dar o dobro de dano quando estiver ativado!";

    // English
    private string text_jogar_en = "Play";
    private string text_continuar_en = "Continue";
    private string text_fases_en = "Levels";
    private string text_creditos_en = "Credits";
    private string text_sair_en = "Exit";
    private string text_mostra_tempo_en = "Game Time:";
    private string text_fala_red_en = "You have learned a new interesting power! Try pressing the 'C' key to activate and deactivate this ability, which will double your damage when active!";

    // 简体中文 (Simplified Chinese)
    private string text_jogar_cn = "玩";
    private string text_continuar_cn = "继续";
    private string text_fases_cn = "关卡";
    private string text_creditos_cn = "制作人员";
    private string text_sair_cn = "退出";
    private string text_mostra_tempo_cn = "游戏时间：";
    private string text_fala_red_cn = "你成功学会了一个有趣的新能力！请尝试按 'C' 键来启用和禁用此能力，启用时你的伤害将翻倍！";    public static string segundos;
    private int conferindo = 0;

    public GameObject painel_pause;
    public GameObject painel_vermelho;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_jogar.text = text_jogar_pt;
            txt_continuar.text = text_continuar_pt;
            txt_fases.text = text_fases_pt;
            txt_creditos.text = text_creditos_pt;
            txt_sair.text = text_sair_pt;
            txt_mostra_tempo.text = text_mostra_tempo_pt;
            txt_fala_red.text = text_fala_red_pt;
            segundos = " Minutos";
        } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            txt_jogar.text = text_jogar_cn;
            txt_continuar.text = text_continuar_cn;
            txt_fases.text = text_fases_cn;
            txt_creditos.text = text_creditos_cn;
            txt_sair.text = text_sair_cn;
            txt_mostra_tempo.text = text_mostra_tempo_cn;
            txt_fala_red.text = text_fala_red_cn;
            segundos = " 秒钟";
         } else {
            txt_jogar.text = text_jogar_en;
            txt_continuar.text = text_continuar_en;
            txt_fases.text = text_fases_en;
            txt_creditos.text = text_creditos_en;
            txt_sair.text = text_sair_en;
            txt_mostra_tempo.text = text_mostra_tempo_en;
            txt_fala_red.text = text_fala_red_en;
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
        som_click.Play();
        Application.Quit();
    }

    public void Jogar() {
        som_click.Play();
        if(GameManager.comecou_game == 1) {
            painel_alerta_iniciar.SetActive(true);
        } else {
            SceneLoader.Instance.LoadSceneAsync("Tutorial");
        }
    }

    public void IrMenuFases() {
        som_click.Play();
        SceneLoader.Instance.LoadSceneAsync("MenuFases");
    }

    public void IrCreditos() {
        som_click.Play();
        SceneLoader.Instance.LoadSceneAsync("Creditos");
    }

    public void CarregarJogo() {
        som_click.Play();
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
        som_click.Play();
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
        GameManager.Instance.SalvarSit(0, "RED");
        GameManager.Instance.SalvarSit(0, "REDVAR");
        GameManager.Instance.SalvarSit(0, "Fase7_5");
        PlayerPrefs.SetFloat("CONT_RED", 0);
        PlayerPrefs.SetFloat("CONT_VOLT_RED", 0);
        GameManager.Instance.SalvarSit(0, "TEMPO_JOGO");
        PlayerPrefs.SetInt("RED_PAUSADO", 0);
        PlayerPrefs.SetInt("ZEROU", 0);
    }

    public void AtivarOpcoes() {
        som_click.Play();
        painel_pause.SetActive(true);
    }

    public void Cancelar() {
        som_click.Play();
        painel_alerta_iniciar.SetActive(false);
    }

    public void CancelarOK() {
        som_click.Play();
        painel_vermelho.SetActive(false);
    }
}
