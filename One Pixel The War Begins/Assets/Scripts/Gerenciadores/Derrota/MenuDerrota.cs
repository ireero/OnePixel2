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

    private string[] frases_portugues = {"Você tem que aprender sozinho como passar dos chefões :)", "Continue a tentar... Continue a tentar...",
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
    
    private string[] frases_chines = {
    "你必须自己学会如何打败BOSS哦 :)", 
    "继续尝试……继续尝试……",
    "我在预告片里就警告过你这游戏会很难",
    "你能通关吗？",
    "我也不知道该说什么了",
    "我更喜欢猫，不过狗也不错",
    "我女朋友让我别把这个做进游戏里，她说'太无关紧要了'，你觉得呢？",
    "对我来说，外星人是存在的",
    "你觉得这游戏怎么样？去写个好评吧 :)", 
    "你要放弃吗？",
    "一切都会好的，我在这儿陪着你 :)",
    "小知识：像素角色没有设定性别",
    "小知识：游戏灵感来自另一款我因体量太大而取消开发的游戏",
    "小知识：所有黑白像素角色都患有全色盲症",
    "小知识：像素世界的名字叫'大像素'",
    "把这游戏推荐给你讨厌的朋友吧 ;)",
    "小知识：最初的创意是猫的世界，但我不会画猫",
    "我最喜欢的BOSS是倒数第二个，你呢？",
    "这是一款纯正的巴西游戏，我们为此自豪"
};

    public Text txt_perdeu;
    public Text txt_reiniciar;
    public Text txt_voltar_menu;

    private string text_perdeu_portugues = "Você Perdeu";
    private string text_reiniciar_portugues = "Reiniciar";
    private string text_voltar_menu_portugues = "Voltar ao Menu";

    private string text_perdeu_ingles = "You've lost";
    private string text_reiniciar_ingles = "Restart";
    private string text_voltar_menu_ingles = "Back to Menu";

    private string text_perdeu_chines = "你失去了";
    private string text_reiniciar_chines = "重新启动";
    private string text_voltar_menu_chines = "返回菜单";
    

    void Start()
    {
        valor_alet = Random.Range(0, 19);
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_frase.text = frases_portugues[valor_alet];
            txt_perdeu.text = text_perdeu_portugues;
            txt_reiniciar.text = text_reiniciar_portugues;
            txt_voltar_menu.text = text_voltar_menu_portugues;
        } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            txt_frase.text = frases_chines[valor_alet];
            txt_perdeu.text = text_perdeu_chines;
            txt_reiniciar.text = text_reiniciar_chines;
            txt_voltar_menu.text = text_voltar_menu_chines;
         } else {
            txt_frase.text = frases_ingles[valor_alet];
            txt_perdeu.text = text_perdeu_ingles;
            txt_reiniciar.text = text_reiniciar_ingles;
            txt_voltar_menu.text = text_voltar_menu_ingles;
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
