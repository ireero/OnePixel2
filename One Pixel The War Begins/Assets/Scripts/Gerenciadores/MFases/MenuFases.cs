using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFases : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial() {
        SceneManager.LoadScene(11);
    }

    public void Fase1() {
        SceneManager.LoadScene(1);
    }

    public void Fase2() {
        SceneManager.LoadScene(2);
    }

    public void Fase3() {
        SceneManager.LoadScene(3);
    }

    public void Fase4() {
        SceneManager.LoadScene(4);
    }

    public void Fase5() {
        SceneManager.LoadScene(5);
    }

    public void Fase6() {
        SceneManager.LoadScene(6);
    }

    public void Fase7() {
        SceneManager.LoadScene(7);
    }

    public void Fase8() {
        SceneManager.LoadScene(8);
    }

    public void Fase9() {
        SceneManager.LoadScene(9);
    }

    public void Fase10() {
        SceneManager.LoadScene(10);
    }
}
