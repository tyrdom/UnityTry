using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using MsgScheme;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public static Dictionary<int, Tuple<String, StatusInRoom>>
        PlayersInRooms;

    public static int MyTempId = -1;
    public static event WebSocketPipe.MsgEventDelegate<bool> RoomStatusReach;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketPipe.RoomPlayerStatusBroadcast += RoomPlayerStatusShow;
    }

    void RoomPlayerStatusShow(RoomPlayerStatusBroadcast roomPlayerStatusBroadcast)
    {
        PlayersInRooms =
            roomPlayerStatusBroadcast.PlayerStatusList.ToDictionary(x => x.TempId,
                x => Tuple.Create(x.NickName, x.Status));
        Debug.Log(PlayersInRooms);
        MyTempId = roomPlayerStatusBroadcast.YourTempId;
        RoomStatusReach(true);
    }


    // Update is called once per frame
    void Update()
    {
    }
}