using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sair() {
        Application.Quit();
    }

    public void Jogar() {
        SceneManager.LoadScene(1);
    }

    public void IrMenuFases() {
        SceneManager.LoadScene(12);
    }
}
