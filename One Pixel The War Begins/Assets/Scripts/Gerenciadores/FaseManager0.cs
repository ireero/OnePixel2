using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager0 : MonoBehaviour
{
    private Animator anim;
    public static int horaDePassar;
    public GameObject[] boss;
    public GameObject barra_de_vida;
    private bool umaVez;
    public GameObject painel_derrota;
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
        } else {
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

        AudioListener.volume = PlayerPrefs.GetFloat("VOLUME");

        if(!umaVez) {
            if(horaDePassar == 5) {
                GameManager.Instance.SalvarSit(2, "Fase0");
                umaVez = true;
            }
        }
    }

    public void PararTremedeira() {
        anim.SetBool("tremer_chao", false);
    }
}
