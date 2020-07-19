using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeTime = 1.5f;
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(DeathDelay());
    }

    void Update(){

    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
    }
}
