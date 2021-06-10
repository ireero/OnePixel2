using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroMorrer : MonoBehaviour
{
    private float velocidade = 0.1f;
    private float contagem;

    // Start is called before the first frame update
    void Start()
    {
        contagem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contagem += Time.deltaTime;
        if(contagem <= 4f) {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Chefoes")) {
            Destroy(this.gameObject);
        }
    }
}
