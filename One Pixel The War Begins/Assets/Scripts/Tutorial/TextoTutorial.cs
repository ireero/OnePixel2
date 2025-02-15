using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextoTutorial : MonoBehaviour
{
    private string[] instrucoes = {"One Pixel The War Begins", "Use as setas ou AWSD para andar e pular", 
    "Atire apertando a tecla Z ou o lado esquerdo do mouse", "Conjure uma plataforma apertando a tecla X ou o lado direito do mouse, Lembre-se que voce não pode conjurar plataformas estando no chão ou em outra plataforma, também pode apagar a que você está em cima apertando SHIFT", "Apertando a tecla espaço ou o botão no meio do mouse você pode dar um Dash e avançar mais rápido, no ar pode ser usado apenas uma vez e no chão infinitas", 
    "Aqui você pode ver sua barra de vida", "Apertando Esc você pausa e despausa o jogo", "Agora que você aprendeu tudo está na hora de ir, Aliás você tem um reino para salvar"};

    private string[] instrucoes_ingles = {"One Pixel The War Begins", "Use the arrow keys or AWSD to walk and jump", 
    "Shoot by pressing the Z key or the left mouse button", "Conjure a platform by pressing the X key or the right mouse button, remember that you can't conjure platforms on the ground or on another platform, you can also delete the one you are on by pressing SHIFT", "By pressing the space key or the middle mouse button you can Dash and advance faster, in the air it can be used only once and on the ground infinitely", 
    "Here you can see your life bar", "By pressing Esc you pause and unpause the game", "Now that you've learned everything it's time to go, by the way you have a kingdom to save"};

    public Text txtTutorial;
    private int contagem;

    public Text pressioneQ;

    private string text_pressioneq = "Pressione 'Q' para avançar";

    private bool andou_direita;
    private bool andou_esquerda;
    private bool pulou;
    private bool abaixou;

    private bool atirou;
    private bool botou_plataforma;
    private bool deu_dash;

    public Image fundo_preto;

    public GameObject painel_controles;
    public GameObject painel_tiros;
    public GameObject painel_plataformas;
    public GameObject painel_dash;

    public GameObject vida_do_player;
    public GameObject seta_apontando;

    public GameObject painel_de_pause;
    public GameObject controles_pause;

    private bool controles;
    private bool tiros_controles;
    private bool controlou_plataforma;
    private bool dash_controles;

    private bool pausado;

    public Animator seta_direita;
    public Animator seta_esquerda;
    public Animator seta_cima;
    public Animator seta_baixo;

    public Animator Ddireita;
    public Animator Aesquerda;
    public Animator Wcima;
    public Animator Sbaixo;

    public Animator Ztiro;
    public Animator mouse_tiro;

    public Animator Xplataforma;
    public Animator mouse_plataforma;

    public Animator space_dash;
    public Animator botao_esq;

    private bool umaVez;
    
    void Start()
    {
        GameManager.Instance.CarregarDados();
        umaVez = false;
        pausado = false;
        deu_dash = false;
        dash_controles = false;
        controlou_plataforma = false;
        abaixou = false;
        controles = false;
        tiros_controles = false;
        andou_direita = false;
        andou_esquerda = false;
        pulou = false;
        atirou = false;
        botou_plataforma = false;
        contagem = 0;
        PlayerControle.conversando = true;
    }

    // Update is called once per frame
    private void Update() {

        if(contagem <= 7 && contagem >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txtTutorial.text = instrucoes[contagem];
                pressioneQ.text = text_pressioneq;
            } else {
                txtTutorial.text = instrucoes_ingles[contagem];
            }
        } 

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(Input.GetKeyDown(KeyCode.Escape)) {
            botao_esq.SetBool("apertando", true);
        }

        if(tiros_controles) {
            if(Input.GetKeyDown(KeyCode.Z)) {
                atirou = true;
                Ztiro.SetBool("apertando", true);
            } else if(Input.GetMouseButtonDown(0)) {
                atirou = true;
                mouse_tiro.SetBool("apertando", true);
            }
        }

        if(controlou_plataforma) {
            if(Input.GetKeyDown(KeyCode.X)) {
                botou_plataforma = true;
                Xplataforma.SetBool("apertando", true);
            } else if(Input.GetMouseButtonDown(1)) {
                botou_plataforma = true;
                mouse_plataforma.SetBool("apertando", true);
            }
        }

        if(dash_controles) {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(2)) {
                deu_dash = true;
                space_dash.SetBool("apertando", true);
            }
        }
        
        if(controles) {
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                pulou = true;
                seta_cima.SetBool("apertando", true);
            } else if(Input.GetKeyDown(KeyCode.W)) {
                pulou = true;
                Wcima.SetBool("apertando", true);
            }

            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                andou_direita = true;
                seta_direita.SetBool("apertando", true);
            } else if(Input.GetKeyDown(KeyCode.D)) {
                andou_direita = true;
                Ddireita.SetBool("apertando", true);
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                andou_esquerda = true;
                seta_esquerda.SetBool("apertando", true);
            } else if(Input.GetKeyDown(KeyCode.A)) {
                andou_esquerda = true;
                Aesquerda.SetBool("apertando", true);
            }

            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                abaixou = true;
                seta_baixo.SetBool("apertando", true);
            } else if(Input.GetKeyDown(KeyCode.S)) {
                abaixou = true;
                Sbaixo.SetBool("apertando", true);
            }
        }

        switch(contagem) {
            case 1:
                if(!pulou || !andou_direita || !andou_esquerda || !abaixou) {
                    pressioneQ.enabled = false;
                    controles = true;
                    painel_controles.SetActive(true);
                    PlayerControle.conversando = false;
                    PlayerControle.pode_mexer = true;
                } else {
                    controles = false;
                    pressioneQ.enabled = true;
                }
                break;
            case 2:
                if(!atirou) {
                    pressioneQ.enabled = false;
                    painel_controles.SetActive(false);
                    painel_tiros.SetActive(true);
                    tiros_controles = true;
                    PlayerControle.podeAtirar = true;
                } else {
                    tiros_controles = false;
                    pressioneQ.enabled = true;
                }
                break; 
            case 3:
                if(!botou_plataforma) {
                    pressioneQ.enabled = false;
                    painel_tiros.SetActive(false);
                    painel_plataformas.SetActive(true);
                    controlou_plataforma = true;
                } else {
                    controlou_plataforma = false;
                    pressioneQ.enabled = true;
                }
                break;
            case 4:
                if(!deu_dash) {
                    pressioneQ.enabled = false;
                    painel_plataformas.SetActive(false);
                    painel_dash.SetActive(true);
                    dash_controles = true;
                } else {
                    dash_controles = false;
                    pressioneQ.enabled  = true;
                }
                break;
            case 5:
                painel_dash.SetActive(false);
                vida_do_player.SetActive(true);
                seta_apontando.SetActive(true);
                break; 
            case 6:
                vida_do_player.SetActive(false);
                seta_apontando.SetActive(false);
                controles_pause.SetActive(true);
                break;
            case 7:
                fundo_preto.enabled = false;
                controles_pause.SetActive(false);
                break;   
            case 8:
                if(!umaVez) {
                    SceneLoader.Instance.LoadSceneAsync("Fase0"); 
                    umaVez = true;
                } 
                break;  
        }

        if(Input.GetKeyDown(KeyCode.Q) && !controles && !tiros_controles && !controlou_plataforma && !dash_controles && !pausado) {
            contagem++;
        }
    }
}
