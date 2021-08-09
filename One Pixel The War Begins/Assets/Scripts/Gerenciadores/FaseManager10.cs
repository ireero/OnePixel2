using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager10 : MonoBehaviour
{
    public GameObject bolaFogo;
    private float delayTiro;

    public Transform[] spawn_cima;
    private int valor_aleatorio;

    private float contador;

    void Start()
    {
        contador = 0;
        valor_aleatorio = 0;
        delayTiro = 1.5f;    
    }

    // Update is called once per frame
    void Update()
    {
        if(PixelPreto.AtirouJa) {
            valor_aleatorio = Random.Range(0, 6);
            contador += Time.deltaTime;
            if(PixelPreto.tirosDados > 0 && contador > delayTiro) {
                Instantiate(bolaFogo, spawn_cima[valor_aleatorio].position, spawn_cima[valor_aleatorio].rotation);
                PixelPreto.tirosDados--;
                contador = 0;
            } else if(PixelPreto.tirosDados == 0) {
                PixelPreto.atirarUmaVez = false;
            }
        }
    }
}
