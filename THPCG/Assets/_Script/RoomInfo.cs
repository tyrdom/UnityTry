using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string roomId = "Room" + Saves.roomId;
        this.GetComponent<Text>().text = roomId;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}