using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDiagonal : MonoBehaviour
{
    public GameObject laser;

    public Transform spawn_laser;
    public Transform spawn_laser2;
    public Transform spawn_laser3;

    private float contador;
    private float contador2;
    private float contador3;

    private Animator anim;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();
        StartCoroutine("idle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        contador2 += Time.deltaTime;
        contador3 += Time.deltaTime;

        if(contador >= 2.5f) {
            Instantiate(laser, spawn_laser.position, spawn_laser.rotation);
            contador = 0;
        }

        if(contador2 >= 1f) {
            Instantiate(laser, spawn_laser2.position, spawn_laser2.rotation);
            contador2 = 0;
        }

        if(contador3 >= 4f) {
            Instantiate(laser, spawn_laser3.position, spawn_laser3.rotation);
            contador3 = 0;
        }

        if(FaseManager9.tempo_sobreviver <= 50) {
            anim.SetBool("idle", false);
            StartCoroutine("sumir");
        }
    }

    IEnumerator idle() {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("idle", true);
    }

    IEnumerator sumir() {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
}
