using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class EchoAsync : MonoBehaviour
{

    public InputField inputField;
    public Text text;

    Socket socketClient;
    //信息缓存\信息队列\信息堆栈
    string msg;
    string inputFieldText;

    /// <summary>
    /// 
    /// </summary>
    private void ConnectionAsync()
    {
        socketClient = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

        socketClient.BeginConnect("127.0.0.1", 8888,ConnectCallback, socketClient);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ar"></param>
    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("异步连接成功");
            msg = "异步连接成功";
            //UI不能跨线程调用,很多unity自带api都不能够被调用
            //text.text = "异步连接成功";
        }
        catch (SocketException se)
        {
            Debug.Log(se.Message);
        }
    }

    private void SendAsync(string value)
    {
        byte[] sendBytes = Encoding.UTF8.GetBytes(value);
        socketClient.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socketClient);
    }

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

    public void OnConnectCliked()
    {
        ConnectionAsync();
    }

    public void OnSendClicked()
    {
        string value = inputField.text;
        SendAsync(value);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = msg;
    }


}
