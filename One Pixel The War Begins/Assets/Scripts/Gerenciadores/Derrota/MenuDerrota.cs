using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDerrota : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Reiniciar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrMenu() {
        SceneManager.LoadScene("Menu");
    }
}
