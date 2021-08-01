using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFases : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial() {
        SceneLoader.Instance.LoadSceneAsync("Tutorial");
    }

    public void Fase1() {
        SceneLoader.Instance.LoadSceneAsync("Fase1");
    }

    public void Fase2() {
        SceneLoader.Instance.LoadSceneAsync("Fase2");
    }

    public void Fase3() {
        SceneLoader.Instance.LoadSceneAsync("Fase3");
    }

    public void Fase4() {
        SceneLoader.Instance.LoadSceneAsync("Fase4");
    }

    public void Fase5() {
        SceneLoader.Instance.LoadSceneAsync("Fase5");
    }

    public void Fase6() {
        SceneLoader.Instance.LoadSceneAsync("Fase6");
    }

    public void Fase7() {
        SceneLoader.Instance.LoadSceneAsync("Fase7");
    }

    public void Fase8() {
        SceneLoader.Instance.LoadSceneAsync("Fase8");
    }

    public void Fase9() {
        SceneLoader.Instance.LoadSceneAsync("Fase9");
    }

    public void Fase10() {
        SceneLoader.Instance.LoadSceneAsync("Fase10");
    }
}
