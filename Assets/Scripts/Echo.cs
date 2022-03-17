using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class Echo : MonoBehaviour
{

    public InputField inputField;

    Socket socketClient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        Connnection();
    }
    
    public void OnSendButtonClick()
    {
        string value = inputField.text;
        Send(value);
    }

    private void Connnection()
    {
        //创建socket连接
        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Socket对象连接
        //loclhost\127.0.0.1    
        socketClient.Connect("127.0.0.1",8888);//（IP，Port）
    }

    private void Send(string msg)
    {
        Debug.Log("发送了：" + msg);
        //将信息字符串转换为Byte数组
        byte[] bytes = Encoding.UTF8.GetBytes(msg);

        socketClient.Send(bytes);

        //接收服务器端返回数据
        byte[] readBytes = new byte[1024];
        int count = socketClient.Receive(readBytes);
        string readStr=Encoding.UTF8.GetString(readBytes,0,count);
        Debug.Log("服务器返回了：" + readStr);
    }


}
