using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseManager5 : MonoBehaviour
{
    private string[] falas_palhaco = {"EI, Olá!", "Você não me parece muito bem", "Se eu vou te deixar passar?", "Claro que não amigo :)", 
    "Por que estou fazendo isso?", "Existem vários motivos sabe....", "Mas o maior deles é porque o caos me faz sorrir","", "Você é realmente forte imperador", 
    "Eu aguardei loucamente por esse momento dês do dia que você mandou seus vassalos me deixarem cego", "O Pixel preto me acolheu, me deu forças, me deu PODER!!!", 
    "E agora finalmente posso lhe matar. É uma pena que eu não posso lhe ver sofrer meu imperador.....",  "Isso é realmente.....", "Engraçado..."};

    public Text txtFalas;

    public static int contagem_falas_5;

    public Image imagem;
    public GameObject painel_falas;

    public Sprite palhaco_normal;
    public Sprite palhaco_surpreso;
    public Sprite palhaco_triste;
    public Sprite palhaco_ameacador;

    public Sprite palhaco_normal_mv;
    public Sprite palhaco_triste_mv;
    public Sprite palhaco_ameacador_mv;
    public Sprite palhaco_euforico_mv;

    public static bool pode_comecar_5;

    public GameObject moeda_risada;

    public Transform ponto_baixo;

    private float contador;
    private bool umaVez;

    public Image BarraDeVida;
    public Image BarraVidaMaior;

    public Sprite icon_metade_vida;

    private float vida_maxima = 50f;

    public static int valor_tiros_dados = 8;

    void Start()
    {
        painel_falas.SetActive(true);
        pode_comecar_5 = false;
        contagem_falas_5 = 0;
        PlayerControle.podeAtirar = false;
        PlayerControle.pode_mexer = false;
        umaVez = false;
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q) && !pode_comecar_5) {
            contagem_falas_5++;
        }

        if(contagem_falas_5 <= 13 && contagem_falas_5 >= 0) {
            txtFalas.text = falas_palhaco[contagem_falas_5];
        }

        switch(contagem_falas_5) {
            case 2:
                imagem.sprite = palhaco_surpreso;
                break;
            case 3:
                imagem.sprite = palhaco_normal;
                break;   
            case 4:
                imagem.sprite = palhaco_surpreso;
                break;
            case 5:
                imagem.sprite = palhaco_triste;
                break;
            case 6:
                imagem.sprite = palhaco_ameacador;
                break;     
            case 7:
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                painel_falas.SetActive(false);
                pode_comecar_5 = true;
                break; 
            case 8:
                imagem.sprite = palhaco_normal_mv;
                pode_comecar_5 = false;
                painel_falas.SetActive(true);
                PlayerControle.pode_mexer = false;
                PlayerControle.podeAtirar = false;
                break;    
            case 9:
                imagem.sprite = palhaco_triste_mv;
                break;      
            case 10:
                imagem.sprite = palhaco_euforico_mv;
                break;
            case 11:
                imagem.sprite = palhaco_triste_mv;
                break;
            case 13:
                imagem.sprite = palhaco_ameacador_mv;
                break;
            case 14:
                PlayerControle.pode_mexer = true;
                PlayerControle.podeAtirar = true;
                painel_falas.SetActive(false);
                pode_comecar_5 = true;      
                break;                      
        }

        BarraVida();

        if(Chefao04.tirosDados == 0) {
            umaVez = false;
        }

        if(Chefao04.tirosDados >= valor_tiros_dados && !umaVez) {
                Instantiate(moeda_risada, ponto_baixo.position, ponto_baixo.rotation);
                umaVez = true;
        }

        if(Chefao04.vida_chefao == 0) {
            Destroy(BarraVidaMaior);
        } else if(Chefao04.vida_chefao <= 25 && Chefao04.vida_chefao > 0) {
            BarraVidaMaior.sprite = icon_metade_vida;
            TiroPequenoChefao.modoHard = true;
            BarraVidaMaior.color = Color.red;
        }
    }

    private void BarraVida() {
        BarraDeVida.fillAmount = Chefao04.vida_chefao / vida_maxima;
    }
}
