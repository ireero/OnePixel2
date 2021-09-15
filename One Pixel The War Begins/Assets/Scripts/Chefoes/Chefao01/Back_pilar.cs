using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_pilar : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PararTremedeira() {
        anim.SetBool("tremer_chao", false);
    }
}
