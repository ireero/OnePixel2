using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlarDireito : MonoBehaviour
{
    private Transform player_pos;
    public static bool olhando_esquerda;
    public Transform virar;
    public Transform[] spawns_balas;
    public Transform normal;
    void Start()
    {
        olhando_esquerda = true;
        player_pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > player_pos.position.x && !olhando_esquerda) {
                Vector2 vetor = transform.localScale;
                vetor.x *= -1;
                transform.localScale = vetor;
                spawns_balas[0].rotation = normal.rotation;
                spawns_balas[1].rotation = normal.rotation;
                spawns_balas[2].rotation = normal.rotation;
                spawns_balas[3].rotation = normal.rotation;
                olhando_esquerda = true;
            } else if(player_pos.position.x > transform.position.x && olhando_esquerda){
                Vector2 vetor = transform.localScale;
                vetor.x *= -1;
                transform.localScale = vetor;
                spawns_balas[0].rotation = virar.rotation;
                spawns_balas[1].rotation = virar.rotation;
                spawns_balas[2].rotation = virar.rotation;
                spawns_balas[3].rotation = virar.rotation;
                olhando_esquerda = false;
            }
    }
}
