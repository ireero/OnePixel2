using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseManager : MonoBehaviour
{

    public GameObject monstros_base;

    public Transform spawn_1;
    public Transform spawn_2;
    public Transform spawn_3;
    public Transform spawn_4;
    public Transform spawn_5;
    public Transform spawn_6;

    public float contagem;
    public int valor_alet;
    public static bool podeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        podeSpawn = true;
        valor_alet = 0;
        contagem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contagem += Time.deltaTime;
        if(contagem >= 3.5f && podeSpawn) {
            valor_alet = Random.Range(1, 7);
            switch(valor_alet) {
                case 1:
                    Instantiate(monstros_base, spawn_1.position, spawn_1.rotation);
                    MetadeVida();
                    break;
                case 2:
                    Instantiate(monstros_base, spawn_2.position, spawn_2.rotation);
                    MetadeVida();
                    break;
                case 3:
                    Instantiate(monstros_base, spawn_3.position, spawn_3.rotation);
                    MetadeVida();
                    break;    
                case 4:
                    Instantiate(monstros_base, spawn_4.position, spawn_4.rotation);
                    MetadeVida();
                    break;    
                case 5:
                    Instantiate(monstros_base, spawn_5.position, spawn_5.rotation);
                    MetadeVida();
                    break;    
                case 6:
                    Instantiate(monstros_base, spawn_6.position, spawn_6.rotation);
                    MetadeVida();
                    break;    
            }
        }
    }

    private void MetadeVida() {
        if(Chefao01.metade_vida == true) {
            contagem = 2.25f;
        } else {
            contagem = 0;
        }
    }
}
