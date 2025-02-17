using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaseManager10 : MonoBehaviour
{

    private string[] falas_pixel_preto_portugues = {"...", "Olá", "Não esperava vê-lo de novo", "Ao menos não nesta situação...", "Sabe, eu juro que tentei evitar tudo isso", "Eu juro que...", 
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

    private string[] falas_pixel_preto_chines = {
    "...",
    "你好",
    "我没想到还能见到他",
    "至少不会在这种情况下……",
    "你知道，我发誓我试过尽量避免这一切",
    "我发誓……",
    "算了，没有任何解释可以为这场大屠杀辩解",
    "不幸的是，我没有回头的路",
    "真可惜，一个民族因统治者的决策而受苦，你不觉得吗？",
    "真遗憾，一个特定群体被削弱",
    "真可惜，经过多年的和谐共处，我们突然成为了竞争对手",
    "我们在王位上不再有代表",
    "我们再也没有发言权",
    "我们不再都是一样的",
    "我们不断受到威胁",
    "我们生活在对攻击的恐惧中",
    "我们带着永不醒来的恐惧入睡",
    "我们带着被消灭的恐惧醒来",
    "有一天，我们终于厌倦了所有的屈辱，决定反抗……",
    "抱歉，但现在是你休息的时候了。",
    "",
    "我看到你在来这里的路上收集了一些东西",
    "具有讽刺意味的是，你正在使用我发起研究所产生的果实，不觉得吗？",
    "你从来不想学习任何东西",
    "我从不想支持和酬劳那些试图寻找进化途径的人",
    "看看你现在，全副武装……",
    "让我告诉你你错过了什么……",
    "研究表明，我们是所有分子结构中最小的",
    "我们是一切的开始和终结",
    "我们有能力变成任何东西",
    "但权力越大，责任越大，不是吗？",
    "伟大的力量需要巨大的能量、重大的改造、严格的训练以及更多",
    "多亏了红色像素，我们终于拥有了掌控一切所需的一切",
    "比看上去要容易，你能相信吗？",
    "一个愚蠢的州长一次又一次地离开自己的城市，以寻找新资源为借口",
    "一个对自己城市后巷发生的事毫不在乎的州长",
    "你基本上是自找的，白色像素",
    "我现在要展示给你看的东西超出了你所见过的一切",
    "这超出了你那封闭且愚蠢的脑袋",
    "这超越了你迄今为止所经历的一切",
    "现在，让那个你一直鄙视的人来杀了你！"
};

    public Text txtAvancar;
    public Text txt_final;
    public Text txt_obrigado;

    private string text_avancar_portugues = "Pressione 'Q' para avançar";
    private string text_avancar_ingles = "Press 'Q' to advance";
    private string text_avancar_chines = "按下 'Q' 键以继续";
    private string text_final_portugues = "Parabéns! você conseguiu zerar esse jogo que sinceramente eu acho muito difícil. Se você puder deixar uma review de como foi essa sua experiencia eu ficaria muito agradecido.\n\nEm memória da minha gatinha Fiora, obrigado por toda alegria e companheirismo que você me proporcionou. Descanse em paz.";
    private string text_obrigado_portugues = "Obrigado";
    private string text_final_ingles = "恭喜你，你已经成功打败了这个游戏，老实说，我觉得这个游戏非常难。为了纪念我的小猫菲奥拉，感谢你给我带来的欢乐和陪伴。安息吧";
    private string text_obrigado_ingles = "Thank you";
    private string text_final_chines = "恭喜！你成功通关了这款我真心觉得非常困难的游戏。如果你能留下评论，讲讲这次体验如何，我将非常感激。\n\n为了纪念我的小猫Fiora，感谢你带给我所有的欢乐和陪伴。安息吧。";
    private string text_obrigado_chines = "谢谢";
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
                txt_falas.text = falas_pixel_preto_portugues[contagem_falas_10];
                txtAvancar.text = text_avancar_portugues;
                txt_final.text = text_final_portugues;
                txt_obrigado.text = text_obrigado_portugues;
            } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                txt_falas.text = falas_pixel_preto_chines[contagem_falas_10];
                txtAvancar.text = text_avancar_chines;
                txt_final.text = text_final_chines;
                txt_obrigado.text = text_obrigado_chines;
            } else {
                txt_falas.text = falas_pixel_preto_ingles[contagem_falas_10];
                txtAvancar.text = text_avancar_ingles;
                txt_final.text = text_final_ingles;
                txt_obrigado.text = text_obrigado_ingles;
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
