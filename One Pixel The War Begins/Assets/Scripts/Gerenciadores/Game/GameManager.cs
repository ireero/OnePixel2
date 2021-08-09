using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int fase1;
    private int fase2;
    private int fase3;
    private int fase4;
    private int fase5;
    private int fase6;
    private int fase7;
    private int fase8;
    private int fase9;
    private int fase10;
    private int tutorial;

    public static GameManager Instance {get; private set;}

    private void Awake() {

        // valor 0 = Nem chegou nela (Nem vai aparecer na tela de menu de fases)
        // valor 1 = Chegou nela (vai aparecer na tela de menu de fases)
        // valor 2 = Passou dela (Vai ser inicializada diferente)
        
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

        tutorial = PlayerPrefs.GetInt("Tutorial");
        
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
}
