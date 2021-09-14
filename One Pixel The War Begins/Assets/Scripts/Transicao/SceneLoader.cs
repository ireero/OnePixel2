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

    private string text_carregando = "Carregando...";

    private void Awake() {

        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txtCarregando.text = text_carregando;
        }
        
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    public void LoadSceneAsync(string sceneName) {
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
