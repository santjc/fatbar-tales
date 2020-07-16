using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    public static PlayerController instance;
    public float speed = 5f;
    public float hp = 100f;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    public Animator animator;

    public Camera cam;
    Vector2 mousePos;

    Vector2 lookDir;
    float lookAngle;
    Vector2 moveInput;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject clonePrefab;

    public float bulletForce = 5f;

    private void Awake () {
        instance = this;
    }

    void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.gravityScale = 0.0f;
    }

    void Update () {
        moveInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        moveInput.Normalize ();
        moveVelocity = moveInput * speed;

        mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

        if (Input.GetButtonDown ("Fire1")) {
            Shoot ();
        }

        if(Input.GetButtonDown("Jump")){
            MakeClone();
        }

        if(hp <= 80){
            Destroy(gameObject);
        }

    }

    void FixedUpdate () {
        rb.MovePosition (rb.position + moveVelocity * Time.fixedDeltaTime);
        // speed = Mathf.Clamp(moveInput.magnitude,0.0f,10f);
        lookDir = mousePos - rb.position;
        lookAngle = Mathf.Atan2 (lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Animate ();
    }

    void Animate () {
        animator.SetFloat ("Horizontal", moveInput.x);
        animator.SetFloat ("Vertical", moveInput.y);
        animator.SetFloat ("Speed", moveVelocity.magnitude);

    }

    void Shoot () {
        GameObject bullet = Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> ();
        rb.AddForce (lookDir * bulletForce, ForceMode2D.Impulse);
    }

    void MakeClone () {

        float timer = 0;
        timer += Time.deltaTime;
        GameObject clone = Instantiate (clonePrefab, transform.position , transform.rotation);
        Destroy(clone, 7);
    }

    public IEnumerator Knockback (float knockDuration, float knockPower, Transform obj) {
        float timer = 0;
        while (knockDuration > timer) {
            timer += Time.deltaTime;
            Vector2 dir = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce (-dir * knockPower);
        }
        yield return 0;
    }

    public IEnumerator damageHp(float dmg){
        hp = hp - dmg;
        yield return 0;
    }

}