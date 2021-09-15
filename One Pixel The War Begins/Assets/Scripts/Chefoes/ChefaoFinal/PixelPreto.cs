using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPreto : MonoBehaviour
{
    public float contador;
    private Animator anim;
    private CapsuleCollider2D collider;
    private Rigidbody2D corpo;

    private float forca_pulo;
    public static bool atirarUmaVez;

    public GameObject bola_fogo;
    public Transform spawn_tiro;

    public static int tirosDados;
    public static bool AtirouJa;

    private bool podeAtirar;
    private float delayTiro;

    private float i;

    public Transform spawn_tiro_bala;

    public GameObject bala;

    public float contador2;

    private bool atirar_normal;
    private bool pularUmaVez;

    public Transform ponto_direita, ponto_esquerda;

    private float speed;

    public static bool estaNaDireita;

    private SpriteRenderer sr;

    private Vector3 myScale;

    public GameObject explosao;
    public Transform spawn_explosao;
    private bool pode_explosao;

    public static int evo_pixel;

    public static float vida_pixel_preto;
    public static bool meia_vida;

    private bool pode_comecar;

    public Transform ponto_meio;

    public static bool sugando;

    private bool voltarCv;
    public static bool atirarAdagas;
    private float cont_adaga;

    public static int atirou_adagas;

    public static bool hora_do_socao;
    public static bool suga_as_pedra;

    public GameObject explosao_meia_vida;

    public Transform spawn_tiro_segunda_explosao;

    public GameObject getsuga;

    private int getsugas_dados;

    public GameObject sulgador;

    private bool sair_daqui;

    void Start()
    {
        sair_daqui = false;
        getsugas_dados = 0;
        suga_as_pedra = false;
        hora_do_socao = false;
        atirou_adagas = 0;
        cont_adaga = 0;
        atirarAdagas = false;
        sugando = false;
        pode_comecar = false;
        meia_vida = false;
        evo_pixel = 0;
        vida_pixel_preto = 500f;
        pode_explosao = false;
        estaNaDireita = true;
        speed = 5f;
        pularUmaVez = false;
        atirar_normal = false;
        contador2 = 0;
        AtirouJa = false;
        i = 0;
        delayTiro = 0.4f;
        podeAtirar = false;
        tirosDados = 0;
        atirarUmaVez = false;
        forca_pulo = 380f;
        contador = 0;
        voltarCv = false;
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        corpo = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FaseManager10.pode_comecar_10) {
            if(evo_pixel == 0) {
                anim.SetBool("transformar", true);
                StartCoroutine("adicionarEvo");
            } else if(evo_pixel == 1) {
                anim.SetBool("transformar", false);
            }

            contador += Time.deltaTime;
            if(!meia_vida) {
                if(contador >= 5f && !atirarUmaVez) {
                    AtirouJa = false;
                    anim.SetBool("atirar_cima", true);
                    anim.SetBool("idle", false);
                    corpo.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    StartCoroutine("Atirar");
            }

                if(contador >= 28f && !pularUmaVez) {
                    anim.SetBool("rugindo", false);
                    anim.SetBool("andando", true);
                    atirar_normal = false;
                    corpo.constraints = RigidbodyConstraints2D.FreezeRotation;
                    if(estaNaDireita) {
                        corpo.bodyType = RigidbodyType2D.Kinematic;
                        transform.position = Vector2.MoveTowards(transform.position, ponto_esquerda.position, speed * Time.deltaTime);
                        if(transform.position.x <= ponto_esquerda.position.x) {
                            Pular();
                            estaNaDireita = false;
                        }
                    } else {
                        corpo.bodyType = RigidbodyType2D.Kinematic;
                        transform.position = Vector2.MoveTowards(transform.position, ponto_direita.position, speed * Time.deltaTime);
                        if(transform.position.x >= ponto_direita.position.x) {
                            Pular();
                            estaNaDireita = true;
                        }
                    }
                }

                if(podeAtirar) {
                    i += Time.deltaTime;
                    if(i > delayTiro && tirosDados <= 19) {
                        Instantiate(bola_fogo, spawn_tiro.position, spawn_tiro.rotation);
                        tirosDados++;
                        i = 0;
                    }
                }

                if(tirosDados >= 20) {
                    AtirouJa = true;
                    atirar_normal = true;
                    podeAtirar = false;
                    anim.SetBool("idle", true);
                    anim.SetBool("atirar_cima", false);
                    contador = 0;
                    pularUmaVez = false;
                }

                if(AtirouJa && atirar_normal) {
                    contador2 += Time.deltaTime;
                    if(contador >= 3f) {
                        anim.SetBool("rugindo", true);
                        anim.SetBool("idle", false);
                        StartCoroutine("Tirao");
                    }
                }
            } else {

                if(vida_pixel_preto <= 0) {
                    collider.isTrigger = true;
                    corpo.bodyType = RigidbodyType2D.Static;
                    anim.SetBool("morrer", true);
                }

                if(!pode_comecar) {
                    anim.SetBool("meia_vida", true);
                    corpo.bodyType = RigidbodyType2D.Dynamic;
                    corpo.constraints = RigidbodyConstraints2D.FreezeRotation;
                    StartCoroutine("sugandoAqui");
                } else {
                    if(!voltarCv) {
                        FaseManager10.contagem_falas_10 = 21;
                        anim.SetBool("meia_vida", false);
                        StartCoroutine("voltarOCv");
                    } else {
                        if(FaseManager10.pode_comecar_10) {
                            cont_adaga += Time.deltaTime;
                            if(evo_pixel == 1) {
                                if(estaNaDireita) {
                                    sr.color = Color.white;
                                    Vector3 vetor = transform.localScale;
                                    vetor.x *= -1;
                                    transform.localScale = vetor;
                                    anim.SetBool("modo_caveira", true);
                                    corpo.bodyType = RigidbodyType2D.Kinematic;
                                    evo_pixel++;
                                } else {
                                    sr.color = Color.white;
                                    anim.SetBool("modo_caveira", true);
                                    corpo.bodyType = RigidbodyType2D.Kinematic;
                                    evo_pixel++;
                                }
                            }

                            if(transform.position.y >= ponto_meio.position.y && !atirarAdagas) {
                                anim.SetBool("idle", true);
                                anim.SetBool("calma", false);
                            } else if(atirou_adagas <= 3) {
                                transform.position = Vector2.MoveTowards(transform.position, ponto_meio.position, speed * Time.deltaTime);
                            }
                            if(cont_adaga >= 2.5f && atirou_adagas <= 2) {
                                anim.SetBool("atirar_adaga", true);
                                atirarAdagas = true;
                                cont_adaga = 0;
                                atirou_adagas++;
                            } else if(atirou_adagas == 3) {
                                Paredona.sumir = true;
                                hora_do_socao = true;
                            }

                            if(cont_adaga >= 1.8f && atirou_adagas == 3 && hora_do_socao) {
                                anim.SetBool("socao_time", true);
                                cont_adaga = 0;
                                hora_do_socao = false;
                                StartCoroutine("voltarAnimSoco");
                                StartCoroutine("seguirPlay");
                            }

                            if(atirou_adagas == 4) {
                                anim.SetBool("idle", false);
                                if(estaNaDireita) {
                                    transform.position = Vector2.MoveTowards(transform.position, ponto_esquerda.position, speed * Time.deltaTime);
                                    if(transform.position.x <= ponto_esquerda.position.x) {
                                        Vector3 vetor = transform.localScale;
                                        vetor.x *= -1;
                                        transform.localScale = vetor;
                                        estaNaDireita = false;
                                        atirou_adagas++;
                                        anim.SetBool("pousando", true);
                                    }
                                } else {
                                    if(transform.position.x >= ponto_direita.position.x) {
                                        Vector3 vetor = transform.localScale;
                                        vetor.x *= -1;
                                        transform.localScale = vetor;
                                        estaNaDireita = true;
                                        atirou_adagas++;
                                        anim.SetBool("pousando", true);
                                    }
                                    transform.position = Vector2.MoveTowards(transform.position, ponto_direita.position, speed * Time.deltaTime);
                                }
                            } else if(atirou_adagas == 5) {
                                anim.SetBool("espadas", true);
                                if(cont_adaga >= 2f && getsugas_dados <= 6) {
                                    anim.SetBool("ataque_espadas", true);
                                    cont_adaga = 0;
                                    StartCoroutine("calmaEspada");
                                    if(getsugas_dados == 6) {
                                        atirou_adagas++;
                                    }
                                }
                            } else if(atirou_adagas == 6) {
                                anim.SetBool("espadas", false);
                                cont_adaga = 0;
                            } else if(atirou_adagas == 7) {
                                if(!sair_daqui) {
                                    anim.SetBool("idle_pousado", true);
                                    sair_daqui = true;
                                }
                                if(cont_adaga >= 1f && sair_daqui) {
                                    anim.SetBool("idle", true);
                                    anim.SetBool("idle_pousado", false);
                                    anim.SetBool("calma", true);
                                    anim.SetBool("pousando", false);
                                }
                                if(cont_adaga >= 4f && atirou_adagas == 7) {
                                    atirou_adagas = 0;
                                    cont_adaga = 0;
                                    FaseManager10.umaParedona = false;
                                    getsugas_dados = 0;
                                    sair_daqui = false;
                                }
                            }

                            if(!atirarAdagas) {
                                anim.SetBool("atirar_adaga", false);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("chao")) {
            Camera.tremer_chao = true;
            anim.SetBool("pulando", false);
            if(pode_explosao) {
                Instantiate(explosao, spawn_explosao.position, spawn_explosao.rotation);
                spawn_explosao.Rotate(new Vector3(0, 180, 0), Space.Self);
                pode_explosao = false;
            }
        }
        if(other.gameObject.CompareTag("bullet")) {
            vida_pixel_preto--;
            if(vida_pixel_preto <= 480) {
                meia_vida = true;
                AtirouJa = false;
            }
        } else if(other.gameObject.CompareTag("bullet_inimiga") || other.gameObject.CompareTag("super_bullet_inimiga")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if(other.gameObject.CompareTag("monstro")) {
            if(meia_vida) {
                StartCoroutine("podeComecarAe");
            }
        }
    }

    IEnumerator Atirar() {
        yield return new WaitForSeconds(1f);
        podeAtirar = true;
        atirarUmaVez = true;
    }

    IEnumerator voltarAnimSoco() {
        yield return new WaitForSeconds(0.55f);
        Instantiate(explosao_meia_vida, spawn_tiro_segunda_explosao.position, spawn_tiro_segunda_explosao.rotation);
        anim.SetBool("socao_time", false);
    }

    IEnumerator calmaEspada() {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("ataque_espadas", false);
    }

    public void atirarGetsuga() {
        Instantiate(getsuga, spawn_tiro_bala.position, spawn_tiro_bala.rotation);
        getsugas_dados++;
    }

    public void Sulgar() {
        sulgador.SetActive(true);
    }

    public void PararSugar() {
        sulgador.SetActive(false);
    }

    IEnumerator seguirPlay() {
        yield return new WaitForSeconds(1.2f);
        atirou_adagas++;
    }

    IEnumerator Tirao() {
        yield return new WaitForSeconds(0.8f);
        if(contador2 >= 2.5f) {
                GameObject cloneBullet = Instantiate(bala, spawn_tiro_bala.position, spawn_tiro_bala.rotation);
                contador2 = 0;
            }
    }

    IEnumerator adicionarEvo() {
        yield return new WaitForSeconds(0.2f);
        evo_pixel = 1;
    }

    IEnumerator voltarOCv() {
        yield return new WaitForSeconds(0.5f);
        voltarCv = true;
    }

    IEnumerator podeComecarAe() {
        yield return new WaitForSeconds(1.2f);
        sr.color = Color.red;
        pode_comecar = true;
    }

    IEnumerator sugandoAqui() {
        yield return new WaitForSeconds(1.5f);
        sugando = true;
    }

    void Pular() {
        corpo.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        pode_explosao = true;
        corpo.bodyType = RigidbodyType2D.Dynamic;
        myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
        pularUmaVez = true;
        corpo.AddForce(new Vector2(0, forca_pulo));
        anim.SetBool("pulando", true);
        anim.SetBool("andando", false);
    }

}
