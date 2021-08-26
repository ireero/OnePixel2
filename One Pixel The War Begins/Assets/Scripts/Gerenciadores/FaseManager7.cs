using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager7 : MonoBehaviour
{

    public Image BarraDeVida;
    public Image BarraVidaMaior;
    public Sprite chefao_meia_vida;

    public GameObject tentaculos;
    public GameObject monstro_olho;
    public Transform[] lista_spawns = new Transform[11];
    public Transform[] lista_baixo_spawns = new Transform[9];
    public static float cont;

    public static int monstros_nascidos;

    private float vida_maxima = 300f;

    private int valor_aleatorio;
    public static bool liberado;
    private int valor_momentaneo;
    public Image back;

    private float tempo_spawn;
    public Sprite back_2;
    public Sprite back_1;

    public GameObject painel_derrota;

    public GameObject chefao;
    public GameObject vida_chefao;

    public GameObject gatilho;
    public GameObject escada;

    public AudioSource som_spawn_tentaculos;

    public AudioSource som_void;
    public AudioSource musica_background;

    private int musicas_at;

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase7 == 0) { 
            GameManager.Instance.SalvarSit(1, "Fase7");
        }

        if(GameManager.progresso <= 6) {
            GameManager.Instance.SalvarSit(7, "Progresso");
        }
        som_void.Play();

        PlayerControle.conversando = false;
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;

        if(GameManager.fase7 == 0 || GameManager.fase7 == 1) {
            musicas_at = 0;
            back.sprite = back_1;
            back.color = Color.white;
            tempo_spawn = 10.5f;
            valor_momentaneo = 0;;
            liberado = true;
            TiroPequenoChefao.modoHard = false;
            valor_aleatorio = 0;
            cont = 8.5f;
            monstros_nascidos = 0;   
            Chefao06.meia_vida = false; 
            Chefao06.camuflado_ja = false;
        } else {
            escada.SetActive(true);
            Destroy(gatilho);
            back.sprite = back_2;
            back.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {

        BarraVida();

        if(AtivarChefao.ativarOlhao == true) {
            if(musicas_at == 0) {
                som_void.Stop();
                musica_background.Play();
                musicas_at++;
            }
            chefao.SetActive(true);
            vida_chefao.SetActive(true);
        }

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        cont += Time.deltaTime;

        if(Chefao06.atacando == true) {
            if(cont >= tempo_spawn) {
                nascerBichos(monstro_olho);
            }
        }

        if(Chefao06.meia_vida == true) {
            BarraVidaMaior.sprite = chefao_meia_vida;
            BarraVidaMaior.color = Color.red;
            back.sprite = back_2;
            back.color = Color.red;
            tempo_spawn = 8.5f;
        }

        if(Chefao06.ta_mortin) {
            if(musicas_at == 1) {
                som_void.Play();
                musica_background.Stop();
            }
            GameManager.Instance.SalvarSit(2, "Fase7");
            Destroy(vida_chefao);
            back.color = Color.white;
            AtivarChefao.ativarOlhao = false;
        }

        if(Chefao06.camuflado_ja) {
            valor_aleatorio = Random.Range(0, 9);
        }

        if(Chefao06.camuflado_ja == true && liberado) {
            liberado = false;
            if(valor_aleatorio == valor_momentaneo) {
                if(valor_aleatorio == 8) {
                    valor_aleatorio--;
                } else {
                    valor_aleatorio++;
                }
            }
            Instantiate(tentaculos, lista_baixo_spawns[valor_aleatorio].position, lista_baixo_spawns[valor_aleatorio].rotation);
            StartCoroutine("somNascerTent");
            valor_momentaneo = valor_aleatorio;
        }
    }

    void nascerBichos(GameObject monstro) {
        for(int i = 0; i <= 10; i++) {
            Instantiate(monstro, lista_spawns[i].position, lista_spawns[i].rotation);
        }
        cont = 0;
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao06.vida_restante / vida_maxima;
    }

    IEnumerator somNascerTent() {
        yield return new WaitForSeconds(1f);
        som_spawn_tentaculos.Play();
    }
}
