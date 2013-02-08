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
    private Vector2 _packagesCounter = new Vector2(0,0);
    private MapleParser _parser = new MapleParser();

    void Start()
    {
        //
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width - 205, Screen.height - 105, 200, 100));
        GUI.Box(new Rect(0, 0, 200, 100), "Web Connector");
        GUI.Label(new Rect(10, 65, 70, 23), "Send/Received packs:");
        GUI.Label(new Rect(80, 65, 120, 23), _packagesCounter.x.ToString(CultureInfo.InvariantCulture) + "/" + _packagesCounter.y.ToString(CultureInfo.InvariantCulture));
        GUI.EndGroup();

        if (message.Length > 0)
            GUI.Label(new Rect(150, 150, 400, 500), message);

        if (message.Length > 500)
            message = "Message: ";
    }
    
    private String message = "Message: ";

    /*public string Calculate(string expression)
    {
        ExternallCall(expression);
        int wait = 0;
        /*while (true)
        {
            if (_mapleResponse.Length > 0)
            {
                string temp = _mapleResponse;
                _mapleResponse = "";
                return temp;
            }
            if (wait == 60)
            {
                break;
            }
            Thread.Sleep(50);
            wait++;
        }#1#
        return "Maple not answer during 3 second...";
    }*/

    public void ExternallCall(string data)
    {
        _packagesCounter.x++;
        message += "\nSend: " + data;
        if (data.Length > 0)
            Application.ExternalCall("MapleCalculate", data);
    }

//    private string _mapleResponse = "";
    public void UnityCall(string data)
    {
        _packagesCounter.y++;
        _parser.Process(data, PhysObjectsManager.PhysObjects);
        message += "\nReceiv: " + data;
//        _mapleResponse = data;
    }
}
