using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float knockPower = 10f;
    public float knockDuration = 5f;
    public float enemyDmg = 2f;
    public float enemyHp = 100f;
    private Transform target;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float range;
    private bool isColliding = false;
    // Start is called before the first frame update
    void Start () {
        target = FindObjectOfType<PlayerController> ().transform;
    }

    // Update is called once per frame
    void Update () {
        if (!isColliding && target != null) {
            FollowPlayer ();
        }

        if (enemyHp <= 0) {
            Destroy (gameObject);
        }

    }

    public void FollowPlayer () {
        transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Player") {
            isColliding = true;
            StartCoroutine (PlayerController.instance.Knockback (knockDuration, knockPower, this.transform));
            StartCoroutine (PlayerController.instance.damageHp (enemyDmg));
        }

        if (other.gameObject.tag == "Ammo") {
            enemyHp -= 15;
        }
    }

    private void OnCollisionExit2D (Collision2D other) {
        isColliding = false;
    }

}