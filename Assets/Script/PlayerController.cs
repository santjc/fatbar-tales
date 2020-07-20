using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    public static PlayerController instance;
    public float speed = 5f;

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
    private float lastFire;
    public float fireDelay = 0.3f;

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

        float shootHor = Input.GetAxis ("ShotH");
        float shootVert = Input.GetAxis ("ShotV");

        Debug.Log(shootHor + " /// " + shootVert);
        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay) {
            Shoot (shootHor, shootVert);
            lastFire = Time.time;
        }

        if (Input.GetButtonDown ("Jump")) {
            MakeClone ();
        }


    }

    void FixedUpdate () {
        rb.MovePosition (rb.position + moveVelocity * Time.fixedDeltaTime);
        Animate ();
    }

    void Animate () {
        animator.SetFloat ("Horizontal", moveInput.x);
        animator.SetFloat ("Vertical", moveInput.y);
        animator.SetFloat ("Speed", moveVelocity.magnitude);

    }

    void Shoot (float x, float y) {
        GameObject bullet = Instantiate (bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D> ().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D> ().velocity = new Vector3 (
            (x < 0) ? Mathf.Floor (x) * bulletForce : Mathf.Ceil (x) * bulletForce,
            (y < 0) ? Mathf.Floor (y) * bulletForce : Mathf.Ceil (y) * bulletForce, 0
        );
    }

    void MakeClone () {
        float timer = 0;
        timer += Time.deltaTime;
        GameObject clone = Instantiate (clonePrefab, transform.position, transform.rotation) as GameObject;
        Destroy (clone, 5);
    }


}