using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaseManager10 : MonoBehaviour
{

    private string[] falas_pixel_preto = {"...", "Olá", "Não esperava vê-lo de novo", "Ao menos não nesta situação...", "Sabe, eu juro que tentei evitar tudo isso", "Eu juro que...", 
    "Deixa, não à nenhuma explicação que justifique esse massacre", "Infelizmente eu não tenho como voltar atrás", "É uma pena todo um povo sofrer por decisões de seus governantes, você não acha?", 
    "É uma pena um grupo específico ser diminuído", "É uma pena que após anos de convivência em harmonia do nada viramos rivais", "Não temos mais representante no trono", 
    "Não temos mais nenhuma voz", "Não somos mais todos iguais", "Somos constantemente ameaçados", "Vivemos com medo de sofrer um ataque", "Dormimos com medo de nunca acordar", "Acordamos com medo de sofrer extermínio", 
    "E um dia simplesmente cansamos de toda humilhação e resolvemos revidar...", "Desculpe, mas chegou a hora de você descansar.","", "Vejo que você adquiriu algumas coisas no seu caminho até aqui", 
    "É irônico você estar usando fruto de pesquisas que eu iniciei, não acha?", "Você nunca quis estudar nada", "Nunca quis apoiar e remunerar aqueles que procuravam maneiras de evoluirmos", "Olhe para você agora, todo equipado...", 
    "Deixa eu te atualizar do que você andou perdendo...", "Pesquisas mostram que somos a menor estrutura molecular de tudo", "Somos o inicio e fim de tudo", "Temos o poder de nos transformar em qualquer coisa", 
    "Mas com grandes poderes vem grandes responsabilidades não é mesmo?", "Para um grande poder precisamos de uma grande energia, grandes modificações, grande treinamento e muito mais", "Graças ao Pixel vermelho temos tudo o que precisávamos para finalmente tomar o poder", 
    "Foi mais fácil do que parece, você acredita?", "Um governante burro que deixa sua cidade várias e várias vezes com a desculpa de buscar novos recursos", "Um governante que não se importa com o que acontece nos becos de sua cidade", 
    "Você praticamente pediu por isso Pixel Branco", "O que vou-te mostrar agora está além de tudo que você já viu", "Está além dessa sua mente fechada e estúpida", "Está além de todos que você enfrentou até agora", 
    "AGORA MORRA PARA AQUELE QUE VOCÊ SEMPRE MENOSPREZOU!"};

    private string[] falas_pixel_preto_ingles = {"...", "Hello", "I didn't expect to see him again", "At least not in this situation...", "You know, I swear I tried to avoid all this", "I swear...", 
    "Leave it, there is no explanation that justifies this massacre", "Unfortunately I have no way back", "It is a shame that a whole people suffers because of the decisions of its rulers, don't you think?", 
    "It is a pity that a specific group is diminished", "It is a pity that after years of living together in harmony we suddenly become rivals", "We no longer have a representative on the throne", 
    "We no longer have any voice", "We are no longer all the same", "We are constantly threatened", "We live in fear of having an attack", "We sleep with the fear of never waking up", "We wake up in fear of extermination", 
    "And one day we simply got tired of all the humiliation and decided to fight back...", "Sorry, but it is time for you to rest.", "", "I see that you have acquired some things on your way here", 
    "It is ironic that you are using fruit from research I initiated, don't you think?", "You never wanted to study anything", "I never wanted to support and remunerate those who were looking for ways to evolve", "Look at you now, all equipped..", 
    "Let me update you on what you have been missing...", "Research shows we are the smallest molecular structure of all", "We are the beginning and the end of everything", "We have the power to transform ourselves into anything", 
    "But with great power comes great responsibility, doesn't it?", "For great power we need great energy, great modifications, great training and much more", "Thanks to the Red Pixel we have everything we needed to finally take power", 
    "It was easier than it looks, can you believe it?", "A dumb governor who leaves his city over and over again with the excuse of seeking new resources", "A governor who doesn't care what happens in the back alleys of his city", 
    "ou practically asked for it White Pixel", "What I will show you now is beyond anything you have ever seen", "It is beyond your closed and stupid mind", "It is beyond all that you have faced so far", 
    "NOW DIE TO THE ONE YOU HAVE ALWAYS DESPISED!"};

    public Text txtAvancar;
    public Text txt_final;
    public Text txt_obrigado;

    private string text_avancar = "Pressione 'Q' para avançar";
    private string text_final = "Parabéns! você conseguiu zerar esse jogo que sinceramente eu acho muito difícil. Se você puder deixar uma review de como foi essa sua experiencia eu ficaria muito agradecido.\n\nEm memória da minha gatinha Fiora, obrigado por toda alegria e companheirismo que você me proporcionou. Descanse em paz.";
    private string text_obrigado = "Obrigado";

    public GameObject bolaFogo;
    private float delayTiro;

    public Transform[] spawn_cima;
    public static int valor_aleatorio;

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

    public GameObject adaga;
    public Sprite meia_vida_caveira;
    public Sprite cara_normal;
    private int i;

    public static bool umaParedona;

    public Transform[] lugares_na_esquerda;
    public Transform[] lugares_na_direita;

    public GameObject pedra_fina;
    public GameObject pedra_pequena;

    public int valor;
    public int valor_prov;

    public int valor_alet;
    public int valor_alet_qual;
    public float cont_spawn;

    public AudioSource som_fala;
    public AudioSource som_back;
    public AudioSource som_vacuo;
    public AudioSource nascer_adagas;

    private int musicaUmaVez;
    private bool adagaUmaVez;

    public GameObject painel_zerou;
    public static bool acabou;

    public GameObject boss_final;
    public GameObject vida_ne;
    public GameObject clone1, clone2;

    private bool morte_uma;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase10 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase10");
        }

        if(GameManager.progresso <= 10) {
            GameManager.Instance.SalvarSit(11, "Progresso");
        }

        if(GameManager.fase10 == 0 || GameManager.fase10 == 1) {
            acabou = false;
            adagaUmaVez = false;
            musicaUmaVez = 0;
            valor_alet_qual = 0;
            cont_spawn = 0;
            valor_alet = 0;
            valor_prov = 0;
            valor = 0;
            morte_uma = false;
            if(GameManager.sem_dialogos == 0) {
                som_fala.Play();
                som_vacuo.Play();
                painel_conversas.SetActive(true);
                contagem_falas_10 = 0;
                pode_comecar_10 = false;
                PlayerControle.conversando = true;
            } else {
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                pode_comecar_10 = true;
                contagem_falas_10 = 0;
                som_back.Play();
            }
            TiroPequenoChefao.modoHard = true;
            umaParedona = false;
            i = 0;
            contador = 0;
            valor_aleatorio = 0;
            delayTiro = 1.5f;    
        } else {
            Destroy(painel_zerou);
            som_vacuo.Play();
            Destroy(boss_final);
            Destroy(vida_ne);
            Destroy(clone1);
            Destroy(clone2);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(PixelPreto.vida_pixel_preto <= 0) {
            if(!morte_uma) {
                GameManager.Instance.SalvarSit(2, "Fase10");
                Destroy(vida_ne);
                som_back.Stop();
                som_vacuo.Play();
                morte_uma = true;
            }
        }

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }

        if(acabou) {
            painel_zerou.SetActive(true);
        }

        if(contagem_falas_10 <= 40 && contagem_falas_10 >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txt_falas.text = falas_pixel_preto[contagem_falas_10];
                txtAvancar.text = text_avancar;
                txt_final.text = text_final;
                txt_obrigado.text = text_obrigado;
            } else {
                txt_falas.text = falas_pixel_preto_ingles[contagem_falas_10];
            }
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_10) {
            contagem_falas_10++;
            if(contagem_falas_10  != 20 && contagem_falas_10 != 41) {
                som_fala.Play();
            }
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
                if(!adagaUmaVez) {
                    nascer_adagas.Play();
                    adagaUmaVez = true;
                }
                Instantiate(adaga, spawn_cima[i].position, spawn_cima[i].rotation);
                i++;
                if(i == 13) {
                    i = 0;
                    adagaUmaVez = false;
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
                StartCoroutine("podeIr");
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
        } else if(PixelPreto.atirou_adagas == 0) {
            valor = 0;
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
                pode_comecar_10 = true;
                if(musicaUmaVez == 0) {
                    PlayerControle.conversando = false;
                    PlayerControle.podeAtirar = true;
                    PlayerControle.pode_mexer = true;
                    som_vacuo.Stop();
                    som_back.Play();
                    musicaUmaVez++;
                }
                break;
            case 21:
                if(musicaUmaVez == 1) {
                    som_vacuo.Play();
                    som_back.Stop();
                    musicaUmaVez++;
                }
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
            case 28:
                img_carinha.sprite = sprites_caras[3];
                break;
            case 30:
                img_carinha.sprite = sprites_caras[5];
                break;  
            case 31:
                img_carinha.sprite = sprites_caras[0];
                break; 
            case 33:
                img_carinha.sprite = sprites_caras[5];
                break;   
            case 34:
                img_carinha.sprite = sprites_caras[4];   
                break;
            case 37:
                img_carinha.sprite = sprites_caras[2];
                break;
            case 39:
                img_carinha.sprite = sprites_caras[0];
                break;    
            case 40:
                img_carinha.sprite = sprites_caras[3];
                break;                   
            case 41:
                if(musicaUmaVez == 2) {
                    som_vacuo.Stop();
                    som_back.Play();
                    PlayerControle.conversando = false;
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                    musicaUmaVez++;
                }
                BarraVidaMaior.color = Color.white;
                painel_conversas.SetActive(false);
                pode_comecar_10 = true;
                break;                                                
        }
    }

    private void Acrescenta() {
        cont_spawn = 0;
        valor_prov = valor_alet;
        valor++;
    }

    public void Zerar() {
        GameManager.Instance.SalvarSit(1, "ZEROU");
        SceneManager.LoadSceneAsync("Menu");
    }

    private void BarraVida() {
        vida_restante.fillAmount = PixelPreto.vida_pixel_preto / vida_maxima;
    }

    IEnumerator podeIr() {
        yield return new WaitForSeconds(5f);
        PixelPreto.atirarUmaVez = false;
    }
}
