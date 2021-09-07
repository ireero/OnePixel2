using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedonaMaior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destruirIsso");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destruirIsso() {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }
}
