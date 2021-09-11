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
    "Te avisei dês do trailer que ia ser difícil", "Será que você consegue zerar o jogo?", "Sei o que falar mais não",
    "Eu prefiro gatos, mas cachorros são legais também.", "Minha namorada mandou não botar isso no jogo, segundo ela é muito 'Nada a ver', tu acha?", 
    "Pra mim aliens existem", "Ta gostando do jogo ae?, deixa uma review top lá :)", "Vai desistir?", "Vai ficar tudo bem, estou aqui para você :)", 
    "Curiosidade: Os Pixels não tem Sexo definido", "Curiosidade: A ideia do jogo veio de outro jogo que eu resolvi cancelar por ser longo de mais", "Curiosidade: Todos os Pixels Pretos e Brancos tem Acromatopsia", 
    "Curiosidade: O nome do mundo dos Pixels é Big Pixel", "Indique o jogo para amigos que você odeia ;)", "Curiosidade: A idéia inicial era um mundo de gatos, mas eu não sei desenhar eles", 
    "Meu Chefão favorito é o penúltimo, e o seu?", "Esse é um jogo totalmente Brasileiro e temos orgulho disso"};

    private string[] frases_ingles = {"You have to learn by yourself how to get past the bosses :)", "Keep on trying... Keep on trying...",
    "I warned you from the trailer that it would be difficult", "Can you beat the game?", "I know what else to say",
    "I prefer cats, but dogs are nice too.", "My girlfriend told me not to put it in the game, she says it's too 'Nothing to do', do you think?", 
    "For me aliens exist", "How do you like the game? leave a good review there :)", "Will you give up?", "It's going to be okay, I'm here for you :)", 
    "Curiosity: The Pixels have no defined gender", "Curiosity: The idea of the game came from another game that I decided to cancel because it was too long", "Curiosity: All Black and White Pixels have Achromatopsia", 
    "Curiosity: The name of the Pixel world is Big Pixel", "Indicate the game to friends you hate ;)", "Curiosity: The initial idea was a world of cats, but I don't know how to draw them", 
    "My favorite Boss is the penultimate one, what about yours?", "This is a totally Brazilian game and we are proud of it"};
    
    public Text txt_perdeu;
    public Text txt_reiniciar;
    public Text txt_voltar_menu;

    private string text_perdeu = "Você Perdeu";
    private string text_reiniciar = "Reiniciar";
    private string text_voltar_menu = "Voltar ao Menu";
    

    void Start()
    {
        valor_alet = Random.Range(0, 19);
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_frase.text = frases[valor_alet];
            txt_perdeu.text = text_perdeu;
            txt_reiniciar.text = text_reiniciar;
            txt_voltar_menu.text = text_voltar_menu;
        } else {
            txt_frase.text = frases_ingles[valor_alet];
        }
    }

    // Update is called once per frame
    void Update()
    {
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
