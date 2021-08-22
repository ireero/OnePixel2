using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int fase1;
    public static int fase2;
    public static int fase3;
    public static int fase4;
    public static int fase5;
    public static int fase6;
    public static int fase7;
    public static int fase8;
    public static int fase9;
    public static int fase10;

    public static int comecou_game;

    public static int progresso;

    public static GameManager Instance {get; private set;}

    private void Awake() {

        // valor 0 = Nem chegou nela (Nem vai aparecer na tela de menu de fases)
        // valor 1 = Chegou nela (vai aparecer na tela de menu de fases)
        // valor 2 = Passou dela (Vai ser inicializada diferente)
        
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    public void SalvarSit(int sit, string nome_fase) {
        PlayerPrefs.SetInt(nome_fase, sit);
    }

    public void CarregarDados() {
        comecou_game = PlayerPrefs.GetInt("Comecou");
        fase1 = PlayerPrefs.GetInt("Fase1");
        fase2 = PlayerPrefs.GetInt("Fase2");
        fase3 = PlayerPrefs.GetInt("Fase3");
        fase4 = PlayerPrefs.GetInt("Fase4");
        fase5 = PlayerPrefs.GetInt("Fase5");
        fase6 = PlayerPrefs.GetInt("Fase6");
        fase7 = PlayerPrefs.GetInt("Fase7");
        fase8 = PlayerPrefs.GetInt("Fase8");
        fase9 = PlayerPrefs.GetInt("Fase9");
        fase10 = PlayerPrefs.GetInt("Fase10");
        progresso = PlayerPrefs.GetInt("Progresso");
        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
    }
}
