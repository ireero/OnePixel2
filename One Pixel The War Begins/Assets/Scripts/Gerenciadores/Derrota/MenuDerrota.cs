using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDerrota : MonoBehaviour
{   

    public Text txt_frase;

    private int valor_alet;

    public AudioSource som_click;

    private string[] frases = {"Você tem que aprender sozinho como passar dos chefões :)", "Continue a tentar... Continue a tentar...",
    "Te avisei dês do trailer que ia ser dificil", "Era uma vez um player ruinzinho (Brinks) ;)", "Sei o que falar mais não",
    "Eu prefiro gatos, mas cachorros são legais também.", "Minha namorada mandou não botar isso no jogo, segundo ela é muito 'Nada a ver', tu acha?", 
    "Pra mim aliens existem", "Ta gostando do jogo ae?, deixa uma review top lá :)", "Aqui jás uma frase cringe", "Vai ficar tudo bem, estou aqui para você :)", 
    "Curiosidade: Os Pixels não tem Sexo definido", "Curiosidade: A idéia do jogo veio de outro jogo que eu resolvi cancelar por ser longo de mais", "Curiosidade: Todos os Pixels Pretos e Brancos tem Acromatopsia", 
    "Curiosidade: O nome do mundo dos Pixels é Big Pixel", "Indique o jogo para amigos que você odeia ;)", "Curiosidade: A idéia inicial era um mundo de gatos mas eu não sei desenhar eles", 
    "Curiosidade: O primeiro One Pixel foi o primeiro jogo que criei na minha vida"};

    void Start()
    {
        valor_alet = Random.Range(0, 18);
    }

    // Update is called once per frame
    void Update()
    {
        txt_frase.text = frases[valor_alet];
    }

    public void Reiniciar() {
        som_click.Play();
        GameManager.Instance.CarregarDados();
        SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void IrMenu() {
        som_click.Play();
        SceneLoader.Instance.LoadSceneAsync("Menu");
        Time.timeScale = 1;
    }
}
