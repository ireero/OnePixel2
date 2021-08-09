using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaseManager : MonoBehaviour
{

    private string[] falas = {"Aleluia, você chegou!", "Meu senhor o castelo foi tomado!", "Todos morreram lá dentro", "Por favor meu lorde nos ajude", 
    "EU TE IMPLORO SENHOR QUE NOS AJUDE!"," ", "Existem alguns verdadeiros monstros nesse castelo agora", 
    "Fuja em quanto ainda tem chance", "Diga a todos que sinto muito...."};

    public Text txtFalas;

    public static int contagem_falas;

    public GameObject painel_derrota;

    public GameObject monstros_base;
    public SpriteRenderer monstro_sprite;

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
    public Image icon_atual;

    public AudioSource som_caida;
    private bool uma_batida;
    public AudioSource som_fala;

    private bool falarUmaVez;
    public GameObject vida_chefao;

    // Start is called before the first frame update
    void Start()
    {
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
        PlayerControle.pode_mexer = false;
        PlayerControle.podeAtirar = false;
        back_void.Play();
        Chefao01.bateu_chao = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Chefao01.bateu_chao == true) {
            if(!uma_batida) {
                vida_chefao.SetActive(true);
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
                PlayerControle.pode_mexer = false;
                PlayerControle.podeAtirar = false;
                imagem.sprite = chefao_normal;
                painel_falas.SetActive(true);
            } else if(contagem_falas == 8 && !chefao_vivo) {
                imagem.sprite = chefao_lamentando;
            }
        } else{
            PlayerControle.pode_mexer = true;
            PlayerControle.podeAtirar = true;
            Chefao01.morrer_de_vez = true;
            painel_falas.SetActive(false);
        }

        if(Chefao01.vida <= 50 && Chefao01.vida > 0) {
            icon_atual.sprite = icon_meia_vida;
            BarraVidaMaior.color = Color.red;
        } else if(Chefao01.vida < 0) {
            background.Stop();
            Destroy(BarraVidaMaior);
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            if(contagem_falas == 8) {
                back_void.Play();
            } else {
                som_fala.Play();
            }
            contagem_falas++;
        }
    }

    private void MetadeVida() {
        if(Chefao01.metade_vida == true) {
            contagem = 2.25f;
            monstro_sprite.color = Color.red;   
        } else {
            monstro_sprite.color = Color.white;
            contagem = 0;
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao01.vida / vidaMaxima; 
    }
}
