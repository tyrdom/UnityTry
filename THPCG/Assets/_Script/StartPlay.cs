using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Google.Protobuf;
using MsgScheme;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Play);
        WebSocketPipe.CreateRoomResponse += CreateRes;
        WebSocketPipe.JoinRoomResponse += JoinRes;
        WebSocketPipe.ErrorResponse += WhenCreate;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GoRoomScene(int roomId)
    {
        Saves.roomId = roomId;
        GameObject find = GameObject.Find("HintCanvas");
        DontDestroyOnLoad(find);
        SceneManager.LoadScene("room");
    }

    void CreateRes(CreateRoomResponse createRoomResponse)
    {
        int roomId = createRoomResponse.RoomId;
        Debug.Log("createRoom OK:" + roomId);
        
        GoRoomScene(roomId);
    }

    void CreateRoom()
    {
        CreateRoomRequest createRoomRequest = new CreateRoomRequest();
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.CreateRoomRequest, CreateRoomRequest = createRoomRequest};
        byte[] byteArray = aMsg.ToByteArray();
        WebSocketLoc.WebSocketUse.Send(byteArray);
    }

    void WhenCreate(ErrorResponse errorResponse)
    {
        if (errorResponse.ErrorType == ErrorResponse.Types.ErrorType.NoRoomToJoin)
        {
            CreateRoom();
        }
        else
        {
            Debug.Log("Sth No Good");
        }
    }

    void Play()
    {
        JoinRoomRequest joinRoomRequest = new JoinRoomRequest() {CertainRoom = false, RoomId = -1};
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.JoinRoomRequest, JoinRoomRequest = joinRoomRequest};
        byte[] byteArray = aMsg.ToByteArray();
        WebSocketLoc.WebSocketUse.Send(byteArray);
    }

    void JoinRes(JoinRoomResponse joinRoomResponse)
    {
        int roomId = joinRoomResponse.RoomId;
        Debug.Log("进入房间：" + roomId.ToString());
        GoRoomScene(roomId);
    }
}