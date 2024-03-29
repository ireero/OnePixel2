using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PauseMenu : MonoBehaviour {
   public GameObject painel_de_pause;
   public Button BotaoRetornarAoJogo,BotaoOpcoes,BotaoVoltarAoMenu;
   [Space(20)]
   public Slider BarraVolume;
   public Toggle CaixaModoJanela;
   public Toggle CaixaSemDialogo;
   public Dropdown Resolucoes, Qualidades;
   public Button BotaoReiniciar, BotaoVoltar, BotaoSalvarPref;
   [Space(20)]
   public Text textoVol;
   public string nomeCenaMenu = "Menu";
   private float VOLUME;
   private int qualidadeGrafica, modoJanelaAtivo, resolucaoSalveIndex;
   private bool telaCheiaAtivada, menuParte1Ativo, menuParte2Ativo;
   private Resolution[] resolucoesSuportadas;

   public Text txt_reiniciar;
   public Text txt_jogar;
   public Text txt_opcoes;
   public Text txt_voltar_menu;
   public Text txt_modo_janela;
   public Text txt_sem_dialogos;
   public Text txt_voltar;
   public Text txt_salvar;

   public string text_reiniciar = "Reiniciar";
   private string text_jogar = "Jogar";
   private string text_opcoes = "Opções";
   private string text_voltar_menu = "Voltar ao Menu";
   private string text_modo_janela = "Modo Janela";
   private string text_sem_dialogos = "Sem Diálogos";
   private string text_voltar = "Voltar";
   private string text_salvar = "Salvar Prefs";

   void Awake(){
      resolucoesSuportadas = Screen.resolutions;

      if(Application.systemLanguage == SystemLanguage.Portuguese) {
         txt_reiniciar.text = text_reiniciar;
         txt_jogar.text = text_jogar;
         txt_opcoes.text = text_opcoes;
         txt_voltar_menu.text = text_voltar_menu;
         txt_modo_janela.text = text_modo_janela;
         txt_sem_dialogos.text = text_sem_dialogos;
         txt_voltar.text = text_voltar;
         txt_salvar.text = text_salvar;
      }
   }

   void Start () {
      Opcoes (false,false);
      ChecarResolucoes ();
      AjustarQualidades ();
      Time.timeScale = 1;
      AudioListener.volume = 1;
      BarraVolume.minValue = 0;
      BarraVolume.maxValue = 1;
      menuParte1Ativo = menuParte2Ativo = false;
      if (PlayerPrefs.HasKey ("RESOLUCAO")) {
         int numResoluc = PlayerPrefs.GetInt ("RESOLUCAO");
         if (resolucoesSuportadas.Length <= numResoluc) {
            PlayerPrefs.DeleteKey ("RESOLUCAO");
         }
      }
      //=============== SAVES===========//
      if (PlayerPrefs.HasKey ("VOLUME")) {
         VOLUME = PlayerPrefs.GetFloat ("VOLUME");
         BarraVolume.value = VOLUME;
      } else {
         PlayerPrefs.SetFloat ("VOLUME", 1);
         BarraVolume.value = 1;
      }
      //=============MODO JANELA===========//
      if (PlayerPrefs.HasKey ("modoJanela")) {
         modoJanelaAtivo = PlayerPrefs.GetInt ("modoJanela");
         if (modoJanelaAtivo == 1) {
            Screen.fullScreen = false;
            CaixaModoJanela.isOn = true;
         } else {
            Screen.fullScreen = true;
            CaixaModoJanela.isOn = false;
         }
      } else {
         modoJanelaAtivo = 0;
         PlayerPrefs.SetInt ("modoJanela", modoJanelaAtivo);
         CaixaModoJanela.isOn = false;
         Screen.fullScreen = true;
      }
      //========RESOLUCOES========//
      if (modoJanelaAtivo == 1) {
         telaCheiaAtivada = false;
      } else {
         telaCheiaAtivada = true;
      }
      if (PlayerPrefs.HasKey ("RESOLUCAO")) {
         resolucaoSalveIndex = PlayerPrefs.GetInt ("RESOLUCAO");
         Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
         Resolucoes.value = resolucaoSalveIndex;
      } else {
         resolucaoSalveIndex = (resolucoesSuportadas.Length -1);
         Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
         PlayerPrefs.SetInt ("RESOLUCAO", resolucaoSalveIndex);
         Resolucoes.value = resolucaoSalveIndex;
      }

      //=======SEM DIALOGOS=====//
      if(PlayerPrefs.HasKey("semdialogos")) {
         GameManager.sem_dialogos = PlayerPrefs.GetInt("semdialogos");
         if(GameManager.sem_dialogos == 1) {
            CaixaSemDialogo.isOn = true;
         } else {
            CaixaSemDialogo.isOn = false;
         }
      } else {
         GameManager.sem_dialogos = 0;
         PlayerPrefs.SetInt("semdialogos", GameManager.sem_dialogos);
         CaixaSemDialogo.isOn = false;
      }

      //=========QUALIDADES=========//
      if (PlayerPrefs.HasKey ("qualidadeGrafica")) {
         qualidadeGrafica = PlayerPrefs.GetInt ("qualidadeGrafica");
         QualitySettings.SetQualityLevel(qualidadeGrafica);
         Qualidades.value = qualidadeGrafica;
      } else {
         QualitySettings.SetQualityLevel((QualitySettings.names.Length-1));
         qualidadeGrafica = (QualitySettings.names.Length-1);
         PlayerPrefs.SetInt ("qualidadeGrafica", qualidadeGrafica);
         Qualidades.value = qualidadeGrafica;
      }
      // =========SETAR BOTOES==========//
      BotaoVoltarAoMenu.onClick = new Button.ButtonClickedEvent();
      BotaoOpcoes.onClick = new Button.ButtonClickedEvent();
      BotaoRetornarAoJogo.onClick = new Button.ButtonClickedEvent();
      BotaoVoltar.onClick = new Button.ButtonClickedEvent();
      BotaoSalvarPref.onClick = new Button.ButtonClickedEvent();
      BotaoReiniciar.onClick = new Button.ButtonClickedEvent();
      //
      BotaoReiniciar.onClick.AddListener(() => ReiniciarCena());
      BotaoVoltarAoMenu.onClick.AddListener(() => VoltarAoMenu());
      BotaoOpcoes.onClick.AddListener(() => Opcoes(false,true));
      BotaoRetornarAoJogo.onClick.AddListener(() => Opcoes(false,false));
      BotaoVoltar.onClick.AddListener(() => Opcoes(true,false));
      BotaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
   }
   void Update(){
      if (Input.GetKeyDown (KeyCode.Escape)) {
         PlayerControle.podeAtirar = false;
         PlayerControle.pode_mexer = false;
         if (menuParte1Ativo == false && menuParte2Ativo == false) {
            menuParte1Ativo = true;
            menuParte2Ativo = false;
            Opcoes (true, false);
            Time.timeScale = 0;
            AudioListener.volume = 0;
         } else if (menuParte1Ativo == true && menuParte2Ativo == false) {
            menuParte1Ativo = menuParte2Ativo = false;
            Opcoes (false, false);
            Time.timeScale = 1;
            AudioListener.volume = VOLUME;
         }
         else if (menuParte1Ativo == false && menuParte2Ativo == true) {
            menuParte1Ativo = true;
            menuParte2Ativo = false;
            Opcoes (true, false);
            Time.timeScale = 0;
            AudioListener.volume = 0;
         }
      }
      if (menuParte1Ativo == true || menuParte2Ativo == true) {
         painel_de_pause.SetActive(true);
      } else {
         painel_de_pause.SetActive(false);
      }
   }
   //=========VOIDS DE CHECAGEM==========//
   private void ChecarResolucoes(){
      Resolution[] resolucoesSuportadas = Screen.resolutions;
      Resolucoes.options.Clear ();
      for(int y = 0; y < resolucoesSuportadas.Length; y++){
         Resolucoes.options.Add(new Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height });
      }
      Resolucoes.captionText.text = "Resolucao";
   }
   private void AjustarQualidades(){
      string[] nomes = QualitySettings.names;
      Qualidades.options.Clear ();
      for(int y = 0; y < nomes.Length; y++){
         Qualidades.options.Add(new Dropdown.OptionData() { text = nomes[y] });
      }
      Qualidades.captionText.text = "Qualidade";
   }
   private void Opcoes(bool ativarOP, bool ativarOP2){
      BotaoReiniciar.gameObject.SetActive(ativarOP);
      BotaoVoltarAoMenu.gameObject.SetActive (ativarOP);
      BotaoOpcoes.gameObject.SetActive (ativarOP);
      BotaoRetornarAoJogo.gameObject.SetActive (ativarOP);
      //
      textoVol.gameObject.SetActive (ativarOP2);
      BarraVolume.gameObject.SetActive (ativarOP2);
      CaixaModoJanela.gameObject.SetActive (ativarOP2);
      CaixaSemDialogo.gameObject.SetActive(ativarOP2);
      Resolucoes.gameObject.SetActive (ativarOP2);
      Qualidades.gameObject.SetActive (ativarOP2);
      BotaoVoltar.gameObject.SetActive (ativarOP2);
      BotaoSalvarPref.gameObject.SetActive (ativarOP2);
      if (ativarOP == true && ativarOP2 == false) {
         menuParte1Ativo = true;
         menuParte2Ativo = false;
      }
      else if (ativarOP == false && ativarOP2 == true) {
         menuParte1Ativo = false;
         menuParte2Ativo = true;
      }
      else if (ativarOP == false && ativarOP2 == false) {
         if(PlayerControle.conversando == false) {
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
         }
         menuParte1Ativo = false;
         menuParte2Ativo = false;
         Time.timeScale = 1;
         AudioListener.volume = VOLUME;
      }
   }
   //=========VOIDS DE SALVAMENTO==========//
   private void SalvarPreferencias(){
      if (CaixaModoJanela.isOn == true) {
         modoJanelaAtivo = 1;
         telaCheiaAtivada = false;
      } else {
         modoJanelaAtivo = 0;
         telaCheiaAtivada = true;
      }

      if(CaixaSemDialogo.isOn == true) {
         GameManager.sem_dialogos = 1;
      } else {
         GameManager.sem_dialogos = 0;
      }

      PlayerPrefs.SetFloat ("VOLUME", BarraVolume.value);
      PlayerPrefs.SetInt ("qualidadeGrafica", Qualidades.value);
      PlayerPrefs.SetInt ("modoJanela", modoJanelaAtivo);
      PlayerPrefs.SetInt ("semdialogos", GameManager.sem_dialogos);
      PlayerPrefs.SetInt ("RESOLUCAO", Resolucoes.value);
      resolucaoSalveIndex = Resolucoes.value;
      AplicarPreferencias ();
   }
   private void AplicarPreferencias(){
      VOLUME = PlayerPrefs.GetFloat ("VOLUME");
      QualitySettings.SetQualityLevel(PlayerPrefs.GetInt ("qualidadeGrafica"));
      Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
   }
   private void VoltarAoMenu(){
      SceneLoader.Instance.LoadSceneAsync(nomeCenaMenu);
      AudioListener.volume = VOLUME;
      Time.timeScale = 1;
   }

   private void ReiniciarCena() {
      SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
      AudioListener.volume = VOLUME;
      Time.timeScale = 1;
   }
}