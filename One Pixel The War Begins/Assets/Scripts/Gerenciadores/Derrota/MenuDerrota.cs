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
        SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void IrMenu() {
        SceneLoader.Instance.LoadSceneAsync("Menu");
    }
}
