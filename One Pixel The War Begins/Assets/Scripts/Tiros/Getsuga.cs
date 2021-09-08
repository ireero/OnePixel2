using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getsuga : MonoBehaviour
{
    private PolygonCollider2D collider;
    private float velocidade = 12.8f;
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
    }
}
