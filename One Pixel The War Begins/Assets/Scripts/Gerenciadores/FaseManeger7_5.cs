using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManeger7_5 : MonoBehaviour
{
    public GameObject alerta;
    public GameObject spawn;
    public GameObject energia;
    public GameObject Red_i;
    public GameObject gatilho;

    public GameObject paredona;
    
    void Start()
    {
        GameManager.Instance.CarregarDados();
        if(GameManager.fase7_5 == 2) {
            Destroy(alerta);
            Destroy(spawn);
            Destroy(energia);
            Destroy(Red_i);
            Destroy(gatilho);
        } else if(GameManager.fase7_5 == 1) {
            Instantiate(paredona, spawn.transform.position, spawn.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
