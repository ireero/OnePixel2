using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager5 : MonoBehaviour
{
    private string[] falas_palhaco_portugues = {"EI, Olá!", "Você não me parece muito bem", "Se eu vou deixá-lo deixar passar?", "Claro que não amigo :)", 
    "Por que estou fazendo isso?", "Existem vários motivos sabe...", "Mas o maior deles é porque o caos me faz sorrir!","", "Você é realmente forte imperador", 
    "Eu aguardei loucamente por esse momento dês do dia que você mandou seus vassalos me deixarem cego", "O Pixel preto me acolheu, me deu forças, me deu PODER!", 
    "E agora finalmente posso-lhe matar. É uma pena que eu não possa vê-lo ver sofrer meu imperador...",  "Isso é realmente...", "ENGRAÇADO!"};

     private string[] falas_palhaco_ingles = {"EI, Hello!", "You don't look so good", "Will I let it go?", "Of course not, my friend :)", 
    "Why am I doing this?", "There are several reasons you know...", "But the greatest of them is because chaos makes me smile!","", "You are really strong emperor", 
    "I have waited madly for this moment since the day you sent your vassals to make me blind", "The black Pixel took me in, gave me strength, gave me POWER!", 
    "And now I can finally kill him. It's a pity that I can't watch him suffer my emperor.",  "That's really...", "FUNNY!"};

    private string[] falas_palhaco_chines = {
    "嘿，你好！",
    "你看起来不太好",
    "我会放过吗？",
    "当然不会，我的朋友 :)",
    "我为什么要这么做？",
    "你知道，有好几个理由……",
    "但最大的原因是，混乱让我发笑！",
    "",
    "皇帝，你真的非常强大",
    "自从你派你的附庸使我失明的那一天起，我就疯狂地等待这一刻",
    "黑像素收留了我，赋予我力量，赋予我权力！",
    "而现在我终于可以杀了他。真遗憾，我不能看着他受苦，皇帝。",
    "那真是……",
    "好笑！"
};

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar_portugues = "Pressione 'Q' para avançar";
    private string text_avancar_ingles = "Press 'Q' to advance";
    private string text_avancar_chines = "按下 'Q' 键以继续";

    public static int contagem_falas_5;

    public Transform[] spawn_slimes;
    public GameObject slime;

    public Image imagem;
    public GameObject painel_falas;

    public Sprite palhaco_normal;
    public Sprite palhaco_surpreso;
    public Sprite palhaco_triste;
    public Sprite palhaco_ameacador;

    public Sprite palhaco_normal_mv;
    public Sprite palhaco_triste_mv;
    public Sprite palhaco_ameacador_mv;
    public Sprite palhaco_euforico_mv;

    public static bool pode_comecar_5;

    public GameObject moeda_risada;

    public Transform ponto_baixo;

    private bool umaVez;

    private float cont_spawn;
    public static int slimes_vivos;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public Sprite icon_metade_vida;

    private float vida_maxima = 80f;

    public static int valor_tiros_dados = 8;

    public GameObject painel_derrota;
    public AudioSource back_som;
    private int podeTocar;

    public AudioSource back_void;
    public AudioSource som_fala;

    private int valor_alet;
    private int valor_prov;

    public GameObject chefao;
    private bool pode_normal;
    public GameObject escada;

    public GameObject vida_chefao;

    public GameObject protetor;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase5 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase5");
        }

        if(GameManager.progresso <= 5) {
            GameManager.Instance.SalvarSit(6, "Progresso");
        }

        if(GameManager.fase5 == 1 || GameManager.fase5 == 0) {
            MoedaRisada.moeda_ativou = false;
            pode_normal = true;
            valor_prov = 0;
            valor_alet = 0;
            cont_spawn = 0;
            slimes_vivos = 0;
            podeTocar = 0;
            if(GameManager.sem_dialogos == 0) {
                som_fala.Play();
                back_void.Play();
                painel_falas.SetActive(true);
                pode_comecar_5 = false;
                contagem_falas_5 = 0;
                PlayerControle.conversando = true;
            } else {
                PlayerControle.conversando = false;
                PlayerControle.podeAtirar = true;
                PlayerControle.pode_mexer = true;
                pode_comecar_5 = true;
                back_som.Play();
            }
            TiroPequenoChefao.modoHard = false;
            umaVez = false;
        } else {
            back_void.Play();
            pode_normal = false;
            escada.SetActive(true);
            Destroy(protetor);
            Destroy(chefao);
            Destroy(vida_chefao);
            PlayerControle.conversando = false;
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
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

            if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_5) {
                if(contagem_falas_5 != 6 && contagem_falas_5 != 13) {
                    som_fala.Play();
                }
                contagem_falas_5++;
            }

            if(contagem_falas_5 <= 13 && contagem_falas_5 >= 0) {
                if(Application.systemLanguage == SystemLanguage.Portuguese) {
                    txtFalas.text = falas_palhaco_portugues[contagem_falas_5];
                    txtAvancar.text = text_avancar_portugues;
                } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
                txtFalas.text = falas_palhaco_chines[contagem_falas_5];
                txtAvancar.text = text_avancar_chines;
            } 
            else {
                txtFalas.text = falas_palhaco_ingles[contagem_falas_5];
                txtAvancar.text = text_avancar_ingles;
            }
            }

            switch(contagem_falas_5) {
                case 2:
                    imagem.sprite = palhaco_surpreso;
                    break;
                case 3:
                    imagem.sprite = palhaco_normal;
                    break;   
                case 4:
                    imagem.sprite = palhaco_surpreso;
                    break;
                case 5:
                    imagem.sprite = palhaco_triste;
                    break;
                case 6:
                    imagem.sprite = palhaco_ameacador;
                    break;     
                case 7:
                    PlayerControle.conversando = false;
                    if(podeTocar <= 0) {
                        back_som.Play();
                        back_void.Stop();
                        podeTocar++;
                    } 
                    if(!pode_comecar_5) {
                        PlayerControle.pode_mexer = true;
                        PlayerControle.podeAtirar = true;
                    }
                    painel_falas.SetActive(false);
                    pode_comecar_5 = true;
                    break; 
                case 8:
                    if(podeTocar <= 1) {
                        som_fala.Play();
                        podeTocar++;
                    }
                    imagem.sprite = palhaco_normal_mv;
                    pode_comecar_5 = false;
                    painel_falas.SetActive(true);
                    PlayerControle.conversando = true;
                    break;    
                case 9:
                    imagem.sprite = palhaco_triste_mv;
                    break;      
                case 10:
                    imagem.sprite = palhaco_euforico_mv;
                    break;
                case 11:
                    imagem.sprite = palhaco_triste_mv;
                    break;
                case 13:
                    imagem.sprite = palhaco_ameacador_mv;
                    break;
                case 14:
                    PlayerControle.conversando = false;
                    if(!pode_comecar_5) {
                        PlayerControle.pode_mexer = true;
                        PlayerControle.podeAtirar = true;
                        pode_comecar_5 = true; 
                    }
                    painel_falas.SetActive(false);     
                    break;                      
            }

            if(pode_comecar_5 && Chefao04.vida_chefao > 0) {
                cont_spawn += Time.deltaTime;
                valor_alet = Random.Range(0, 6);

                if(cont_spawn >= 2f && slimes_vivos <= 5) {
                    if(valor_alet == valor_prov) {
                        if(valor_alet == 5) {
                            valor_alet--;
                        } else {
                            valor_alet++;
                        }
                    }
                    Instantiate(slime, spawn_slimes[valor_alet].position, spawn_slimes[valor_alet].rotation);
                    valor_prov = valor_alet;
                    slimes_vivos++;
                    cont_spawn = 0;
                }

            }

            BarraVida();

            if(Chefao04.tirosDados == 0) {
                umaVez = false;
            }

            if(Chefao04.tirosDados >= valor_tiros_dados && !umaVez) {
                    Instantiate(moeda_risada, ponto_baixo.position, ponto_baixo.rotation);
                    umaVez = true;
            }

            if(Chefao04.vida_chefao <= 0) {
                GameManager.Instance.SalvarSit(2, "Fase5");
                if(podeTocar <= 2) {
                    back_void.Play();
                    back_som.Stop();
                    podeTocar++;
                }
                Destroy(vida_chefao);
            } else if(Chefao04.vida_chefao <= 40 && Chefao04.vida_chefao > 0) {
                BarraVidaMaior.sprite = icon_metade_vida;
                TiroPequenoChefao.modoHard = true;
                BarraVidaMaior.color = Color.red;
            }
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao04.vida_chefao / vida_maxima;
    }
}
