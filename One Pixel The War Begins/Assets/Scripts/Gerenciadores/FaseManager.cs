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

    public static int contagem_falas;

    public GameObject painel_derrota;

    public GameObject monstros_base;

    public Transform spawn_1;
    public Transform spawn_2;
    public Transform spawn_3;
    public Transform spawn_4;
    public Transform spawn_5;
    public Transform spawn_6;

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

    public AudioSource background;
    public AudioSource back_void;

    public Image imagem;

    public Sprite cara_chorando;
    public Sprite cara_raiva;

    public Sprite chefao_normal;
    public Sprite chefao_lamentando;

    public Sprite icon_meia_vida;
    public Sprite icon_normal;
    public Image icon_atual;

    public AudioSource som_caida;
    private bool uma_batida;
    public AudioSource som_fala;

    private bool falarUmaVez;
    public GameObject vida_chefao;

    public GameObject base_branco;

    public GameObject escada;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CarregarDados();
        
        if(GameManager.fase1 == 0) {
            GameManager.Instance.SalvarSit(1, "Comecou");
            GameManager.Instance.SalvarSit(1, "Fase1");
        }

        if(GameManager.progresso <= 0) {
            GameManager.Instance.SalvarSit(1, "Progresso");
        }

        if(GameManager.fase1 == 1 || GameManager.fase1 == 0) {
            falarUmaVez = false;
            som_fala.Play();
            painel_falas.SetActive(true);
            falas_terminaram = false;
            chefao_vivo = true;
            chefao_nasceu = false;
            podeSpawn = false;
            valor_alet = 0;
            uma_batida = false;
            contagem = 0;
            contagem_falas = 0;
            back_void.Play();
            Chefao01.bateu_chao = false;
            PlayerControle.conversando = true;
        } else {
            escada.SetActive(true);
            Destroy(chefao);
            Destroy(spawn_1);
            Destroy(spawn_2);
            Destroy(spawn_3);
            Destroy(spawn_4);
            Destroy(spawn_5);
            Destroy(spawn_6);
            base_branco.SetActive(false);
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Chefao01.bateu_chao == true) {
            if(!uma_batida) {
                vida_chefao.SetActive(true);
                icon_atual.sprite = icon_normal;
                icon_atual.color = Color.white;
                som_caida.Play();
                uma_batida = true;
            }
        }

        BarraVida();
        contagem += Time.deltaTime;
        if(contagem >= 3.5f) {
            if(!chefao_nasceu && falas_terminaram) {
                back_void.Pause();
                background.Play();
                chefao.SetActive(true);
                chefao_nasceu = true;
            }
            valor_alet = Random.Range(1, 7);
            if(podeSpawn) {
                switch(valor_alet) {
                case 1:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_1.position, spawn_1.rotation);
                    break;
                case 2:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_2.position, spawn_2.rotation);
                    break;
                case 3:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_3.position, spawn_3.rotation);
                    break;    
                case 4:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_4.position, spawn_4.rotation);
                    break;    
                case 5:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_5.position, spawn_5.rotation);
                    break;    
                case 6:
                    MetadeVida();
                    Instantiate(monstros_base, spawn_6.position, spawn_6.rotation);
                    break;    
            }
            }
        }

        if(contagem_falas <= 8 && contagem_falas >= 0) {
            txtFalas.text = falas[contagem_falas];
            if(contagem_falas >= 1 && contagem_falas <= 2) {
                imagem.sprite = cara_raiva;
            } else if(contagem_falas >= 2 && contagem_falas <= 4) {
                imagem.sprite = cara_chorando;
            } else if(contagem_falas == 5) {
                falas_terminaram = true;
                painel_falas.SetActive(false);
            } else if((contagem_falas >= 6 && contagem_falas <= 7) && !chefao_vivo) {
                if(!falarUmaVez) {
                    som_fala.Play();
                    falarUmaVez = true;
                }
                PlayerControle.conversando = true;
                imagem.sprite = chefao_normal;
                painel_falas.SetActive(true);
            } else if(contagem_falas == 8 && !chefao_vivo) {
                imagem.sprite = chefao_lamentando;
            }
        } else{
            PlayerControle.conversando = false;
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            Chefao01.morrer_de_vez = true;
            painel_falas.SetActive(false);
        }

        if(Chefao01.vida <= 50 && Chefao01.vida > 0) {
            icon_atual.sprite = icon_meia_vida;
            BarraVidaMaior.color = Color.red;
        } else if(Chefao01.vida < 0) {
            GameManager.Instance.SalvarSit(2, "Fase1");
            background.Stop();
            Destroy(vida_chefao);
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            if(contagem_falas == 8) {
                back_void.Play();
            } else if(contagem_falas != 4){
                som_fala.Play();
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
