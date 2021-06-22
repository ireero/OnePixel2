using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstroBomba : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D corpo;
    private BoxCollider2D collider;
    private Transform target;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
      speed = 2.5f;
      target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      anim = GetComponent<Animator>();
      corpo = GetComponent<Rigidbody2D>();
      collider = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("bullet")) {
            speed = 0;
            anim.SetBool("morreu", true);
            collider.isTrigger = true;
            StartCoroutine("morre");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            speed = 0;
            anim.SetBool("explode", true);
            StartCoroutine("morreExplode");
        }
    }

    IEnumerator morre() {
        yield return new WaitForSeconds(2.2f);
        Destroy(this.gameObject);
    }

    IEnumerator morreExplode() {
        yield return new WaitForSeconds(1.6f);
        Destroy(this.gameObject);
    }
}
