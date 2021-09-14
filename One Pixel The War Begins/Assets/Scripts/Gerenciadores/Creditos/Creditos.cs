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

    private string textinho = "Sem o apoio de vocês esse jogo não seria o que ele é e talvez nem teria sido lançado, muito obrigado <3.";
    private string um_jogo_por = "Um jogo feito por:";
    private string codigo_design = "Código e Design";
    private string musicas = "Músicas";
    private string agrad_especiais = "Agradecimentos Especiais";

    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Portuguese) {
            txt_textinho.text = textinho;
            txt_jogo_por.text = um_jogo_por;
            txt_cod_desi.text = codigo_design;
            txt_musicas.text = musicas;
            txt_agrad_especiais.text = agrad_especiais;
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
