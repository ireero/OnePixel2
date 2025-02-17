using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instrucao : MonoBehaviour
{
    public Text txtInstrucao;
    private string text_instrucao_portugues = "Apertando para pular estando no ar resulta em um DOUBLE JUMP que faz você pular novamente.";
    private string text_instrucao_2_portugues = "Você acaba de ganhar um PET!, se você chegar a 1 de vida esse fofo bichano se sacrifica por você, resumindo: +1 de VIDA!.";

    private string text_instrucao_ingles = "Pressing jump while mid-air triggers a DOUBLE JUMP, allowing you to jump again.";
    private string text_instrucao_2_ingles = "You've just gained a PET! If your health drops to 1, this adorable little critter will sacrifice itself for you, granting you +1 LIFE!.";

    private string text_instrucao_chines = "在空中按下跳跃键会触发二段跳，让你能够再次跳跃。";
    private string text_instrucao_2_chines = "你刚刚获得了一只宠物！如果你的生命值降到1，这个可爱的小家伙将会牺牲自己，为你提供+1生命！";


    public int qual;

    public Text txtEntendido;
    private string text_entendido_portugues = "Entendido";
    private string text_entendido_ingles = "Understood";
    private string text_entendido_chines = "明白";

    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
                if(qual == 0) {
                    txtInstrucao.text = text_instrucao_portugues;
                } else if(qual == 1) {
                    txtInstrucao.text = text_instrucao_2_portugues;
                }
                txtEntendido.text = text_entendido_portugues;
            } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            if(qual == 0) {
                txtInstrucao.text = text_instrucao_chines;
            } else if(qual == 1) {
                    txtInstrucao.text = text_instrucao_2_chines;
            }
            txtEntendido.text = text_entendido_chines;
         } else {
            if(qual == 0) {
                txtInstrucao.text = text_instrucao_ingles;
            } else if(qual == 1) {
                txtInstrucao.text = text_instrucao_2_ingles;
            }
            txtEntendido.text = text_entendido_ingles;
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
