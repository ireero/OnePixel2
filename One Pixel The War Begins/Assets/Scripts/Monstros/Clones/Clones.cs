using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clones : MonoBehaviour
{

    public Transform spawn_tiro;
    public GameObject tiro;
    private float contador;
    // Start is called before the first frame update
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= 2f) {
            Instantiate(tiro, spawn_tiro.position, spawn_tiro.rotation);
            contador = 0;
        }
    }
}
