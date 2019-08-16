using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using MsgScheme;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour
{
    private bool isReady = false;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketPipe.GetReadyResponse += ReadyShow;
        this.GetComponent<Button>().onClick.AddListener(AClick);
    }

    void ReadyShow(GetReadyResponse getReadyResponse)
    {
        StatusInRoom statusInRoom = getReadyResponse.YourStatus;
        switch (statusInRoom)
        {
            case StatusInRoom.Ready:
                isReady = true;
                GetComponentInChildren<Text>().text = "OK";
                break;
            case StatusInRoom.Standby:
                isReady = false;
                GetComponentInChildren<Text>().text = "Ready";
                break;
            case StatusInRoom.Gaming:
                this.GetComponent<CanvasGroup>().alpha = 0;
                break;
            case StatusInRoom.Error:
                break;
            
        }
    }

    private void AClick()

    {
        GetReadyRequest getReadyRequest = new GetReadyRequest() {IsReady = !isReady};
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.GetReadyRequest, GetReadyRequest = getReadyRequest};
        WebSocketLoc.WebSocketUse.Send(aMsg.ToByteArray());
    }

    // Update is called once per frame
    void Update()
    {
    }
}