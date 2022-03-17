using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class Form1 : Form
    {
        class ClientInfo
        {
            public Socket socket;
            public byte[] bytes=new byte[1024];
        }

        SynchronizationContext synchronizationContext;


        List<ClientInfo> socketClients = new List<ClientInfo>();


        public Form1()
        {
            synchronizationContext = SynchronizationContext.Current;
            InitializeComponent();
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            InitServer();
        }

        private void InitServer()
        {

            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //服务端需要绑定监听的IP和端口号
            string ip=textBoxIP.Text;
            int port = int.Parse(textBoxPort.Text);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

            socketServer.Bind(iPEndPoint);
            socketServer.Listen(0);
            Console.WriteLine("服务器异步启动成功");

            socketServer.BeginAccept(AcceptCallback, socketServer);

        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket socketServer = (Socket)ar.AsyncState;
                Socket socketClient = socketServer.EndAccept(ar);

                ClientInfo clientInfo = new ClientInfo();
                clientInfo.socket = socketClient;
                socketClients.Add(clientInfo);

                //socketClients.Add(socketClient);

                //显示具体信息到listboxuser
                synchronizationContext.Post(ShowClientInfo, clientInfo);
                Console.WriteLine("客户端异步连接服务器成功");

                //等待接收信息
                clientInfo.socket.BeginReceive(clientInfo.bytes, 0, 1024, 0, ReceiveCallBack, clientInfo);

                //继续等待接受客户端连接
                socketServer.BeginAccept(AcceptCallback, socketServer);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
                throw;
            }
            
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                ClientInfo clientInfo = (ClientInfo)ar.AsyncState;
                int count = clientInfo.socket.EndReceive(ar);

                if (count <= 0)
                {
                    //线程安全，对象的调用，变量的值
                    string ip = FormatAddress(clientInfo.socket);

                    //删除list里面的用户信息
                    socketClients.Remove(clientInfo);
                    //通知其他客户端某客户端下线
                    //SendAsync(clientInfo.socket, ip + "已下线");
                    //关闭该连接
                    clientInfo.socket.Close();
                    //删除界面上的UI
                    synchronizationContext.Post(RemoveClientInfo,ip);
                    Console.WriteLine("客户端关闭连接");
                    return;
                }

                //对接收到的socket信息进行封装
                string str = Encoding.UTF8.GetString(clientInfo.bytes, 0, count);
                string[] strs = str.Split('|');
                string sendStr = FormatAddress(clientInfo.socket) +"|" +strs[1];
                Console.WriteLine("服务端收到的异步信息是：" + sendStr);

                //遍历客户端并发送
                for (int i = 0; i < socketClients.Count; i++)
                {
                    if(strs[0]=="0")
                        SendAsync(socketClients[i].socket, sendStr);
                    else
                    {
                        if(FormatAddress(socketClients[i].socket) ==strs[0])
                        {
                            SendAsync(socketClients[i].socket,sendStr);
                        }
                    }
                }

                //继续等待接收信息
                clientInfo.socket.BeginReceive(clientInfo.bytes, 0, 1024, 0, ReceiveCallBack, clientInfo);

            }
            catch (SocketException se)
            {

                Console.WriteLine(se.Message);
            }
            catch(ArgumentOutOfRangeException aore)
            {
                Console.WriteLine(aore.Message);
            }

        }

        private void SendAsync(Socket socketClient, string value)
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
                Console.WriteLine("信息发送成功，长度为：" + count);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }

        }

        #region 子线程调用主线程UI的回调函数
        //在服务端主线程UI显示具体信息到listboxuser
        private void ShowClientInfo(object obj)
        {
            ClientInfo clientInfo = (ClientInfo)obj;
            string ip = FormatAddress(clientInfo.socket);
            listBoxUsers.Items.Add(ip);
        }

        //移除服务端主线程UI对应listboxuser的项
        private void RemoveClientInfo(object obj)
        {
            string ip = (string)obj;
            listBoxUsers.Items.Remove(ip);
        }
        #endregion

        //Socket地址格式化
        private string FormatAddress(Socket socket)
        {
            string clientIP = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            string clientPort = ((IPEndPoint)socket.RemoteEndPoint).Port.ToString();

            return string.Format("{0}:{1}", clientIP, clientPort);
           
        }

    }
}
