using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager6 : MonoBehaviour
{

    private string[] falas_chefao06 = {"- . / .--. . -.-. --- / --.- ..- . / ...- .- / . -- -... --- .-. .-", 
                                        ". ..- / .-. . .- .-.. -- . -. - . / -. .- --- / --.- ..- . .-. --- / -- .- -.-. .... ..- -.-. .- .-. / ...- --- -.-. .", "", 
                                        "...- --- -.-. . / . ... - .- / .--. .. --- .-. .- -. -.. --- / - ..- -.. --- --..-- / -. .- --- / .--. --- ... ... --- / -.-. --- -. - .-. --- .-.. .- .-. / .. ... ... --- / -- . ..- / ... . -. .... --- .-. --..-- / .- -.-. .- -... .- .-. . .. / -- .- - .- -. -.. --- / ...- --- -.-. . --..-- / ..-. ..- .--- .- --..-- / .--. --- .-. / ..-. .- ...- --- .-. / ..-. ..- .--- .- -.-.-- -.-.-- -.-.-- -.-.--",
                                        "", 
                                        ".--. .- .-. .- / ... . -- .--. .-. . / ..-. .- .-. . .. / .--. .- .-. - . / -.. . ... ... . / -.-. .- ... - . .-.. --- / .- --. --- .-. .-", 
                                        "-- . / ... .. -. - --- / ..-. .. -. .- .-.. -- . -. - . / ..-. . .-.. .. --..", "... ..- .- ... / .- - .-. --- -.-. .. -.. .- -.. . ... / -. .- --- / ..-. --- .-. .- -- / .--. . .-. -.. --- .- -.. .- ... --..-- / -- .- ... / --- -... .-. .. --. .- -.. --- / .--. --- .-. / -- . / .-.. .. -... . .-. - .- .-. / -.. --- / -- . ..- / .. -. ..-. . .-. -. --- --..-- / .- -.. . ..- ... .-.-.- .-.-.- .-.-.- .-.-.-"};
                                        
                                        // 1: Te peco que va embora
                                        // 2: Eu realmente não quero machucar você
                                        // 3: Você está piorando tudo, não posso controlar meu senhor, acabarei matando você, FUJA, POR FAVOR FUJA!!!!!
                                        // 4: Para sempre farei parte desse castelo agora
                                        // 5: Me sinto finalmente feliz
                                        // 6: Suas atrocidades nao foram perdoadas, mas obrigado por me libertar do meu inferno, Adeus....

                                        

    public Text txtFalas;

    public Text txtAvancar;

    private string text_avancar = "Pressione 'Q' para avançar";

    public static int contagem_falas_6;       

    public Image imagem;
    public GameObject painel_falas;                             

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public Sprite icon_meia_vida_ne;

    private float vida_maxima = 600f;

    public Transform spawn_1;
    public Transform spawn_2;
    public Transform spawn_3;
    public Transform spawn_4;
    public Transform spawn_5;
    public Transform spawn_6;

    private int valor_alet;

    public static bool podeCair;

    public GameObject tijolo_caindo;

    private float contador;

    private float tempo_de_cair;

    public GameObject painel_derrota;

    public static bool pode_comecar_6;

    public Sprite sprite_base;
    public Sprite sprite_meia_vida;

    public AudioSource fala_1;
    public AudioSource fala_2;
    public AudioSource fala_3;
    public AudioSource fala_4;
    public AudioSource fala_5;
    public AudioSource fala_6;

    private int tocarMusica;

    public AudioSource back_void;
    public AudioSource back;

    private bool back_pode;

    public GameObject chefao;
    private bool pode_normal;
    public GameObject escada;

    public GameObject vida_chefao;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase6 == 0) {   
            GameManager.Instance.SalvarSit(1, "Fase6");
        }

        if(GameManager.progresso <= 5) {
            GameManager.Instance.SalvarSit(6, "Progresso");
        }

        if(GameManager.fase6 == 0 || GameManager.fase6 == 1) {
            back_pode = false;
            pode_normal = true;
            tocarMusica = 0;
            tempo_de_cair = 1.5f;
            if(GameManager.sem_dialogos == 0) {
                if(Application.systemLanguage == SystemLanguage.Portuguese) {
                    txtAvancar.text = text_avancar;
                }
                contagem_falas_6 = 0;
                pode_comecar_6 = false;
                back_void.Play();
                painel_falas.SetActive(true);
                fala_1.Play();
                PlayerControle.conversando = true;
            } else {
                back.Play();
                pode_comecar_6 = true;
                PlayerControle.conversando = false;
                PlayerControle.podeAtirar = true;
                PlayerControle.pode_mexer = true;
            }
            contador = 0;
            podeCair = false;
            valor_alet = 0;
        } else {
            pode_normal = false;
            Destroy(chefao);
            escada.SetActive(true);
            Destroy(vida_chefao);
            PlayerControle.conversando = false;
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(pode_normal) {
            if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_6) {
            contagem_falas_6++;
        }

       BarraVida(); 

       contador += Time.deltaTime;

       if(Chefao05.vida <= 0) {
           GameManager.Instance.SalvarSit(2, "Fase6");
           if(GameManager.sem_dialogos == 1) {
               if(!back_pode) {
                   back_void.Stop();
                   back.Play();
                   back_pode = true;
               }
           }
           Destroy(vida_chefao);
       } else if(Chefao05.vida <= 300 && Chefao05.vida > 0) {
           tempo_de_cair = 0.85f;
           BarraVidaMaior.sprite = icon_meia_vida_ne;
           BarraVidaMaior.color = Color.red;
       }

       valor_alet = Random.Range(1, 7);

       if(podeCair && contador >= tempo_de_cair) {
           contador = 0;
            switch(valor_alet) {
                case 1:
                    Instantiate(tijolo_caindo, spawn_1.position, spawn_1.rotation);
                    break;
                case 2:
                    Instantiate(tijolo_caindo, spawn_2.position, spawn_2.rotation);
                    break;
                case 3:
                    Instantiate(tijolo_caindo, spawn_3.position, spawn_3.rotation);
                    break;    
                case 4:
                    Instantiate(tijolo_caindo, spawn_4.position, spawn_4.rotation);
                    break;    
                case 5:
                    Instantiate(tijolo_caindo, spawn_5.position, spawn_5.rotation);
                    break;    
                case 6:
                    Instantiate(tijolo_caindo, spawn_6.position, spawn_6.rotation);
                    break;    
            }
        }

        if(contagem_falas_6 <= 7 && contagem_falas_6 >= 0) {
            txtFalas.text = falas_chefao06[contagem_falas_6];
        }

        switch(contagem_falas_6) {
            case 1:
                fala_1.Stop();
                if(tocarMusica == 0) {
                    fala_2.Play();
                    tocarMusica = 1;
                }
                break;
            case 2:
                back_void.Stop();
                if(!back_pode) {
                    back.Play();
                    back_pode = true;
                }
                fala_2.Stop();
                PlayerControle.conversando = false;
                if(!pode_comecar_6) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                pode_comecar_6 = true;
                break;
            case 3:
                if(tocarMusica == 1) {
                    fala_3.Play();
                    tocarMusica = 2;
                }
                imagem.sprite = sprite_meia_vida;
                PlayerControle.conversando = true;
                painel_falas.SetActive(true);
                pode_comecar_6 = false;
                break;
            case 4:
                PlayerControle.conversando = false;
                fala_3.Stop();
                if(!pode_comecar_6) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                pode_comecar_6 = true;
                break;   
            case 5:
                back.Stop();
                if(back_pode) {
                    back_void.Play();
                    back_pode = false;
                }
                if(tocarMusica == 2) {
                    fala_4.Play();
                    tocarMusica = 3;
                }
                imagem.sprite = sprite_base;
                PlayerControle.conversando = true;
                painel_falas.SetActive(true);
                pode_comecar_6 = false;
                break;
            case 6:
                fala_4.Stop();
                if(tocarMusica == 3) {
                    fala_5.Play();
                    tocarMusica = 4;
                }
                break;
            case 7:
                fala_5.Stop();
                if(tocarMusica == 4) {
                    fala_6.Play();
                    tocarMusica = 5;
                }
                break;        
            case 8:
                fala_6.Stop();
                PlayerControle.conversando = false;
                if(!pode_comecar_6) {
                    PlayerControle.pode_mexer = true;
                    PlayerControle.podeAtirar = true;
                }
                painel_falas.SetActive(false);
                pode_comecar_6 = true;
                break;    

        }
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao05.vida / vida_maxima;
    }
}
