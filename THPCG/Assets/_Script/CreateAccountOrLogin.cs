using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using MsgScheme;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAccountOrLogin : MonoBehaviour
{
    private string accountId;

    private string password;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketPipe.LoginResponse += LoginResponseDo;
        WebSocketPipe.CreateAccountResponse += CreateAccountResponseDo;
        this.GetComponent<Button>().onClick.AddListener(AccountClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoginResponseDo(LoginResponse loginResponse)
    {
        switch (loginResponse.Reason)
        {
            case LoginResponse.Types.Reason.Ok:
                Debug.Log("登陆成功,进入大厅场景");
                Saves.myNickname = loginResponse.Nickname;
                GameObject find = GameObject.Find("HintCanvas");
                DontDestroyOnLoad(find);
                SceneManager.LoadScene("hall");
                break;
            case LoginResponse.Types.Reason.WrongPassword:
                Debug.Log("密码错误");
                break;
            case LoginResponse.Types.Reason.Other:
                Debug.Log("未知异常");
                break;
            default:
                Debug.Log("未知异常");
                break;
        }
    }


    void SendLogin()
    {
        LoginRequest lr = new LoginRequest() {AccountId = accountId, Password = password};
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.LoginRequest, LoginRequest = lr};
        byte[] byteArray = aMsg.ToByteArray();
        WebSocketLoc.WebSocketUse.Send(byteArray);
    }

    void CreateAccountResponseDo(CreateAccountResponse createAccountResponse)

    {
        switch (createAccountResponse.Reason)
        {
            case CreateAccountResponse.Types.Reason.Ok:
                Debug.Log("创建成功，使用此账号登陆");
                SendLogin();
                break;
            case CreateAccountResponse.Types.Reason.AlreadyExist:
                Debug.Log("此账号已存在，使用账号密码登陆");
                SendLogin();
                break;
            case CreateAccountResponse.Types.Reason.NoGoodPassword:
                Debug.Log("密码不符合格式");
                break;
            case CreateAccountResponse.Types.Reason.Other:
                Debug.Log("未知错误");
                break;
            default:
                Debug.Log("未知错误");
                break;
        }
    }

    void AccountClick()
    {
        GameObject accountIdInput = GameObject.Find("AccountId");
        this.accountId = accountIdInput.GetComponent<InputField>().text;
        GameObject passwordInput = GameObject.Find("Password");
        this.password = passwordInput.GetComponent<InputField>().text;
        CreateAccountRequest ca = new CreateAccountRequest()
            {AccountId = accountId, Password = password, Phone = 0, WeChat = ""};
        AMsg aMsg = new AMsg() {Head = AMsg.Types.Head.CreateAccountRequest, CreateAccountRequest = ca};
        byte[] byteArray = aMsg.ToByteArray();
        WebSocketLoc.WebSocketUse.Send(byteArray);
    }
}