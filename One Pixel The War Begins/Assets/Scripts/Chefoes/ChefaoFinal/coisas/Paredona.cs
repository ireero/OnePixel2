using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paredona : MonoBehaviour
{
    public static bool sumir;
    private Animator anim;
    void Start()
    {
        sumir = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sumir) {
            anim.SetBool("sumir", true);
            Destroy(gameObject, 0.6f);
        }
    }
}
