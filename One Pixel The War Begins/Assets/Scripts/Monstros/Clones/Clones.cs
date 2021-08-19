using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clones : MonoBehaviour
{

    public Transform spawn_tiro;
    public GameObject tiro;
    private float contador;

    private float speed = 6f;

    public Transform local_chefao;
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

        if(PixelPreto.meia_vida) {
            contador = 0;
            transform.position = Vector2.MoveTowards(transform.position, local_chefao.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes")) {
            Destroy(this.gameObject);
        }
    }
}
