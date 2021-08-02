using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDerrota : MonoBehaviour
{   

    public Text txt_frase;

    private int valor_alet;

    private string[] frases = {"Você tem que aprender sozinho como passar dos chefões :)", "Continue a tentar... Continue a tentar...",
    "Te avisei dês do trailer que ia ser dificil", "Era uma vez um player ruinzinho (Brinks) ;)", "Sei o que falar mais não",
    "Eu prefiro gatos, mas cachorros são legais também.", "Minha namorada mandou não botar isso no jogo, segundo ela é muito 'Nada a ver', tu acha?", 
    "Pra mim aliens existem", "Ta gostando do jogo ae?, deixa uma review top lá :)", "Aqui jás uma frase cringe", "Vai ficar tudo bem, estou aqui para você :)"};

    void Start()
    {
        valor_alet = Random.Range(0, 11);
    }

    // Update is called once per frame
    void Update()
    {
        txt_frase.text = frases[valor_alet];
    }

    public void Reiniciar() {
        SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void IrMenu() {
        SceneLoader.Instance.LoadSceneAsync("Menu");
    }
}
