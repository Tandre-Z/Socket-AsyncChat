using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgItem : MonoBehaviour
{
    public Text ipText;
    public Text portText;
    public Text msgText;

    public void SetMessage(string ip,int port,string msg)
    {
        ipText.text = ip;
        portText.text = port.ToString();
        msgText.text = msg;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
