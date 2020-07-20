using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeTime = 1.5f;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(DeathDelay());

        if(!isEnemyBullet){
            transform.localScale = new Vector2(GameController.bulletSize, GameController.bulletSize);
        }
    }

    void Update(){
        if(isEnemyBullet){
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position,playerPos, 5f * Time.deltaTime);
            if(curPos == lastPos){
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" && !isEnemyBullet){
            other.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if(other.tag == "Player" && isEnemyBullet){
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
