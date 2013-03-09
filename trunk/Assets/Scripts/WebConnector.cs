using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class WebConnector : MonoBehaviour
{
    public void JsExternalCall(string data)
    {
        //((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("Send: " + data);
        if (data.Length > 0)
            Application.ExternalCall("MapleCalculate", data);
    }

    public void UnityCall(string data)
    {
        //((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("Receiv: " + data);
        ((LabPlayer)FindObjectOfType(typeof(LabPlayer))).SetResponse(data);
    }
}
