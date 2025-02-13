using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class OpcoesMenu : MonoBehaviour {
   public GameObject painel_de_pause;
   public Button BotaoRetornarAoJogo;
   [Space(20)]
   public Slider BarraVolume;
   public Toggle CaixaModoJanela;
   public Toggle CaixaSemDialogo;
   public Dropdown Resolucoes, Qualidades;
   public Button BotaoSalvarPref;
   [Space(20)]
   public Text textoVol;
   public string nomeCenaMenuPortugues = "Menu";
   public string nomeCenaMenuIngles = "Menu";
   public string nomeCenaMenuChines = "菜单";
   private float VOLUME;
   private int qualidadeGrafica, modoJanelaAtivo, resolucaoSalveIndex;
   private bool telaCheiaAtivada;
   private Resolution[] resolucoesSuportadas;

   public AudioSource som_click;

   public Text txt_voltar;
   public Text txt_modo_janela;
   public Text txt_sem_dialogos;
   public Text txt_salvar;
   public Text txt_opcoes;
   public Text txt_resetar;

   private string text_voltar_portugues = "Voltar";
   private string text_modo_janela_portugues = "Modo Janela";
   private string text_sem_dialogos_portugues = "Sem Diálogos";
   private string text_salvar_portugues = "Salvar Prefs";
   private string text_opces_portugues = "Opções";
   private string text_resetar_portugues = "Resetar Save";

   private string text_voltar_ingles = "Back";
   private string text_modo_janela_ingles = "Window mode";
   private string text_sem_dialogos_ingles = "No dialog";
   private string text_salvar_ingles = "Save Prefs";
   private string text_opces_ingles = "Options";
   private string text_resetar_ingles = "Reset Save";

   private string text_voltar_chines = "返回";
   private string text_modo_janela_chines = "窗口模式";
   private string text_sem_dialogos_chines = "无对话";
   private string text_salvar_chines = "保存首选项";
   private string text_opces_chines = "选项";
   private string text_resetar_chines = "重置保存";

   public GameObject painel_resetar;

   void Awake(){
      resolucoesSuportadas = Screen.resolutions;

      if(Application.systemLanguage == SystemLanguage.Portuguese) {
         txt_voltar.text = text_voltar_portugues;
         txt_modo_janela.text = text_modo_janela_portugues;
         txt_sem_dialogos.text = text_sem_dialogos_portugues;
         txt_salvar.text = text_salvar_portugues;
         txt_opcoes.text = text_opces_portugues;
         txt_resetar.text = text_resetar_portugues;
      } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
         txt_voltar.text = text_voltar_chines;
         txt_modo_janela.text = text_modo_janela_chines;
         txt_sem_dialogos.text = text_sem_dialogos_chines;
         txt_salvar.text = text_salvar_chines;
         txt_opcoes.text = text_opces_chines;
         txt_resetar.text = text_resetar_chines;
      } else {
         txt_voltar.text = text_voltar_ingles;
         txt_modo_janela.text = text_modo_janela_ingles;
         txt_sem_dialogos.text = text_sem_dialogos_ingles;
         txt_salvar.text = text_salvar_ingles;
         txt_opcoes.text = text_opces_ingles;
         txt_resetar.text = text_resetar_ingles;
      }
   }

   void Start () {
      ChecarResolucoes ();
      AjustarQualidades ();
      Time.timeScale = 1;
      AudioListener.volume = 1;
      BarraVolume.minValue = 0;
      BarraVolume.maxValue = 1;
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
      BotaoRetornarAoJogo.onClick = new Button.ButtonClickedEvent();
      BotaoSalvarPref.onClick = new Button.ButtonClickedEvent();
      //
      BotaoRetornarAoJogo.onClick.AddListener(() => painel_de_pause.SetActive(false));
      BotaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
   }
   void Update(){
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

   public void ResetarSave() {
      painel_resetar.SetActive(true);
   }

   //=========VOIDS DE SALVAMENTO==========//
   private void SalvarPreferencias(){
      som_click.Play();
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
}