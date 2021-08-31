using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager10 : MonoBehaviour
{

    private string[] falas_pixel_preto = {"...", "Olá", "Não esperava vê-lo de novo", "Ao menos não nesta situação...", "Sabe, eu juro que tentei evitar tudo isso", "Eu juro que...", 
    "Deixa, não à nenhuma explicação que justifique esse massacre", "Infelizmente eu não tenho como voltar atrás", "É uma pena todo um povo sofrer por decisões de seus governantes, você não acha?", 
    "É uma pena um grupo específico ser diminuído", "É uma pena que após anos de convivência em harmonia do nada viramos rivais", "Não temos mais representante no trono", 
    "Não temos mais nenhuma voz", "Não somos mais todos iguais", "Somos constantemente ameaçados", "Vivemos com medo de sofrer um ataque", "Dormimos com medo de nunca acordar", "Acordamos com medo de sofrer extermínio", 
    "E um dia simplesmente cansamos de toda humilhação e resolvemos revidar...", "Desculpe, mas chegou a hora de você descansar."};

    private string[] falas_pixel_preto_ingles = {"...", "Hello", "I didn't expect to see him again", "At least not in this situation...", "You know, I swear I tried to avoid all this", "I swear...", 
    "Leave it, there is no explanation that justifies this massacre", "Unfortunately I have no way back", "It is a shame that a whole people suffers because of the decisions of its rulers, don't you think?", 
    "It is a pity that a specific group is diminished", "It is a pity that after years of living together in harmony we suddenly become rivals", "We no longer have a representative on the throne", 
    "We no longer have any voice", "We are no longer all the same", "We are constantly threatened", "We live in fear of having an attack", "We sleep with the fear of never waking up", "We wake up in fear of extermination", 
    "And one day we simply got tired of all the humiliation and decided to fight back...", "Sorry, but it is time for you to rest."};

    public GameObject bolaFogo;
    private float delayTiro;

    public Transform[] spawn_cima;
    private int valor_aleatorio;

    private float contador;

    public static bool pode_comecar_10;

    public Image img_carinha;
    public Text txt_falas;

     public static int contagem_falas_10;

    public GameObject painel_conversas;

    public Image BarraVidaMaior;
    public Image vida_restante;

    public Sprite cara_transformado;

    private float vida_maxima = 500f;

    public GameObject paredona;

    public Transform lado_esquerdo;
    public Transform lado_direito;

    public GameObject painel_derrota;

    public Sprite[] sprites_caras;
    public Sprite[] sprites_painel_conversas;
    public Image contorno_painel;

    private bool umaVez;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase10 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase10");
        }

        if(GameManager.progresso <= 9) {
            GameManager.Instance.SalvarSit(10, "Progresso");
        }

        pode_comecar_10 = false;
        umaVez = false;
        contador = 0;
        valor_aleatorio = 0;
        delayTiro = 1.5f;    
        PlayerControle.conversando = true;
        painel_conversas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(contagem_falas_10 <= 19 && contagem_falas_10 >= 0) {
            txt_falas.text = falas_pixel_preto[contagem_falas_10];
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_10) {
            contagem_falas_10++;
        }

        if(PlayerControle.player_morto == true) {
                painel_derrota.SetActive(true);
            }

        if(PixelPreto.evo_pixel == 1) {
            BarraVidaMaior.sprite = cara_transformado;
        }

        BarraVida();
        if(PixelPreto.AtirouJa) {
            valor_aleatorio = Random.Range(0, 6);
            contador += Time.deltaTime;
            if(PixelPreto.tirosDados > 0 && contador > delayTiro) {
                Instantiate(bolaFogo, spawn_cima[valor_aleatorio].position, spawn_cima[valor_aleatorio].rotation);
                PixelPreto.tirosDados--;
                contador = 0;
            } else if(PixelPreto.tirosDados == 0) {
                PixelPreto.atirarUmaVez = false;
            }
        }

        switch(contagem_falas_10) {
            case 3:
                img_carinha.sprite = sprites_caras[6];
                break;
            case 6:
                img_carinha.sprite = sprites_caras[0];
                break;
            case 7:
                img_carinha.sprite = sprites_caras[1];
                break;
             case 8:
                img_carinha.sprite = sprites_caras[4];
                contorno_painel.sprite = sprites_painel_conversas[0]; 
                break;
            case 11:
                img_carinha.sprite = sprites_caras[2];      
                break;
            case 12:
                img_carinha.sprite = sprites_caras[6];  
                break;
            case 13:
                img_carinha.sprite = sprites_caras[1];
                contorno_painel.sprite = sprites_painel_conversas[1];  
                break;
            case 18:
                img_carinha.sprite = sprites_caras[4];
                break;
            case 19:  
                img_carinha.sprite = sprites_caras[3];
                contorno_painel.sprite = sprites_painel_conversas[2];
                break;
            case 20:
                painel_conversas.SetActive(false);
                PlayerControle.conversando = false;
                PlayerControle.podeAtirar = true;
                PlayerControle.pode_mexer = true;
                pode_comecar_10 = true;
                break;                             
        }
    }

    private void BarraVida() {
        vida_restante.fillAmount = PixelPreto.vida_pixel_preto / vida_maxima;
    }
}
