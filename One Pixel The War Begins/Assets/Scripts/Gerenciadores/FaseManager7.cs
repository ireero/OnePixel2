using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager7 : MonoBehaviour
{
    public GameObject tentaculos;
    public GameObject monstro_olho;
    public Transform[] lista_spawns = new Transform[11];
    public Transform[] lista_baixo_spawns = new Transform[9];
    public static float cont;

    public static int monstros_nascidos;

    private int valor_aleatorio;
    public static bool liberado;
    private int valor_momentaneo;

    void Start()
    {
        valor_momentaneo = 0;;
        liberado = true;
        valor_aleatorio = 0;
        cont = 8.5f;
        monstros_nascidos = 0;    
    }

    // Update is called once per frame
    void Update()
    {

        cont += Time.deltaTime;

        if(Chefao06.atacando == true) {
            if(cont >= 9f) {
                nascerBichos(monstro_olho);
            }
        }

        if(Chefao06.camuflado_ja) {
            valor_aleatorio = Random.Range(0, 9);
        }

        if(Chefao06.camuflado_ja == true && liberado) {
            liberado = false;
            if(valor_aleatorio == valor_momentaneo) {
                if(valor_aleatorio == 8) {
                    valor_aleatorio--;
                } else {
                    valor_aleatorio++;
                }
            }
            Instantiate(tentaculos, lista_baixo_spawns[valor_aleatorio].position, lista_baixo_spawns[valor_aleatorio].rotation);
            valor_momentaneo = valor_aleatorio;
        }
    }

    void nascerBichos(GameObject monstro) {
        for(int i = 0; i <= 10; i++) {
            Instantiate(monstro, lista_spawns[i].position, lista_spawns[i].rotation);
        }
        cont = 0;
    }
}
