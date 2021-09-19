using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFases : MonoBehaviour
{
    public Sprite[] sprites_fases;

    public Image[] imagens_fases;

    public bool[] podeclicar;

    public AudioSource som_click;

    public Text[] nome_fases;
    private string[] text_nome_fases = {"Fase 0", "Fase 1", "Fase 2", "Fase 3", "Fase 4", "Fase 5", "Fase 6", "Fase 7", "Fase 8", "Fase 9", "Fase 10"};

    void Start()
    {
        Time.timeScale = 1;

        if(GameManager.fase0 != 0) {
            imagens_fases[0].sprite = sprites_fases[0];
            podeclicar[0] = true;
        }

        if(GameManager.fase1 != 0) {
            imagens_fases[1].sprite = sprites_fases[1];
            podeclicar[1] = true;
        }

        if(GameManager.fase2 != 0) {
            imagens_fases[2].sprite = sprites_fases[2];
            podeclicar[2] = true;
        }

        if(GameManager.fase3 != 0) {
            imagens_fases[3].sprite = sprites_fases[3];
            podeclicar[3] = true;
        }

        if(GameManager.fase4 != 0) {
            imagens_fases[4].sprite = sprites_fases[4];
            podeclicar[4] = true;
        }

        if(GameManager.fase5 != 0) {
            imagens_fases[5].sprite = sprites_fases[5];
            podeclicar[5] = true;
        }

        if(GameManager.fase6 != 0) {
            imagens_fases[6].sprite = sprites_fases[6];
            podeclicar[6] = true;
        }

        if(GameManager.fase7 != 0) {
            imagens_fases[7].sprite = sprites_fases[7];
            podeclicar[7] = true;
        }

        if(GameManager.fase8 != 0) {
            imagens_fases[8].sprite = sprites_fases[8];
            podeclicar[8] = true;
        }

        if(GameManager.fase9 != 0) {
            imagens_fases[9].sprite = sprites_fases[9];
            podeclicar[9] = true;
        }

        if(GameManager.fase10 != 0) {
            imagens_fases[10].sprite = sprites_fases[10];
            podeclicar[10] = true;
        }

        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            for(int i = 0; i <= 10; i++) {
                nome_fases[i].text = text_nome_fases[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneLoader.Instance.LoadSceneAsync("Menu");
        }
    }

    public void Tutorial() {
        som_click.Play();
        SceneLoader.Instance.LoadSceneAsync("Tutorial");
    }

    public void Fase0() {
        som_click.Play();
        if(podeclicar[0] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase0");
        }
    }

    public void Fase1() {
        som_click.Play();
        if(podeclicar[1] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase1");
        }
    }

    public void Fase2() {
        som_click.Play();
        if(podeclicar[2] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase2");
        }
    }

    public void Fase3() {
        som_click.Play();
        if(podeclicar[3] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase3");
        }
    }

    public void Fase4() {
        som_click.Play();
        if(podeclicar[4] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase4");
        }
    }

    public void Fase5() {
        som_click.Play();
        if(podeclicar[5] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase5");
        }
    }

    public void Fase6() {
        som_click.Play();
        if(podeclicar[6] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase6");
        }
    }

    public void Fase7() {
        som_click.Play();
        if(podeclicar[7] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase7");
        }
    }

    public void Fase8() {
        som_click.Play();
        if(podeclicar[8] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase8");
        }
    }

    public void Fase9() {
        som_click.Play();
        if(podeclicar[9] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase9");
        }
    }

    public void Fase10() {
        som_click.Play();
        if(podeclicar[10] == true) {
            SceneLoader.Instance.LoadSceneAsync("Fase10");
        }
    }
}
