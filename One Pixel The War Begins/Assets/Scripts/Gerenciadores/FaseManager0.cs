using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager0 : MonoBehaviour
{
    private Animator anim;
    public static int horaDePassar;
    public GameObject[] boss;
    public GameObject barra_de_vida;
    private bool umaVez;
    public GameObject painel_derrota;
    public GameObject painel_socorro;
    public Text txt_help;
    private string text_help = "Socorrro!";

    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase0 == 0) {
            GameManager.Instance.SalvarSit(1, "Fase0");
        }

        if(GameManager.progresso <= 0) {
            GameManager.Instance.SalvarSit(1, "Progresso");
        }

        if(GameManager.fase0 == 0 || GameManager.fase0 == 1) {
            horaDePassar = 0;
            PlayerControle.conversando = false;
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
            if(Application.systemLanguage == SystemLanguage.Portuguese) {
                txt_help.text = text_help;
            } 
        } else {
            if(GameManager.progresso >= 3) {
                Destroy(painel_socorro);
            } else {
                if(Application.systemLanguage == SystemLanguage.Portuguese) {
                    txt_help.text = text_help;
                } 
                painel_socorro.SetActive(true);
            }
            PlayerControle.conversando = false;
            PlayerControle.podeAtirar = true;
            PlayerControle.pode_mexer = true;
            horaDePassar = 5;
            umaVez = false;
            for(int i = 0; i <= 4; i++) {
                Destroy(boss[i]);
            }
            Destroy(barra_de_vida);
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(Time.timeScale == 1) {
            AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");
        }

        if(!umaVez) {
            if(horaDePassar == 5) {
                GameManager.Instance.SalvarSit(2, "Fase0");
                StartCoroutine("aparecerPainel");
                umaVez = true;
            }
        }
    }

    public void PararTremedeira() {
        anim.SetBool("tremer_chao", false);
    }

    IEnumerator aparecerPainel() {
        yield return new WaitForSeconds(1.2f);
        painel_socorro.SetActive(true);
    }
}
