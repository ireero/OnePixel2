using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager10 : MonoBehaviour
{

    private string[] falas_pixel_preto = {"...", "Olá", "Não esperava te ver de novo", "Ao menos não nessa situação....", "Sabe, eu juro que tentei evitar tudo isso", "Eu juro que....", 
    "Deixa, não á nenhuma explicação que justifique esse massacre", "Infelizmente eu não tenho como voltar atrás", "é uma pena todo um povo sofrer por decisões de seus governantes, você não acha?", 
    "é uma pena um grupo especifico ser diminuido", "é uma pena que após anos de convivência em harmonia do nada viramos rivais", "não temos mais representante no trono", 
    "não temos mais nenhuma voz", "não somos mais todos iguais", "Somos constantemente ameaçados", "Vivemos com medo de ser atacados", "Dormimos com medo de ser assassinados", "Acordamos com medo de ser exterminados", 
    "E um dia simplesmente cansamos de ser humilhados e resolvemos revidar com retaliação....", "Desculpe, mas chegou a hora de você descansar."};

    public GameObject bolaFogo;
    private float delayTiro;

    public Transform[] spawn_cima;
    private int valor_aleatorio;

    private float contador;

    public Image BarraVidaMaior;
    public Image vida_restante;

    public Sprite cara_transformado;
    public Sprite vida_transformado;

    private float vida_maxima = 500f;

    public GameObject paredona;

    public Transform lado_esquerdo;
    public Transform lado_direito;

    private bool umaVez;

    void Start()
    {
        umaVez = false;
        contador = 0;
        valor_aleatorio = 0;
        delayTiro = 1.5f;    
        PlayerControle.conversando = false;
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(PixelPreto.evo_pixel == 1) {
            BarraVidaMaior.sprite = cara_transformado;
            vida_restante.sprite = vida_transformado;
        }

        BarraVida();
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

        if(PixelPreto.meia_vida) {
            if(!umaVez) {
                Instantiate(paredona, lado_direito.position, lado_direito.rotation);
                Instantiate(paredona, lado_esquerdo.position, lado_esquerdo.rotation);
                umaVez = true;
            }
        }
    }

    private void BarraVida() {
        vida_restante.fillAmount = PixelPreto.vida_pixel_preto / vida_maxima;
    }
}