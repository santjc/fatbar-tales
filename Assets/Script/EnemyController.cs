﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    private Transform target;
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer(){
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed * Time.deltaTime);
    }
}
