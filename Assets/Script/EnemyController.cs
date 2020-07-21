using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Search,
    Follow,
    Die,
    Attack
}

public enum EnemyType{
    Ranged,
    Melee
};
public class EnemyController : MonoBehaviour {

    GameObject player;
    public EnemyState currentState = EnemyState.Search;
    public EnemyType enemyType;
    public float range = 5f;
    public float attackRange;
    public float speed = 2f;
    public GameObject bulletPrefab;

    private bool chooseDir = false;
    private bool dead = false;
    private bool cdAttack = false;
    public float coolDown;
    private Vector3 randomDir;

    void Start () {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    void Update () {
        switch (currentState) {
            // case (EnemyState.Search):
            //     Search ();
            //     break;
            // case (EnemyState.Follow):
            //     Follow ();
            //     break;
            case (EnemyState.Die):

                break;

            case (EnemyState.Attack):
                Attack ();
                break;
        }

        if (isInRange (range) && currentState != EnemyState.Die) {
            currentState = EnemyState.Follow;
        } else if (!isInRange (range) && currentState != EnemyState.Die) {
            currentState = EnemyState.Search;
        }

        if (Vector3.Distance (transform.position, player.transform.position) <= attackRange) {
            currentState = EnemyState.Attack;
        }
    }

    private IEnumerator ChooseDirection () {
        chooseDir = true;
        yield return new WaitForSeconds (Random.Range (0f, 5f));
        randomDir = new Vector3 (0, 0, Random.Range (0, 360));
        Quaternion nextRotation = Quaternion.Euler (randomDir);
        transform.rotation = Quaternion.Lerp (transform.rotation, nextRotation, Random.Range (1f, 2f));
        chooseDir = false;
    }

    private bool isInRange (float range) {
        return Vector3.Distance (transform.position, player.transform.position) <= range;
    }
    void Search () {
        if (!chooseDir) {
            StartCoroutine (ChooseDirection ());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (isInRange (range)) {
            currentState = EnemyState.Follow;
        }

    }

    void Follow () {
        transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack () {
        if (!cdAttack) {
            switch (enemyType) {
                case (EnemyType.Melee):
                    GameController.DamagePlayer (1);
                    StartCoroutine (CoolDown ());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate (bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet> ().GetPlayer (player.transform);
                    bullet.AddComponent<Rigidbody2D> ().gravityScale = 0;
                    bullet.GetComponent<Bullet> ().isEnemyBullet = true;
                    StartCoroutine (CoolDown ());
                    break;
            }
        }
    }

    public void Death () {
        Destroy (gameObject);
    }

    private IEnumerator CoolDown () {
        cdAttack = true;
        yield return new WaitForSeconds (coolDown);
        cdAttack = false;
    }

}