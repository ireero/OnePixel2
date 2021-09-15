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

    private string text_jogar = "Jogar";
    private string text_continuar = "Continuar";
    private string text_fases = "Fases";
    private string text_creditos = "Créditos";
    private string text_sair = "Sair";

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_jogar.text = text_jogar;
            txt_continuar.text = text_continuar;
            txt_fases.text = text_fases;
            txt_creditos.text = text_creditos;
            txt_sair.text = text_sair;
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
                SceneLoader.Instance.LoadSceneAsync("Fase1");
                break;
            case 2:
                SceneLoader.Instance.LoadSceneAsync("Fase2");
                break;
            case 3:
                SceneLoader.Instance.LoadSceneAsync("Fase3");
                break;
            case 4:
                SceneLoader.Instance.LoadSceneAsync("Fase4");
                break;
            case 5:
                SceneLoader.Instance.LoadSceneAsync("Fase5");
                break;
            case 6:
                SceneLoader.Instance.LoadSceneAsync("Fase6");
                break;
            case 7:
                SceneLoader.Instance.LoadSceneAsync("Fase7");
                break;
            case 8:
                SceneLoader.Instance.LoadSceneAsync("Fase8");
                break;
            case 9:
                SceneLoader.Instance.LoadSceneAsync("Fase9");
                break;
            case 10:
                SceneLoader.Instance.LoadSceneAsync("Fase10");
                break;                                    
        }
    }

    public void ReiniciarTudo() {
        som_click.Play();
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
    }

    public void Cancelar() {
        som_click.Play();
        painel_alerta_iniciar.SetActive(false);
    }
}
