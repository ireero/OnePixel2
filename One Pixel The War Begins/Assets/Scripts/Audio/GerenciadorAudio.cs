using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorAudio : MonoBehaviour
{
    public AudioSource som_pulo;
    public AudioSource som_tiro;
    public AudioSource som_morte;
    public AudioSource som_dash;

    public static GerenciadorAudio inst = null;

    void Awake() {
        if(inst == null) {
            inst = this;
        } else if(inst != this) {
            Destroy(gameObject);
        }
    }

    public void PlayPulo(AudioClip clipAudio) {
        som_pulo.clip = clipAudio;
        som_pulo.Play();
    }

    public void PlayTiro(AudioClip clipAudio) {
        som_tiro.clip = clipAudio;
        som_tiro.Play();
    }

    public void PlayMorte(AudioClip clipAudio) {
        som_morte.clip = clipAudio;
        som_morte.Play();
    }

    public void PlayDash(AudioClip clipAudio) {
        som_dash.clip = clipAudio;
        som_dash.Play();
    }

}
