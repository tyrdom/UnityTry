using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MsgScheme;
using Google.Protobuf;


public class Click : MonoBehaviour
{
    public void AClick()
    {
        Debug.Log("11111");
//        GameObject socketP = GameObject.Find("WebSocketPipe");
        GameObject input = GameObject.Find("InputField");
        string text = input.GetComponent<InputField>().text;
        TestRequest tq = new TestRequest() {TestText = text};
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.TestRequest, TestRequest = tq};
        byte[] byte2Send = aMsg.ToByteArray();
        Debug.Log("sendMsg:" + text);
        WebSocketLoc.WebSocketUse.Send(byte2Send);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(AClick);
    }

    // Update is called once per frame
    void Update()
    {
    }
}