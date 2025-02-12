using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager9 : MonoBehaviour
{

    private string[] falas_portugues = {"Eita!, Você chegou até aqui", "Me falaram que você estava vindo mas sinceramente eu nunca imaginaria que chegaria em mim",
    "Que tal irmos direto ao assunto?, Eu gostaria de ir no banheiro urgentemente", "Infelizmente para você eu sou imortal e nada do que fizer vai-me matar ou mesmo me machucar", 
    "Então que tal você dar meia volta e me deixar cagar em paz?", "Não?", "Mas você com certeza vai morrer amigo", "Não se importa?", "É um idiota mesmo...", "MAS EU GOSTO DA SUA DETERMINAÇÃO!", "Sobreviva aos meus ataques por 75 segundos e eu te deixo passar", 
    "Mas se você não se importar irei ficar parado pois estou em um estado de concentração aqui, você entende né?","", "Você acredita na audácia desse cara?", "Não lembro de ter autorizado ninguém a me controlar assim", "Enfim Imperador, você passou no meu teste", 
    "Tomara que você vença eles", "Boa sorte lá, Tchau!"};

    private string fala_mutado_portugues = "Você realmente se acha Deus calando os outros assim não é?";
    private string fala_mutado_ingles = "You really think you are God by shutting others up like that, don't you?";
    private string fala_mutado_chines = "你真以为这样让别人闭嘴就是上帝了，是吗？";

    private string fala_red_portugues = "Poder interessante esse seu!";
    private string fala_red_ingles = "Interesting power that yours!";
    private string fala_red_chines = "你的力量很有趣！";

    private string[] falas_ingles = {"damn!, You have come this far", "I was told that you were coming but honestly I never would have imagined that you would come to me",
    "How about we cut to the chase, I would like to go to the bathroom urgently", "Unfortunately for you I am immortal and nothing you do will kill me or even hurt me", 
    "So how about you turn around and let me shit in peace?", "No?", "But you will surely die friend", "Don't you care?", "He's a real jerk...", "BUT I LIKE YOUR DETERMINATION!", "Survive my attacks for 75 seconds and I let you go", 
    "But if you don't mind I will stay still because I am in a state of concentration here, you understand?","", "Can you believe the audacity of this guy?", "I don't remember ever allowing anyone to control me like that", "Anyway Emperoyr, ou passed my test", 
    "Hopefully you will beat them", "Good luck there, bye!"};

    private string[] falas_chines = {
    "该死！你竟然走到了这一步",
    "有人告诉我你会来，但老实说，我从未想过你会亲自来找我",
    "我们开门见山吧，我急需去厕所",
    "对你来说不幸的是，我是不死的，无论你做什么都无法杀死或伤害我",
    "那么，你能转过身，让我安静地拉屎吗？",
    "不？",
    "但你肯定会死的，朋友",
    "你不在乎吗？",
    "他真是个混蛋……",
    "但是我喜欢你的决心！",
    "在75秒内挺过我的攻击, 我就放你走",
    "但如果你不介意的话，我会保持静止，因为我正处于集中状态，你明白吗？",
    "",
    "你能相信这个家伙的厚颜无耻吗？",
    "我不记得曾经允许任何人那样控制过我",
    "总之, Emperoyr, 你通过了我的测试",
    "希望你能打败他们",
    "祝你好运，再见！"
};

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar_portugues = "Pressione 'Q' para avançar";
    private string text_avancar_ingles = "Press 'Q' to advance";
    private string text_avancar_chines = "按下 'Q' 键以继续";

    public static int contagem_falas_9;

    public Image imagem;

    public Transform[] pontos_direita;

    public static float tempo_sobreviver;

    public static bool pode_comecar_9;

    public Text txt_tempo;

    public GameObject painel_falas;

    public GameObject painel_derrota;

    public GameObject portal_1;
    public GameObject portal_2;
    public GameObject portal_3;
    public GameObject portal_4;

    public GameObject escada;

    private bool segunda_parte;
    private bool umaVez;
    private bool umaVezTempo;

    public GameObject mao_1;
    public GameObject mao_2;

    public GameObject raio;
    public Transform parado_esquerdo;
    public Transform parado_direito;

    public static bool soltarRaio;

    private float cont_lobo;
    private int valor_aleatorio;
    private int valor_provisorio;

    public GameObject cabeca_lobo;

    public Image BarraVidaMaior;
    public Image BarraDeVida;
    public Image infinito;
    
    public Sprite chefao_meia_vida;
    public Sprite chefao_normal;

    public GameObject cara_vermelha;

    public static bool acabou;

    public Sprite cara_soberba;
    public Sprite cara_desconfiado;
    public Sprite poker_face;
    public Sprite cara_surpreso;
    public Sprite cara_sorrindo;
    public Sprite cara_irritado;
    public Sprite cara_foco;

    public GameObject portal_direita, portal_esquerda, portal_cima;
    public GameObject raposa;

    public static bool cabo_tudo;

    public GameObject barra_vida;

    public GameObject chefao;

    private bool pode_normal;

    public AudioSource som_void;
    public AudioSource musica_fase;
    public AudioSource som_fala;

    private Animator anim;

    public GameObject tempo_objeto;

    public GameObject esquerda;
    public GameObject direita;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase9 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase9");
        }

        if(GameManager.progresso <= 9) {
            GameManager.Instance.SalvarSit(10, "Progresso");
            GameManager.Instance.CarregarDados();
        }

        anim = GetComponent<Animator>();

        som_void.Play();

        if(GameManager.fase9 == 0 || GameManager.fase9 == 1) {
            som_fala.Play();
            pode_normal = true;
            FaseManager6.pode_comecar_6 = true;
            contagem_falas_9 = 0;
            pode_comecar_9 = false;
            TiroPequenoChefao.modoHard = false;
            SuperTiroChefao.modoHard = false;
            acabou = false;
            valor_provisorio = 0;
            valor_aleatorio = 0;
            cont_lobo = 0;
            soltarRaio = false;
            umaVezTempo = false;
            umaVez = false;
            segunda_parte = false;
            tempo_sobreviver = 75f;
            painel_falas.SetActive(true);   
            Portal.atira_ae_po = 0;
            cabo_tudo = false;
            PlayerControle.conversando = true;
        } else {
            pode_normal = false;
            Destroy(chefao);
            Destroy(raposa);
            Destroy(esquerda);
            Destroy(direita);
            escada.SetActive(true);
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            BarraVidaMaior.enabled = false;
            BarraDeVida.enabled = false;
            txt_tempo.enabled = false;
            infinito.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }
        
        if(pode_normal) {
            if(PlayerControle.player_morto == true) {
                painel_derrota.SetActive(true);
            }

        if(contagem_falas_9 <= 17 && contagem_falas_9 >= 0) {
            if(GameManager.sem_dialogos == 0) { 
                if(Application.systemLanguage == SystemLanguage.Portuguese) {
                    if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                        imagem.sprite = cara_foco;
                        txtFalas.text = fala_red_portugues;
                    } else {
                        txtFalas.text = falas_portugues[contagem_falas_9];
                    }
                    txtAvancar.text = text_avancar;
                } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                imagem.sprite = cara_foco;
                txtFalas.text = fala_red_chines;
            } else {
                txtFalas.text = falas_chines[contagem_falas_9];
            }
         } else {
                    if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                        imagem.sprite = cara_foco;
                        txtFalas.text = fala_red_ingles;
                    } else {
                        txtFalas.text = falas_ingles[contagem_falas_9];
                    }
                }
            } else {
                if(Application.systemLanguage == SystemLanguage.Portuguese) {
                    txtAvancar.text = text_avancar_portugues;
                } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                    txtAvancar.text = text_avancar_chines
                } else {
                    txtAvancar.text = text_avancar_ingles
                }
                if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                    if(Application.systemLanguage == SystemLanguage.Portuguese) {
                        txtFalas.text = fala_red_portugues;
                    } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                        txtFalas.text = fala_red_chines
                    } else {
                        txtFalas.text = fala_red_ingles;
                    }
                } else {
                    if(Application.systemLanguage == SystemLanguage.Portuguese) {
                        txtFalas.text = fala_mutado_portugues;
                    } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                        txtFalas.text = fala_mutado_chines;
                    } else {
                        txtFalas.text = fala_mutado_ingles;
                    }
                }
            }
        }

        txt_tempo.text = tempo_sobreviver.ToString("F0");

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_9 && GameManager.sem_dialogos == 0) {
            if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                imagem.sprite = cara_foco;
                painel_falas.SetActive(false);
                tempo_objeto.SetActive(true);
            if(!pode_comecar_9) {
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                som_void.Stop();
                musica_fase.Play();
            }
            pode_comecar_9 = true;
            Chefao7.red_sair = true;
            } else {
                if(contagem_falas_9 != 11 && contagem_falas_9 != 17) {
                    som_fala.Play();
                }
                contagem_falas_9++;
            }
        } else if(Input.GetKeyDown(KeyCode.Q) && GameManager.sem_dialogos == 1 && (!pode_comecar_9 || PlayerControle.type_red == 1 || PlayerControle.type_red == 2)) {
            painel_falas.SetActive(false);
            tempo_objeto.SetActive(true);
            if(!pode_comecar_9) {
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                som_void.Stop();
                musica_fase.Play();
            }
            Chefao7.red_sair = true;
            imagem.sprite = cara_foco;
            pode_comecar_9 = true;
        }

        if(tempo_sobreviver > 0 && pode_comecar_9) {
            tempo_sobreviver -= Time.deltaTime;
        }

        if(Parado.escondido == true && PlayerControle.parado == true && !soltarRaio) {
            Instantiate(raio, parado_direito.position, parado_direito.rotation);
            Instantiate(raio, parado_esquerdo.position, parado_esquerdo.rotation);
            soltarRaio = true;
        }

        switch(contagem_falas_9) {
            case 1:
                imagem.sprite = cara_desconfiado;
                break;
            case 3:
                imagem.sprite = cara_soberba;
                break;
            case 5:
                imagem.sprite = poker_face;
                break;  
            case 7:
                imagem.sprite = cara_surpreso;
                break;
            case 8:
                imagem.sprite = poker_face;
                break;
            case 9:
                imagem.sprite = cara_sorrindo;
                break;
            case 10:
                imagem.sprite = cara_soberba;
                break;
            case 11:
                imagem.sprite = poker_face;
                break;
            case 12:
                PlayerControle.conversando = false;
                if(!pode_comecar_9) {
                    tempo_objeto.SetActive(true);
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                    som_void.Stop();
                    musica_fase.Play();
                }
                painel_falas.SetActive(false);
                pode_comecar_9 = true;
                break;
            case 14:
                som_fala.Play();
                break;    
            case 15:
                imagem.sprite = poker_face;
                break;
            case 16:
                imagem.sprite = cara_desconfiado;
                break;
            case 17:
                imagem.sprite = cara_sorrindo;
                break;
            case 18:
                PlayerControle.conversando = false;
                if(!cabo_tudo) {
                    Destroy(barra_vida);
                    txt_tempo.enabled = false;
                    GameManager.Instance.SalvarSit(2, "Fase9");
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                escada.SetActive(true);
                cabo_tudo = true;
                break;            

        }

        if(segunda_parte) {

            if(Chefao7.possuido == false) {
            BarraVidaMaior.color = Color.white;
            BarraVidaMaior.sprite = chefao_normal;
        }

            if(!umaVezTempo) {
                cara_vermelha.SetActive(true);
                BarraVidaMaior.sprite = chefao_meia_vida;
                BarraVidaMaior.color = Color.red;
                tempo_sobreviver = 75f;
                umaVezTempo = true;
            }

            if(tempo_sobreviver <= 50f && tempo_sobreviver > 25f && pode_comecar_9) {
                mao_1.SetActive(true);
                mao_2.SetActive(true);
            } else if(tempo_sobreviver <= 25f && tempo_sobreviver > 0 && pode_comecar_9) {
                if(tempo_sobreviver <= 0.1 && !acabou) {
                    StartCoroutine("falasFinais");
                    Destroy(esquerda);
                    Destroy(direita);
                    som_void.Play();
                    musica_fase.Stop();
                    acabou = true;
                }
                valor_aleatorio = Random.Range(0, 9);
                cont_lobo += Time.deltaTime;
                if(cont_lobo > 1f) {
                        if(valor_aleatorio == valor_provisorio) {
                            if(valor_aleatorio == 8) {
                                valor_aleatorio--;
                            } else {
                                valor_aleatorio++;
                            }
                        }
                        Instantiate(cabeca_lobo, pontos_direita[valor_aleatorio].position, pontos_direita[valor_aleatorio].rotation);
                        valor_provisorio = valor_aleatorio;
                        cont_lobo = 0;
                    }
                }

        } else {

            if(tempo_sobreviver <= 75f && tempo_sobreviver > 50f && pode_comecar_9) {
                portal_cima.SetActive(true);
                portal_direita.SetActive(true);
                portal_esquerda.SetActive(true);
                raposa.SetActive(true);
            } else if(tempo_sobreviver <= 50f && tempo_sobreviver > 25f) {
                Portal.atira_ae_po = 2;
            } else if(tempo_sobreviver <= 25f && tempo_sobreviver >= 0f) {
                Portal.atira_ae_po = 3;
            } else if(tempo_sobreviver <= 0) {
                Portal.atira_ae_po = 5;
                if(!umaVez) {
                    umaVez = true;
                    StartCoroutine("segundaParte");
                }
            }
        }
        }
    }


    IEnumerator segundaParte() {
        yield return new WaitForSeconds(2f);
        Chefao7.possuido = true;
        segunda_parte = true;
        portal_1.SetActive(true);
        portal_2.SetActive(true);
        portal_3.SetActive(true);
        portal_4.SetActive(true);
    }

    public void PararTremedeira() {
        anim.SetBool("tremer_chao", false);
    }

    IEnumerator falasFinais() {
        yield return new WaitForSeconds(7f);
        PlayerControle.conversando = true;
        imagem.sprite = cara_irritado;
        if(GameManager.sem_dialogos == 0) {
            if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
                CabarTudo();
            } else {
                painel_falas.SetActive(true);
                pode_comecar_9 = false;
                contagem_falas_9 = 14;
            }
        } else if(GameManager.sem_dialogos == 1 || PlayerControle.type_red == 1 || PlayerControle.type_red == 2){
            CabarTudo();
        }
    }

    private void CabarTudo() {
        if(!cabo_tudo) {
                som_void.Play();
                musica_fase.Stop();
                GameManager.Instance.SalvarSit(2, "Fase9");
                cabo_tudo = true;
                PlayerControle.conversando = false;
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true; 
                escada.SetActive(true);
            }
        Destroy(barra_vida);
        Destroy(txt_tempo);
    }

    IEnumerator escadaAparecer() {
        yield return new WaitForSeconds(1.5f);
        escada.SetActive(true);
    }
}
