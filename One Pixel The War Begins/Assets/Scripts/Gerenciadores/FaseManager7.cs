using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseManager7 : MonoBehaviour
{
    public GameObject monstro_olho;
    public Transform[] lista_spawns = new Transform[11];
    public Transform[] lista_baixo_spawns = new Transform[9];
    private float contador;

    public static int monstros_nascidos;

    private int valor_aleatorio;

    void Start()
    {
        valor_aleatorio = 0;
        contador = 6f;
        monstros_nascidos = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Chefao06.atacando == true) {
            contador += Time.deltaTime;
            if(contador >= 6f) {
                nascerBichos(monstro_olho);
            }
        }
    }

    void nascerBichos(GameObject monstro) {
        for(int i = 0; i <= 10; i++) {
            Instantiate(monstro, lista_spawns[i].position, lista_spawns[i].rotation);
        }
        contador = 0;
    }
}
