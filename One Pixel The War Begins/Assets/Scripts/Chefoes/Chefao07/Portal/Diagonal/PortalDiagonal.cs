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

    private float[] tempos = {2.5f, 1f, 4f};

    private AudioSource som_lazer;

    void Start()
    {
        contador = 0;
        anim = GetComponent<Animator>();
        som_lazer = GetComponent<AudioSource>();
        if(GameManager.sem_dialogos == 1) {
            tempos[0] = 2.25f;
            tempos[1] = 0.75f;
            tempos[2] = 3.75f;
        }
        StartCoroutine("idle");
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        contador2 += Time.deltaTime;
        contador3 += Time.deltaTime;

        if(contador >= tempos[0]) {
            som_lazer.Play();
            Instantiate(laser, spawn_laser.position, spawn_laser.rotation);
            contador = 0;
        }

        if(contador2 >= tempos[1]) {
            som_lazer.Play();
            Instantiate(laser, spawn_laser2.position, spawn_laser2.rotation);
            contador2 = 0;
        }

        if(contador3 >= tempos[2]) {
            som_lazer.Play();
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
