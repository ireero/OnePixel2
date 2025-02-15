using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instrucao : MonoBehaviour
{
    public Text txtInstrucao;
    private string text_instrucao = "Apertando para pular estando no ar resulta em um DOUBLE JUMP que faz você pular novamente.";
    private string text_instrucao_2 = "Você acaba de ganhar um PET!, se você chegar a 1 de vida esse fofo bichano se sacrifica por você, resumindo: +1 de VIDA!.";

    public int qual;

    public Text txtEntendido;
    private string text_entendido = "Entendido";

    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
                if(qual == 0) {
                    txtInstrucao.text = text_instrucao;
                } else if(qual == 1) {
                    txtInstrucao.text = text_instrucao_2;
                }
                txtEntendido.text = text_entendido;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fechar() {
        this.gameObject.SetActive(false);
    }
}
