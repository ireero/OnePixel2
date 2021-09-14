using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pintura : MonoBehaviour
{
    public GameObject chao_fase;
    public float contador;
    public Sprite[] sprites_rachaduras;
    public Image imagem;
    private int ponto;

    void Start()
    {
        if(PlayerPrefs.HasKey("PONTO")) {
            ponto = PlayerPrefs.GetInt("PONTO");
        } else {
            PlayerPrefs.SetInt("PONTO", 0);
            ponto = PlayerPrefs.GetInt("PONTO");
        }
        contador = 0;
        imagem.sprite = sprites_rachaduras[ponto];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            contador += Time.deltaTime;
            if(contador >= 5f && contador < 8.3f && ponto == 0) {
                imagem.sprite = sprites_rachaduras[1];
                ponto++;
                PlayerPrefs.SetInt("PONTO", 1);
            } else if(contador >= 8.3f && contador < 10f && ponto == 1) {
                imagem.sprite = sprites_rachaduras[2];
                ponto++;
                PlayerPrefs.SetInt("PONTO", 2);
            } else if(contador >= 10f && contador < 14f && ponto == 2) {
                imagem.sprite = sprites_rachaduras[3];   
                ponto++;
                PlayerPrefs.SetInt("PONTO", 3);
            } else if(contador >= 14f && contador < 20f && ponto == 3) {
                imagem.sprite = sprites_rachaduras[4];
                ponto++;
                PlayerPrefs.SetInt("PONTO", 4);
            } else if(contador >= 20f && contador < 22f && ponto == 4) {
                imagem.sprite = sprites_rachaduras[5];
                ponto++;
                PlayerPrefs.SetInt("PONTO", 5);
            } else if(contador >= 22f && ponto == 5 && ponto == 5) {
                imagem.sprite = sprites_rachaduras[6];
                ponto++;
                PlayerPrefs.SetInt("PONTO", 6);
                chao_fase.SetActive(false);
                SceneLoader.Instance.LoadSceneAsync("Fase7_5");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(ponto == 6) {
                chao_fase.SetActive(false);
                SceneLoader.Instance.LoadSceneAsync("Fase7_5");
            }
        }
    }
}
