using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager8 : MonoBehaviour
{
    private string[] falas_cientista = {"O.... Oi Imperador", "sinceramente não imaginei que você fosse chegar até aqui", "Faziam meses que você não pisava no castelo", 
    "Me diga como foi sua expedição em busca de novas terras?", "Não está muito bem humorado não é mesmo?", "Entendo......", "Serei bem sincero com você imperador, eu não gosto de brigas", 
    "Então faça ai o que você quiser e vá pra onde você quiser que eu não ligo", "Tome esse presente por me deixar ir, tenho certeza que vai lhe ajudar bastante com os inimigos a frente"};

    public Text txtFalas;

    public static int contagem_falas_8;

    public GameObject painel_falas;

    public static bool pode_comecar_8;
    public static bool jabateuUmPapo;

    private bool umaVezGanho;

    void Start()
    {
        GameManager.Instance.SalvarSit(1, "Fase8");
        umaVezGanho = false;
        pode_comecar_8 = false;
        jabateuUmPapo = false;
        contagem_falas_8 = 0;
        PlayerControle.conversando = false;
        PlayerControle.pode_mexer = true;
        PlayerControle.podeAtirar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(pode_comecar_8) {
            PlayerControle.conversando = true;
            painel_falas.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Q) && pode_comecar_8) {
            contagem_falas_8++;
        }

        if(contagem_falas_8 <= 8 && contagem_falas_8 >= 0) {
            txtFalas.text = falas_cientista[contagem_falas_8];
        }

        if(contagem_falas_8 == 8) {
            if(!umaVezGanho) {
                PlayerControle.pet_ativado = true;
                PlayerPrefs.SetInt("Pet", 1);
                umaVezGanho = true;
            }
        }

        if(contagem_falas_8 == 9) {
            GameManager.Instance.SalvarSit(2, "Fase8");
            jabateuUmPapo = true;
            pode_comecar_8 = false;
            painel_falas.SetActive(false);
        }
    }
}
