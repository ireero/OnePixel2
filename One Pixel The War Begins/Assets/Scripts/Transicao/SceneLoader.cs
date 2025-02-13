using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingOverlay;
    [SerializeField] 
    [Range(0.01f, 3f)]  
    private float fadeTime = 0.5f;

    public static SceneLoader Instance {get; private set;}

    public Text txtCarregando;

    private string text_carregando_portugues = "Carregando...";
    private string text_carregando_ingles = "Loading...";
    private string text_carregando_chines = "正在加载...";

    private void Awake() {

        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txtCarregando.text = text_carregando_portugues;
        } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            txtCarregando.text = text_carregando_chines;
         } else {
            txtCarregando.text = text_carregando_ingles;
         }
        
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    public void LoadSceneAsync(string sceneName) {
        if(PlayerControle.type_red == 1 || PlayerControle.type_red == 2) {
            if(PlayerControle.red_var) {
                if(PlayerControle.cont_red > 0 && PlayerControle.cont_red <= 30) {
                    PlayerPrefs.SetFloat("CONT_RED", PlayerControle.cont_red);
                }

                if(PlayerControle.cont_volt_red > 0 && PlayerControle.cont_volt_red <= 121) {
                    PlayerPrefs.SetFloat("CONT_VOLT_RED", PlayerControle.cont_volt_red);
                }

            } else {
                if(PlayerControle.cont_volt_red > 0 && PlayerControle.cont_volt_red <= 121) {
                    PlayerPrefs.SetFloat("CONT_VOLT_RED", PlayerControle.cont_volt_red);
            } 

            if(PlayerControle.cont_red > 0 && PlayerControle.cont_red <= 30) {
                    PlayerPrefs.SetFloat("CONT_RED", PlayerControle.cont_red);
                }  
        }
        if(GameManager.tempo_de_jogo < 60f) {
            PlayerPrefs.SetFloat("TEMPO_JOGO", GameManager.tempo_de_jogo);
        }
        }
    StartCoroutine(PerformLoadSceneAsync(sceneName));
    }

    private IEnumerator PerformLoadSceneAsync(string sceneName) {

        yield return StartCoroutine(FadeIn());
        

        var operation = SceneManager.LoadSceneAsync(sceneName);
        while(operation.isDone == false) {
            yield return null;
        }

        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn() {
        float start = 0;
        float end = 1;
        float speed = (end - start) / fadeTime;
        loadingOverlay.alpha = start;
        while(loadingOverlay.alpha < end) {
            loadingOverlay.alpha  += speed * Time.deltaTime;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }

    private IEnumerator FadeOut() {
        float start = 1;
        float end = 0;
        float speed = (end - start) / fadeTime;
        loadingOverlay.alpha = start;
        while(loadingOverlay.alpha > end) {
            loadingOverlay.alpha  += speed * Time.deltaTime;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }
}
