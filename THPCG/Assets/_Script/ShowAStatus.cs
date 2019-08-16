using System;
using System.Collections;
using System.Collections.Generic;
using MsgScheme;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShowAStatus : MonoBehaviour
{
    private int tempid;

    void getTempid()
    {
        int myTempId = PlayerStatus.MyTempId;
        switch (this.name)
        {
            case "this":
                tempid = myTempId;
                break;
            case "p1":
                tempid = (myTempId + 2) % GameConfig.MaxPlayer;
                break;
            case "p2":
                tempid = (myTempId + 1) % GameConfig.MaxPlayer;
                break;
            case "p3":
                tempid = (myTempId + 3) % GameConfig.MaxPlayer;
                break;
            default:
                tempid = -1;
                break;
        }
    }

    void RefreshShow(bool ok)
    {
        getTempid();
        if (PlayerStatus.PlayersInRooms.TryGetValue(tempid, out var onePlayerInRoom))
            this.GetComponent<Text>().text = onePlayerInRoom.Item1 + ":" + onePlayerInRoom.Item2.ToString();
        else
        {
            this.GetComponent<Text>().text = this.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus.RoomStatusReach += RefreshShow;
        if (this.name == "this")
        {
            this.GetComponent<Text>().text = Saves.myNickname;
        }
        else
            this.GetComponent<Text>().text = this.name;

        Debug.Log("set nickname ok!!!!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}