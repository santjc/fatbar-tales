using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{


    public static RoomController instance;
    string currentWorldName = "Base";
    RoomInfo currentLoadRoomData;

    Queue<Room> loadRoomQueue = new Queue<Room>();
    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update

    public bool existRoom (int x, int y){
        return loadedRooms.Find( item => item.X == x && item.Y == y) != null;
    }
}
