using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [Header("服务器连接")]
    public InputField inputFieldIp;
    public InputField inputFieldPort;
    public Button connectBtn;

    [Header("连接事件判断")]
    public List<string> ipandPostList;//已连接服务器列表
    bool isConnected = false;

    [Header("信息输入及发送")]
    public Toggle toggleAll;
    public Toggle toggleAlone;
    public Text iPAndPortText;

    public InputField iPAndPortInputField;
    string iPAndPortName;

    public InputField inputFieldMsg;
    string ipAndPort;

    [Header("消息列表")]
    public MsgItem msgItemPrefab;
    public RectTransform content;
    public Queue<string> msgQueue = new Queue<string>();

    [Header("当前客户端")]
    Socket socketClient;
    byte[] bytes;


    /// <summary>
    /// 连接服务器
    /// </summary>
    private void ConnectionAsync()
    {
        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        string ip=inputFieldIp.text;
        int port = int.Parse(inputFieldPort.text);
        socketClient.BeginConnect(ip, port, ConnectCallback, socketClient);
    }

    /// <summary>
    /// 连接回调函数
    /// </summary>
    /// <param name="ar"></param>
    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {

            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);

           ipAndPort= FormatAddress(socket);

            Debug.Log("异步连接成功");
            isConnected = true; 

            bytes = new byte[1024];
            //等待接收信息
            socket.BeginReceive(bytes, 0, 1024, 0, ReceiveCallBack, socket);
        }
        catch (SocketException se)
        {
            Debug.Log(se.Message);
        }
    }
/// <summary>
/// 接收回调函数
/// </summary>
/// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            Socket socketClient = (Socket)ar.AsyncState;
            int count = socketClient.EndReceive(ar);

            if (count <= 0)
            {
                socketClient.Close();
                Debug.Log("服务器关闭连接");
                return;
            }

            string str = Encoding.UTF8.GetString(bytes, 0, count);
            Debug.LogError(FormatAddress(socketClient)+"收到的异步信息是：" + str);
            msgQueue.Enqueue(str);

            //继续等待接受信息
            socketClient.BeginReceive(bytes, 0, 1024, 0, ReceiveCallBack, socketClient);
        }
        catch (SocketException se)
        {
            Debug.Log(se.Message);
        }

    }
    /// <summary>
    /// 异步发送信息
    /// </summary>
    /// <param name="value"></param>
    private void SendAsync(string value)
    {
        if(toggleAll.isOn)
        {
            value = "0|" + value;
        }
        else if(toggleAlone.isOn)
        {
            value = iPAndPortName + "|" + value;
        }
        byte[] sendBytes = Encoding.UTF8.GetBytes(value);
        socketClient.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socketClient);
    }
    /// <summary>
    /// 发送回调函数
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndSend(ar);
            Debug.Log("信息发送成功，长度为：" + count);
        }
        catch (SocketException se)
        {

            Debug.LogError(se.Message);
        }

    }
    /// <summary>
    /// 地址格式化
    /// </summary>
    /// <param name="socket"></param>
    /// <returns></returns>
    private string FormatAddress(Socket socket)
    {
        string clientIP = ((IPEndPoint)socket.LocalEndPoint).Address.ToString();
        string clientPort = ((IPEndPoint)socket.LocalEndPoint).Port.ToString();

        return string.Format("{0}:{1}", clientIP, clientPort);

    }

    private void ShowMessage(string ip,int port,string msg)
    {
        MsgItem msgItem = Instantiate(msgItemPrefab,content);//实例化的同时指定父物体，避免默认实例化为根目录后再指定导致scale改变
        msgItem.SetMessage(ip, port,msg);
    }

    #region UI回调
    public void OnConnectButtonCliked()
    {
        ConnectionAsync();
    }

    public void OnSendButtonClicked()
    {
        string value = inputFieldMsg.text;
        SendAsync(value);
    }

    public void OnIpOrPortChanged()
    {
        connectBtn.GetComponent<Button>().interactable = true;
    }
    public void OnIpOrPortEndEdit()
    {
        for (int i = 0; i < ipandPostList.Count; i++)
        {
            if (ipandPostList[i] == inputFieldIp.text + "|" + inputFieldPort.text)
            {
                connectBtn.GetComponent<Button>().interactable = false;
            }
        }
    }
    #endregion

    #region 生命周期函数
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(iPAndPortInputField.gameObject.activeSelf)
        {
            iPAndPortName = iPAndPortInputField.text;
        }
        if(msgQueue.Count > 0)
        { 
            string str=msgQueue.Dequeue();
            string[] strs=str.Split(':','|');
            ShowMessage(strs[0], Convert.ToInt32( strs[1]), strs[2]);
        }

        if (isConnected)
        {
            isConnected = false;
            ipandPostList.Add(inputFieldIp.text + "|" + inputFieldPort.text);
            connectBtn.GetComponent<Button>().interactable = false;
        }
        if(toggleAll.isOn)
        {
            iPAndPortInputField.gameObject.SetActive(false);
            iPAndPortText.text = string.Format("当前IP:{0}", ipAndPort);
        }
        else if(toggleAlone.isOn)
        {
            iPAndPortInputField.gameObject.SetActive(true);
            iPAndPortText.text = string.Format("发送给：");
        }

    }
    #endregion

}
