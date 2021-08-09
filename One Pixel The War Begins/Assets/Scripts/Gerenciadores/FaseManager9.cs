using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager9 : MonoBehaviour
{

    private string[] falas = {"Eita! você chegou até aqui", "Me falaram que você estava vindo mas sinceramente eu nunca imaginaria que chegaria em mim",
    "Que tal irmos direto ao assunto? eu gostaria de ir no banheiro urgentemente", "Infelizmente para você eu sou imortal e nada do que fizer vai me matar ou mesmo me machucar", 
    "Então que tal você dar meia volta e me deixar cagar em paz? ", "Não?", "Mas você com certeza vai morrer amigo", "Não se importa?", "É um idiota mesmo....", "MAS EU GOSTO DA SUA DETERMINAÇÃO!!!!", "Sobreviva aos meus ataques por 75 segundos e eu te deixo passar", 
    "Mas se você não se importar irei ficar parado pois estou em um estado de concentração aqui, você entende né?","", "Você acredita na audacia desse cara?", "Não lembro de ter autorizado ninguém a me controlar assim", "Enfim Imperador, você passou no meu teste", 
    "Tomara que você venca eles", "Boa sorte lá, Tchau!"};

    public Text txtFalas;

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

    public GameObject portal_direita, portal_esquerda, portal_cima;
    public GameObject raposa;

    public static bool cabo_tudo;

    void Start()
    {
        contagem_falas_9 = 0;
        pode_comecar_9 = false;
        TiroPequenoChefao.modoHard = false;
        acabou = false;
        valor_provisorio = 0;
        valor_aleatorio = 0;
        cont_lobo = 0;
        soltarRaio = false;
        umaVezTempo = false;
        umaVez = false;
        segunda_parte = false;
        tempo_sobreviver = 75f;
        PlayerControle.pode_mexer = false;
        PlayerControle.podeAtirar = false;
        painel_falas.SetActive(true);   
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(contagem_falas_9 <= 17 && contagem_falas_9 >= 0) {
            txtFalas.text = falas[contagem_falas_9];
        }

        txt_tempo.text = tempo_sobreviver.ToString("F0");

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_9) {
            contagem_falas_9++;
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
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                painel_falas.SetActive(false);
                pode_comecar_9 = true;
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
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                painel_falas.SetActive(false);
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


    IEnumerator segundaParte() {
        yield return new WaitForSeconds(2f);
        Chefao7.possuido = true;
        segunda_parte = true;
        portal_1.SetActive(true);
        portal_2.SetActive(true);
        portal_3.SetActive(true);
        portal_4.SetActive(true);
    }

    IEnumerator falasFinais() {
        yield return new WaitForSeconds(7f);
        PlayerControle.pode_mexer = false;
        PlayerControle.podeAtirar = false;
        imagem.sprite = cara_irritado;
        painel_falas.SetActive(true);
        pode_comecar_9 = false;
        contagem_falas_9 = 14;
        Destroy(BarraVidaMaior);
        Destroy(txt_tempo);
    }
}
