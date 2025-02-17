using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creditos : MonoBehaviour
{
    public Text txt_textinho;
    public Text txt_jogo_por;
    public Text txt_cod_desi;
    public Text txt_musicas;
    public Text txt_agrad_especiais;

    private string textinho_portugues = "Sem o apoio de vocês esse jogo não seria o que ele é e talvez nem teria sido lançado, muito obrigado <3.";
    private string um_jogo_por_portugues = "Um jogo feito por:";
    private string codigo_design_portugues = "Código e Design";
    private string musicas_portugues = "Músicas";
    private string agrad_especiais_portugues = "Agradecimentos Especiais";

    private string textinho_ingles = "Without your support this game wouldn't be what it is and perhaps wouldn't have been released, thank you very much <3.";
    private string um_jogo_por_ingles = "A game made by:";
    private string codigo_design_ingles = "Code and Design";
    private string musicas_ingles = "Music";
    private string agrad_especiais_ingles = "Special Thanks";

    private string textinho_chines = "没有你们的支持，这款游戏就不会是现在的样子，甚至可能不会发布，非常感谢 <3.";
    private string um_jogo_por_chines = "游戏制作者";
    private string codigo_design_chines = "代码与设计";
    private string musicas_chines = "音乐";
    private string agrad_especiais_chines = "特别感谢";

    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_textinho.text = textinho_portugues;
            txt_jogo_por.text = um_jogo_por_portugues;
            txt_cod_desi.text = codigo_design_portugues;
            txt_musicas.text = musicas_portugues;
            txt_agrad_especiais.text = agrad_especiais_portugues;
        } else if (Application.systemLanguage == SystemLanguage.Chinese ||
         Application.systemLanguage == SystemLanguage.ChineseSimplified ||
         Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            txt_textinho.text = textinho_chines;
            txt_jogo_por.text = um_jogo_por_chines;
            txt_cod_desi.text = codigo_design_chines;
            txt_musicas.text = musicas_chines;
            txt_agrad_especiais.text = agrad_especiais_chines;
         } else {
            txt_textinho.text = textinho_ingles;
            txt_jogo_por.text = um_jogo_por_ingles;
            txt_cod_desi.text = codigo_design_ingles;
            txt_musicas.text = musicas_ingles;
            txt_agrad_especiais.text = agrad_especiais_ingles;
         }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneLoader.Instance.LoadSceneAsync("Menu");
        }
    }
}
