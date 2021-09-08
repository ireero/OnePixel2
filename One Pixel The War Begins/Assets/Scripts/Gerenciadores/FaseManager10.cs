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
    "E um dia simplesmente cansamos de toda humilhação e resolvemos revidar...", "Desculpe, mas chegou a hora de você descansar.","", "Vejo que você adquiriu algumas coisas no seu caminho até aqui", 
    "é irônico você estar usando fruto de pesquisas que eu iniciei, não acha?", "Você nunca quis estudar nada", "Nunca quis apoiar e remunarar aqueles que procuravam maneiras de evoluirmos", "Olhe para você agora, todo equipado...", 
    "Deixa eu te atualizar do que você andou perdendo...", "Pesquisas mostram que somos a menor estrutura molecular de tudo", "Somos o inicio e fim de tudo", "Temos o poder de nos transformar em qualquer coisa", 
    "Mas com grandes poderes vem grandes responsabilidades não é mesmo?", "Para um grande poder precisamos de uma grande energia, grandes modificações, grande treinamento e muito mais", "Graças ao Pixel vermelho temos tudo o que precisavámos para finalmente tomar o poder", 
    "Foi mais fácil do que parece, você acredita?", "Um governante burro que deixa sua cidade várias e várias vezes com a desculpa de buscar novos recursos", "Um governante que não se importa com o que acontece nos becos de sua cidade", 
    "Você praticamente pediu por isso Pixel Branco", "O que vou te mostrar agora está além de tudo que você já viu", "Está além dessa sua mente fechada e estupida", "Quando eu terminar você não estará mais aqui para poder contemplar o meu plano", 
    "AGORA MORRA PARA AQUELE QUE VOCÊ SEMPRE MENOSPREZOU!"};

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

    public GameObject adaga;
    public Sprite meia_vida_caveira;
    public Sprite cara_normal;
    private int i;

    private bool umaParedona;

    public Transform[] lugares_na_esquerda;
    public Transform[] lugares_na_direita;

    public GameObject pedra_fina;
    public GameObject pedra_pequena;

    public int valor;
    public int valor_prov;

    public int valor_alet;
    public int valor_alet_qual;
    public float cont_spawn;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase10 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase10");
        }

        if(GameManager.progresso <= 9) {
            GameManager.Instance.SalvarSit(10, "Progresso");
        }
        valor_alet_qual = 0;
        cont_spawn = 0;
        valor_alet = 0;
        valor_prov = 0;
        valor = 0;
        TiroPequenoChefao.modoHard = true;
        umaParedona = false;
        i = 0;
        contagem_falas_10 = 0;
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

        if(contagem_falas_10 <= 40 && contagem_falas_10 >= 0) {
            txt_falas.text = falas_pixel_preto[contagem_falas_10];
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_10) {
            contagem_falas_10++;
        }

        if(PlayerControle.player_morto == true) {
                painel_derrota.SetActive(true);
            }

        if(PixelPreto.evo_pixel == 1 && pode_comecar_10) {
            BarraVidaMaior.sprite = cara_transformado;
        } else if(PixelPreto.evo_pixel == 2) {
            BarraVidaMaior.sprite = meia_vida_caveira;
        }

        if(PixelPreto.meia_vida && PixelPreto.atirarAdagas) {
            if(!umaParedona) {
                Instantiate(paredona, lado_direito.position, lado_direito.rotation);
                Instantiate(paredona, lado_esquerdo.position, lado_esquerdo.rotation);
                umaParedona = true;
            }
            if(i <= 13) {
                Instantiate(adaga, spawn_cima[i].position, spawn_cima[i].rotation);
                i++;
                if(i == 13) {
                    i = 0;
                    PixelPreto.atirarAdagas = false;
                }
            }
        }

        BarraVida();
        if(PixelPreto.AtirouJa) {
            valor_aleatorio = Random.Range(0, 13);
            contador += Time.deltaTime;
            if(PixelPreto.tirosDados > 0 && contador > delayTiro) {
                Instantiate(bolaFogo, spawn_cima[valor_aleatorio].position, spawn_cima[valor_aleatorio].rotation);
                PixelPreto.tirosDados--;
                contador = 0;
            } else if(PixelPreto.tirosDados == 0) {
                PixelPreto.atirarUmaVez = false;
            }
        }

        if(PixelPreto.atirou_adagas == 6) {
            valor_alet_qual = Random.Range(0, 2);
            valor_alet = Random.Range(0, 10);
            cont_spawn += Time.deltaTime;
            if(PixelPreto.estaNaDireita) {
                if(cont_spawn >= 0.65f && valor_alet != valor_prov) {
                    if(valor_alet_qual == 0) {
                        Instantiate(pedra_fina, lugares_na_esquerda[valor_alet].position, lugares_na_esquerda[valor_alet].rotation);
                        Acrescenta();
                    } else {
                        Instantiate(pedra_pequena, lugares_na_esquerda[valor_alet].position, lugares_na_esquerda[valor_alet].rotation);
                        Acrescenta();
                    }
                }
            } else {
                if(cont_spawn >= 0.65f && valor_alet != valor_prov) {
                    if(valor_alet_qual == 0) {
                        Instantiate(pedra_fina, lugares_na_direita[valor_alet].position, lugares_na_direita[valor_alet].rotation);
                        Acrescenta();
                    } else {
                        Instantiate(pedra_pequena, lugares_na_direita[valor_alet].position, lugares_na_direita[valor_alet].rotation);
                        Acrescenta();
                    }
                }
            }

            if(valor >= 30) {
                PixelPreto.atirou_adagas++;
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
            case 21:
                pode_comecar_10 = false;
                painel_conversas.SetActive(true);
                PlayerControle.conversando = true;
                img_carinha.color = Color.red;
                BarraVidaMaior.sprite = cara_normal;
                BarraVidaMaior.color = Color.red;
                img_carinha.sprite = sprites_caras[0];
                contorno_painel.sprite = sprites_painel_conversas[3];
                break;
            case 22:
                img_carinha.sprite = sprites_caras[2];    
                break;
            case 24:
                img_carinha.sprite = sprites_caras[4];
                break; 
            case 25:
                img_carinha.sprite = sprites_caras[2];
                break;
            case 26:
                img_carinha.sprite = sprites_caras[0];
                break;
            case 41:
                BarraVidaMaior.color = Color.white;
                painel_conversas.SetActive(false);
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                pode_comecar_10 = true;
                break;                                                
        }
    }

    private void Acrescenta() {
        cont_spawn = 0;
        valor_prov = valor_alet;
        valor++;
    }

    private void BarraVida() {
        vida_restante.fillAmount = PixelPreto.vida_pixel_preto / vida_maxima;
    }
}
