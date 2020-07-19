using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Search,
    Follow,
    Die
}
public class EnemyController : MonoBehaviour {

    GameObject player;
    public EnemyState currentState = EnemyState.Search;
    public float range = 5f;
    public float speed = 2f;

    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomDir;

    void Start () {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    void Update () {
        switch (currentState) {
            case (EnemyState.Search):
                Search ();
                break;
            case (EnemyState.Follow):
                Follow ();
                break;
            case (EnemyState.Die):

                break;
        }

        if (isInRange (range) && currentState != EnemyState.Die) {
            currentState = EnemyState.Follow;
        } else if (!isInRange (range) && currentState != EnemyState.Die) {
            currentState = EnemyState.Search;
        }
    }

    private IEnumerator ChooseDirection () {
        chooseDir = true;
        yield return new WaitForSeconds (Random.Range (2f, 8f));
        randomDir = new Vector3 (0, 0, Random.Range (0, 360));
        Quaternion nextRotation = Quaternion.Euler (randomDir);
        transform.rotation = Quaternion.Slerp (transform.rotation, nextRotation, Random.Range (0f, 1f));
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

    public void Death(){
        Destroy(gameObject);
    }

}