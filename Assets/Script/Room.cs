﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X;
    public int Y;
    // Start is called before the first frame update
    void Start()
    {

        transform.position = Vector3.zero;
     if(RoomController.instance == null){
         return;
     }
        
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(Width,Height,0));
    }

    public Vector3 GetRoomCenter(){
        return new Vector3 (X * Width, Y*Height);
    }
}
