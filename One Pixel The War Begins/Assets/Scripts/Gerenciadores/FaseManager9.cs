using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager9 : MonoBehaviour
{
    public static float tempo_sobreviver;

    public Text txt_tempo;

    public GameObject painel_derrota;

    void Start()
    {
        tempo_sobreviver = 75f;
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerControle.player_morto == true) {
            painel_derrota.SetActive(true);
        }

        if(tempo_sobreviver > 0) {
            tempo_sobreviver -= Time.deltaTime;
        }
        txt_tempo.text = tempo_sobreviver.ToString("F0");

        if(tempo_sobreviver <= 50f && tempo_sobreviver > 25f) {
            Portal.atira_ae_po = 2;
        } else if(tempo_sobreviver <= 25f && tempo_sobreviver >= 0f) {
            Portal.atira_ae_po = 3;
        } else if(tempo_sobreviver <= 0) {
            Portal.atira_ae_po = 5;
        }
    }
}
