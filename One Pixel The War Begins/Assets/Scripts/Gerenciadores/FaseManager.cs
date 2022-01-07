using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaseManager : MonoBehaviour
{

    private string[] falas = {"Aleluia, você chegou!", "Meu senhor o castelo foi tomado!", "Todos morreram lá dentro", "Por favor meu lorde nos ajude", 
    "TE IMPLORO SENHOR QUE NOS AJUDE!"," ", "Existem alguns verdadeiros monstros nesse castelo agora", 
    "Fuja em quanto ainda tem chance", "Diga a todos que sinto muito."};

    private string[] falas_ingles = {"Hallelujah, you are here!", "My lord, the castle has been taken!", "Everyone died inside", "Please my lord help us", 
    "I BEG YOU LORD HELP US!"," ", "There are some real monsters in this castle now", 
    "Escape while you still have the chance", "Tell everyone I'm sorry."};

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar = "Pressione 'Q' para avançar";

    public static int contagem_falas;

    public GameObject painel_derrota;

    public GameObject monstros_base;

    public GameObject chefao;
    private bool chefao_nasceu;
    public static bool chefao_vivo;

    public float contagem;
    private int valor_alet;
    public static bool podeSpawn;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public static float vidaMaxima = 100f;

    public GameObject painel_falas;

    public static bool falas_terminaram;

    public Image imagem;

    public Sprite cara_chorando;
    public Sprite cara_raiva;

    public Sprite chefao_normal;
    public Sprite chefao_lamentando;

    public Sprite icon_meia_vida;
    public Sprite icon_normal;
    public Image icon_atual;

    private bool uma_batida;

    private bool falarUmaVez;
    public GameObject vida_chefao;

    public GameObject base_branco;

    public GameObject escada;

    private bool pode_comecar;

    public GameObject[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CarregarDados();
        
        if(GameManager.fase1 == 0) {
            GameManager.Instance.SalvarSit(1, "Comecou");
            GameManager.Instance.SalvarSit(1, "Fase1");
        }

        if(GameManager.progresso <= 1) {
            GameManager.Instance.SalvarSit(2, "Progresso");
        }

        if(GameManager.fase1 == 1 || GameManager.fase1 == 0) {
            falarUmaVez = false;
            pode_comecar = false;
            if(GameManager.sem_dialogos == 0) {
                painel_falas.SetActive(true);
                contagem_falas = 0;
                falas_terminaram = false;   
            } else {
                falas_terminaram = true;
            }
            chefao_vivo = true;
            chefao_nasceu = false;
            podeSpawn = false;
            valor_alet = 0;
            uma_batida = false;
            contagem = 0;
            Chefao01.bateu_chao = false;
            PlayerControle.conversando = true;
        } else {
            Chefao01.bateu_chao = false;
            escada.SetActive(true);
            Destroy(chefao);
            for(int i = 0; i <= 5; i++) {
                Destroy(spawns[i]);
            }
            base_branco.SetActive(false);
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Chefao01.bateu_chao == true) {
            if(!uma_batida) {
                vida_chefao.SetActive(true);
                icon_atual.sprite = icon_normal;
                icon_atual.color = Color.white;
                uma_batida = true;
            }
        }

        BarraVida();
        contagem += Time.deltaTime;
        if(contagem >= 3.5f) {
            if(!chefao_nasceu && falas_terminaram) {
                chefao.SetActive(true);
                chefao_nasceu = true;
            }
            valor_alet = Random.Range(1, 7);
            if(podeSpawn) {
                switch(valor_alet) {
                case 1:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[0].transform.position, spawns[0].transform.rotation);
                    break;
                case 2:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[1].transform.position, spawns[1].transform.rotation);
                    break;
                case 3:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[2].transform.position, spawns[2].transform.rotation);
                    break;    
                case 4:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[3].transform.position, spawns[3].transform.rotation);
                    break;    
                case 5:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[4].transform.position, spawns[4].transform.rotation);
                    break;    
                case 6:
                    MetadeVida();
                    Instantiate(monstros_base, spawns[5].transform.position, spawns[5].transform.rotation);
                    break;    
            }
            }
        }

        if(contagem_falas <= 8 && contagem_falas >= 0) {
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txtFalas.text = falas[contagem_falas];
                txtAvancar.text = text_avancar;
            } else {
                txtFalas.text = falas_ingles[contagem_falas];
            }
            if(contagem_falas >= 1 && contagem_falas <= 2) {
                imagem.sprite = cara_raiva;
            } else if(contagem_falas >= 2 && contagem_falas <= 4) {
                imagem.sprite = cara_chorando;
            } else if(contagem_falas == 5) {
                falas_terminaram = true;
                pode_comecar = true;
                painel_falas.SetActive(false);
            } else if((contagem_falas >= 6 && contagem_falas <= 7) && !chefao_vivo && GameManager.sem_dialogos == 0) {
                if(!falarUmaVez) {
                    falarUmaVez = true;
                }
                PlayerControle.conversando = true;
                imagem.sprite = chefao_normal;
                pode_comecar = false;
                painel_falas.SetActive(true);
            } else if(contagem_falas == 8 && !chefao_vivo) {
                imagem.sprite = chefao_lamentando;
            } else if(GameManager.sem_dialogos != 0 && !chefao_vivo) {
                Chefao01.morrer_de_vez = true;
                if(!falarUmaVez) {
                    falarUmaVez = true;
                }
            }
        } else{
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            Chefao01.morrer_de_vez = true;
            pode_comecar = true;
            painel_falas.SetActive(false);
        }

        if(Chefao01.vida <= 50 && Chefao01.vida > 0) {
            icon_atual.sprite = icon_meia_vida;
            BarraVidaMaior.color = Color.red;
        } else if(Chefao01.vida < 0) {
            GameManager.Instance.SalvarSit(2, "Fase1");
            Destroy(vida_chefao);
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar) {
            if(contagem_falas == 8) {
            } else if(contagem_falas != 4){
            }
            contagem_falas++;
        }
    }

    private void MetadeVida() {
        if(Chefao01.metade_vida == true) {
            contagem = 2.25f;
        } else {
            contagem = 0;
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao01.vida / vidaMaxima; 
    }
}
