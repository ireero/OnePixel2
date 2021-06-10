using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextoTutorial : MonoBehaviour
{
    private string[] instrucoes = {"One Pixel The War Begins", "Use as setas ou AWSD para se locomover", "Para pular bastar usar seta para cima ou W", 
    "Atire apertando a tecla Z ou o lado esquerdo do mouse", "Conjure uma plataforma apertando a tecla X ou o lado direito do mouse", 
    "Lembre-se que voce não pode conjurar plataformas estando no chão ou em outra plataforma", "Agora que você lembrou de tudo está na hora de acordar", 
    "Aliás pixel branco, você tem um reino para salvar"};

    public Text txtTutorial;
    private int contagem;

    private bool andou;
    private bool pulou;
    private bool atirou;
    private bool botou_plataforma;
    private bool comecou;
    
    void Start()
    {
        comecou = false;
        andou = false;
        pulou = false;
        atirou = false;
        botou_plataforma = false;
        contagem = 0;
    }

    // Update is called once per frame
    private void Update() {

        if(contagem <= 7 && contagem >= 0) {
            txtTutorial.text = instrucoes[contagem];
        } else if(contagem >= 8) {
            SceneManager.LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            contagem++;
        }
    }
}
