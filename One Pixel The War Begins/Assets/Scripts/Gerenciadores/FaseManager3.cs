using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager3 : MonoBehaviour
{

    private string[] falas_chefao = {"Quem diria que você chegaria até aqui", "Tenho que dizer que estou impressionado pela sua força de vontade", 
    "Tenho que lhe contar uma coisa Imperador", "Esses monstros que você está matando são nada mais nada menos do que cidadões do seu Reino", 
    "Graças ao imperador Pixel Preto agora temos o poder que precisavamos", "Finalmente iremos deixar de ser a minoria", "Finalmente seremos nós a tomar grande decisões", 
    "Sua sorte acaba aqui Imperador Pixel Branco, prepare-se para morrer", " ", "Chega de brincadeira, irei lhe mostrar a força de todo um povo diminuido, amargurado e pronto para VINGANÇA!!"}; // 9

    public Text txtFalas;

    public static int contagem_falas_3;

    // Vidas
    public Image BarraVida1;
    public Image vidinha1;

    public Image BarraVida2;
    public Image vidinha2;

    public Image BarraVida3;
    public Image vidinha3;

    private int vida_chefao;

    public Image imagem;
    public GameObject painel_falas;

    public static bool pode_comecar_3;

    public Sprite chefao_ameacando;
    public Sprite chefao_raiva;
    public Sprite chefao_meia_vida;

    public Sprite icon_metade_da_vida;
    public Sprite icon_vida_normal;

    public GameObject painel_derrota;
    public AudioSource backSound;
    private int podeTocar;

    public AudioSource fala_personagens;
    public AudioSource back_void;

    private bool umaVezBack;

    void Start()
    {
        GameManager.Instance.SalvarSit(1, "Fase3");
        BarraVida1.sprite = icon_vida_normal;
        BarraVida2.sprite = icon_vida_normal;
        BarraVida3.sprite = icon_vida_normal;
        BarraVida1.color = Color.white;
        BarraVida2.color = Color.white;
        BarraVida3.color = Color.white;
        umaVezBack = false;
        back_void.Play();
        podeTocar = 0;
        painel_falas.SetActive(true);
        pode_comecar_3 = false;
        contagem_falas_3 = 0;
        PlayerControle.podeAtirar = false;
        PlayerControle.pode_mexer = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_3) {
            fala_personagens.Play();
            contagem_falas_3++;
        }

        if(contagem_falas_3 <= 9 && contagem_falas_3 >= 0) {
            txtFalas.text = falas_chefao[contagem_falas_3];
        }

        switch(contagem_falas_3) {
            case 3:
                imagem.sprite = chefao_ameacando;
                break;
            case 5:
                imagem.sprite = chefao_raiva;
                break;
            case 8:
                if(podeTocar <= 0) {
                    backSound.Play();
                    back_void.Stop();
                    podeTocar++;
                }
                PlayerControle.conversando = false;
                if(!pode_comecar_3) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                pode_comecar_3 = true;
                break; 
            case 9:
                pode_comecar_3 = false;
                imagem.sprite = chefao_meia_vida;
                PlayerControle.conversando = true;
                painel_falas.SetActive(true);
                break;  
            case 10:
                PlayerControle.conversando = false;
                if(!pode_comecar_3) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                pode_comecar_3 = true;
                painel_falas.SetActive(false);
                break;             
        }

        vida_chefao = Chefao03.vida_chefao;
        switch(vida_chefao) {
            case 2:
                vidinha1.enabled = false;
                break;
            case 1:
                vidinha2.enabled = false;
                break;
            case 0:
                BarraVida1.sprite = icon_metade_da_vida;
                BarraVida2.sprite = icon_metade_da_vida;
                BarraVida3.sprite = icon_metade_da_vida;
                vidinha1.enabled = true;
                vidinha2.enabled = true;
                BarraVida1.color = Color.red;
                BarraVida2.color = Color.red;
                BarraVida3.color = Color.red;
                break;  
            case -1:
                Destroy(BarraVida1);
                Destroy(vidinha1);
                break;
            case -2:
                Destroy(BarraVida2);
                Destroy(vidinha2);
                break;
            case -3:
                backSound.Stop();
                if(!umaVezBack) {
                    GameManager.Instance.SalvarSit(2, "Fase3");
                    back_void.Play();
                    umaVezBack = true;
                }
                Destroy(BarraVida3);
                Destroy(vidinha3);
                break;        
        }
    }
}
