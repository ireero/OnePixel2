using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class PlayerControle : MonoBehaviour {
   public Transform groundCheck;
   public float speed = 4;
   public float jumpForce = 200;
   public LayerMask whatIsGround;
 
   [HideInInspector]
   public bool lookingRight = true;

   public static bool player_morto;
 
   private Rigidbody2D rb2d;
   private bool isGrounded = false;
   private bool jump = false;
   private bool abaixado = true;

   private Animator anim;

   public Transform bulletSpawn;
   public GameObject bulletObject;
   

   public static bool podeAtirar;
   public static bool pode_mexer;

   private bool caiu;

   public GameObject plataforma;
   public Transform chaoSpawn;
   private bool podePor;

   // Sons do Player
   public AudioClip som_pulo;
   public AudioClip som_morte;
   public AudioClip som_tiro;

   private BoxCollider2D player_collider;

   private float dashAtual;
   private bool canDash;
   private bool isDashing;
   private float duracaoDash;
   private float dashSpeed;

   public Image Barra_de_vida;
   public Image valor_vida;
   public Image BarraVidaMaior;

   public Sprite icon_morte;

   public Animator anim_gatilhos;
   public ParticleSystem dust;

   private SpriteRenderer sr;
   public GameObject pet;
   public static bool pet_ativado;
   public Animator anim_pet;

   public GameObject vida_pet;

   public static bool parado;

   private bool doubleJump;

   public static bool jaPodePularDuas;
   public GameObject simbuloVoador;

   private int pularPegar;
 
   void Start () {  
      pularPegar = PlayerPrefs.GetInt("PularDuas");
      if(pularPegar == 1) {
         jaPodePularDuas = true;
      } else {
         jaPodePularDuas = false;
      }
      doubleJump = true;
      parado = true;
      player_morto = false;
      pet_ativado = true;
      pode_mexer = true;
      podeAtirar = true;
      Barra_de_vida.fillAmount = 1;
      isDashing = false;
      canDash = true;
      dashSpeed = 20;
      duracaoDash = 0.1f;
      dashAtual = duracaoDash;
      player_collider = GetComponent<BoxCollider2D>();
      podePor = false;
      caiu = false;
      anim = GetComponent<Animator>();
      rb2d = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
   }
 
   void Update () {

      if(jaPodePularDuas) {
         simbuloVoador.SetActive(true);
      } else {
         simbuloVoador.SetActive(false);
      }

      if(rb2d.velocity == new Vector2(0, 0)) {
         parado = true;
      } else {
         parado = false;
      }
      
      if(pode_mexer) {
         inputCheck ();
         move ();
         Dash();
      }

      if(!isGrounded) {
         podePor = true;
         anim.SetBool("pulou", false);
         anim.SetBool("idle", false);
         anim_gatilhos.SetBool("poeira", false);
      } else {
         anim_gatilhos.SetBool("asas", false);
         doubleJump = true;
         anim_gatilhos.SetBool("poeira", true);
         podePor = false;
         caiu = true;
         anim.SetBool("caiu", true);
         StartCoroutine("posQueda");
      }

      if(pet_ativado) {
         vida_pet.SetActive(true);
         pet.SetActive(true);
      } else {
         vida_pet.SetActive(false);
      }
   }
 
   void inputCheck (){
      if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded){
      jump = true;
      } else if(((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded) && caiu) {
         jump = true;
         anim.SetBool("caiu", false);
      } else if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !isGrounded && doubleJump) {
         if(jaPodePularDuas) {
            jump = true;
            doubleJump = false;
            anim_gatilhos.SetBool("asas", true);
         }
      }

      if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
         anim.SetBool("abaixou", true);
         abaixado = true;
      }

      if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
         anim.SetBool("abaixou", false);
         anim.SetBool("levantou", true);
         abaixado = false;
         StartCoroutine("levantar");
      }

      if(Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1)) {
			if(podePor) {
				SpawnPedra();
			}
		}
   }
 
   void move(){
      float horizontalForceButton = Input.GetAxis ("Horizontal");
      rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
      isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.185f, whatIsGround);
 
      if ((horizontalForceButton > 0  && !lookingRight) || (horizontalForceButton < 0 && lookingRight)) {
         Flip ();
      }

      if (jump) {
         GerenciadorAudio.inst.PlayPulo(som_pulo);
         rb2d.AddForce(new Vector2(0, jumpForce));
         anim.SetBool("pulou", true);
         jump = false;
      }

      if(podeAtirar) {
         if(Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)) {
				Fire();
			}
      }
   }
 
   void Flip(){
      lookingRight = !lookingRight;
      Vector3 myScale = transform.localScale;
      myScale.x *= -1;
      transform.localScale = myScale;
   }

   IEnumerator posQueda() {
      yield return new WaitForSeconds(0.2f);
      anim.SetBool("caiu", false);
      anim.SetBool("idle", true);
   }

    IEnumerator levantar() {
      yield return new WaitForSeconds(0.25f);
      anim.SetBool("levantou", false);
   }

   IEnumerator pararDeRir() {
      yield return new WaitForSeconds(2f);
      player_collider.isTrigger = false;
      rb2d.bodyType = RigidbodyType2D.Dynamic;
      anim.SetBool("rir", false);
      anim.SetBool("idle", true);
   }

   void Fire() {

      GerenciadorAudio.inst.PlayTiro(som_tiro);

		GameObject cloneBullet = Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

		if(!lookingRight)
			cloneBullet.transform.eulerAngles = new Vector3(0, 0, 180);
	}

   private void SpawnPedra() { // Spawn da plataforma
		Instantiate(plataforma, chaoSpawn.transform.position, plataforma.transform.rotation);
	}

   private void OnCollisionEnter2D(Collision2D other) {
      if(other.gameObject.CompareTag("monstro")) {
         Morrer();
      } else if(other.gameObject.CompareTag("moeda_rir")) {
         player_collider.isTrigger = true;
         rb2d.bodyType = RigidbodyType2D.Static;
         anim.SetBool("rir", true);
         StartCoroutine("pararDeRir");
      } else if(other.gameObject.CompareTag("tijolo")) {
         speed -= 1;
         dashSpeed -= 5;
         jumpForce -= 50;
         sr.color = Color.grey;
         StartCoroutine("tijolada");
      } else if(other.gameObject.CompareTag("plataforma")) {
      if(Input.GetKey(KeyCode.LeftShift)) {
            Destroy(other.gameObject);
         }
      }
   }

   private void OnCollisionStay2D(Collision2D other) {
   if(other.gameObject.CompareTag("plataforma")) {
      if(Input.GetKey(KeyCode.LeftShift)) {
            Destroy(other.gameObject);
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other) {
      if(other.gameObject.CompareTag("alerta_falas")) {
         rb2d.velocity = new Vector2(0, 0);
      }
   }

   IEnumerator tijolada() {
      yield return new WaitForSeconds(3f);
      dashSpeed += 5;
      jumpForce += 50;
      speed += 1;
      sr.color = Color.white;
   }

   IEnumerator petSumir() {
      yield return new WaitForSeconds(0.7f);
      pet_ativado = false;
      pet.SetActive(false);
      anim_pet.SetBool("sacrificar", false);
   }

   void Dash() {
      if(Input.GetKey(KeyCode.Space) && canDash) {
         if(dashAtual <= 0) {
            StopDash();
         } else {
            if(isGrounded) {
               CreateDust();
            }
            isDashing = true;
            dashAtual -= Time.deltaTime;
            if(lookingRight) {
               rb2d.velocity = Vector2.right * dashSpeed;
            } else {
               rb2d.velocity = Vector2.left * dashSpeed;
            }
         }
      }

      if(isGrounded) {
         isDashing = false;
         canDash = true;
         dashAtual = duracaoDash;
      } 
   }

   private void StopDash() {
      rb2d.velocity = Vector2.zero;
      dashAtual = duracaoDash;
      isDashing = false;
      canDash = false;
   }

   void CreateDust() {
      dust.Play();
   }

   void Morrer() {
      simbuloVoador.SetActive(false);
      if(!pet_ativado) {
         player_morto = true;
         GerenciadorAudio.inst.PlayMorte(som_morte);
         Camera.tremer = true;
         pode_mexer = false;
         podeAtirar = false;
         anim.SetBool("morreu", true);
         player_collider.isTrigger = true;
         rb2d.bodyType = RigidbodyType2D.Static;
         Barra_de_vida.fillAmount = 0;
         BarraVidaMaior.sprite = icon_morte;
         Destroy(valor_vida);
         StartCoroutine("morrerDeVez");
         } else {
            Camera.tremer = true;
            anim_pet.SetBool("sacrificar", true);
            StartCoroutine("petSumir");
         }
   }

   IEnumerator morrerDeVez() {
      yield return new WaitForSeconds(3.2f);
      Time.timeScale = 0;
      Destroy(this.gameObject, 3.2f);
   }

}