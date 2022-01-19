using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorAudio : MonoBehaviour
{

    public AudioSource sair_sons_background;
    public AudioSource sair_sons_sfx;
    public AudioSource sair_sons_sfx_chefoes;
    public AudioSource sair_sons_sfx_monstros;

    public AudioSource sfx_dano_chefao;
    public AudioSource sfx_som_extra_chefao;
    public AudioSource sfx_dano_temporario_chefao;

    public AudioClip[] sons_sfx;
    public AudioClip[] sons_background;
    public AudioClip[] sons_sfx_monstros;

    protected int valor_cena;

    public static GerenciadorAudio inst = null;

    void Awake() {
        if(inst == null) {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        } else if(inst != this) {
            Destroy(gameObject);
        }
        valor_cena = SceneManager.GetActiveScene().buildIndex;
    }

    public void Update() {

        if(SceneManager.GetActiveScene().buildIndex == valor_cena) {
            if(!sair_sons_background.isPlaying) {
                EscolherSomBackground(valor_cena);
                sair_sons_background.Play();
            }
        } else {
            sair_sons_background.Stop();
            valor_cena = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void EscolherSomBackground(int qualCena) {
        if(!sair_sons_background.isPlaying) {
                sair_sons_background.clip = sons_background[qualCena];
                sair_sons_background.Play();
            }
    }

    public void PausarSomBackGround() {
        sair_sons_background.Pause();
    }

    public void DespausarSomBackGround() {
        sair_sons_background.Play();
    }

    public void PlaySomSfx(int numeroSom, string oqueEIsso) {
        if(oqueEIsso == "SfxPlayer") {
            sair_sons_sfx.clip = sons_sfx[numeroSom];
            sair_sons_sfx.Play();
        } else if(oqueEIsso == "SfxMonstros") {
            sair_sons_sfx_monstros.clip = sons_sfx_monstros[numeroSom];
            sair_sons_sfx_monstros.Play();
        }
    }

}
