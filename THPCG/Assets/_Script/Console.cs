using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using MsgScheme;
using Google.Protobuf;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebSocketPipe.TestResponse += ShowText;
        WebSocketPipe.ErrorResponse += ShowError;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ShowText(TestResponse testResponse)
    {
        Debug.Log("eventBus: " + testResponse.TestText);
        this.GetComponent<Text>().text = "eventBus: " + testResponse.TestText;
    }

    void ShowError(ErrorResponse errorResponse)
    {
        string errorResponseReason = errorResponse.Reason;
        Debug.Log("error:" + errorResponseReason);
        this.GetComponent<Text>().text = errorResponseReason;
        switch (errorResponse.ErrorType)
        {
            case ErrorResponse.Types.ErrorType.OtherLogin:
                SceneManager.LoadScene("login");
                break;
            
            default:
                Debug.Log("errorDefError");
                break;
        }
    }

    private void OnDestroy()
    {
        WebSocketPipe.TestResponse -= ShowText;
        WebSocketPipe.ErrorResponse -= ShowError;
    }
}