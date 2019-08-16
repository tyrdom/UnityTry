using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Text;
using System;
using System.Diagnostics.Tracing;
using MsgScheme;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;


static class WebSocketLoc
{
    private static string url = "ws://localhost:8080/";
    public static readonly WebSocket WebSocketUse = new WebSocket(new Uri(url));
}

public class WebSocketPipe : MonoBehaviour
{
    public delegate void MsgEventDelegate<T>(T msgBody);

    public static event MsgEventDelegate<TestResponse> TestResponse;
    public static event MsgEventDelegate<LoginResponse> LoginResponse;
    public static event MsgEventDelegate<CreateAccountResponse> CreateAccountResponse;
    public static event MsgEventDelegate<ErrorResponse> ErrorResponse;
    public static event MsgEventDelegate<JoinRoomResponse> JoinRoomResponse;
    public static event MsgEventDelegate<CreateRoomResponse> CreateRoomResponse;
    public static event MsgEventDelegate<GetReadyResponse> GetReadyResponse;
    public static event MsgEventDelegate<RoomPlayerStatusBroadcast> RoomPlayerStatusBroadcast;


//    private AMsg theMsg;


    // Start is called before the first frame update
    void Start()
    {
        WebSocketLoc.WebSocketUse.Connect().MoveNext();
        Debug.Log("连接打开");
    }

    // Update is called once per frame
    void Update()
    {
        byte[] someByte = WebSocketLoc.WebSocketUse.Recv();
        if (someByte.Length > 0)
        {
            //  IMessage im = new AMsg();
            AMsg aMsg = AMsg.Parser.ParseFrom(someByte);

            switch (aMsg.Head)
            {
                case AMsg.Types.Head.TestResponse:
                    TestResponse(aMsg.TestResponse);
                    break;
                case AMsg.Types.Head.CreateAccountResponse:
                    CreateAccountResponse(aMsg.CreateAccountResponse);
                    break;
                case AMsg.Types.Head.LoginResponse:
                    LoginResponse(aMsg.LoginResponse);
                    break;
                case AMsg.Types.Head.ErrorResponse:
                    ErrorResponse(aMsg.ErrorResponse);
                    break;
                case AMsg.Types.Head.CreateRoomResponse:
                    CreateRoomResponse(aMsg.CreateRoomResponse);
                    break;
                case AMsg.Types.Head.JoinRoomResponse:
                    JoinRoomResponse(aMsg.JoinRoomResponse);
                    break;
                case AMsg.Types.Head.RoomPlayerStatusBroadcast:
                    RoomPlayerStatusBroadcast(aMsg.RoomPlayerStatusBroadcast);
                    break;
                case AMsg.Types.Head.GetReadyResponse:
                    GetReadyResponse(aMsg.GetReadyResponse);
                    break;
                default:
                    Debug.Log("unknown message!!!!");
                    break;
            }
        }
    }

    void OnDestroy()
    {
        Debug.Log("关闭连接");
        WebSocketLoc.WebSocketUse.Close();
    }
}