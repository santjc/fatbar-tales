using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
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

    public float bulletForce = 8f;



    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
    }   

    void Update(){
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
        
        
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        // speed = Mathf.Clamp(moveInput.magnitude,0.0f,10f);

        lookDir = mousePos - rb.position;
        lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Animate();

    }

    void Animate(){
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical",moveInput.y);
        //         animator.SetFloat("Horizontal", lookDir.x);
        // animator.SetFloat("Vertical",lookDir.y);
        animator.SetFloat("Speed",moveVelocity.magnitude);

        Debug.Log(speed);
    }

        void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(lookDir * bulletForce, ForceMode2D.Impulse);

    }
}
