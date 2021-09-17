using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaRisada : MonoBehaviour
{
    private CircleCollider2D collider_moeda;
    public static bool moeda_ativou;

    void Start()
    {
        moeda_ativou = false;
        collider_moeda = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            moeda_ativou = true;
            collider_moeda.isTrigger = true;
            Destroy(this.gameObject);
        }
    }
}
