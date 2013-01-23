using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private WebConnector _webConnector;
    private bool _isPlay = false;

    // Use this for initialization
    void Start()
    {
        //_webConnector = (WebConnector) FindObjectOfType(typeof (WebConnector));
        StartMapleEngine();
    }

    void Update()
    {
//        try
//        {
            if (_isPlay)
                foreach (UserScript script in FindObjectsOfType(typeof(UserScript)).OfType<UserScript>().ToList())
                {
                    IntPtr val = MapleEngine.EvalMapleStatement(kv, Encoding.UTF8.GetBytes(script.Code));
                    if (MapleEngine.IsMapleStop(kv, val).ToInt32() != 0)
                    {
                        MapleEngine.StopMaple(kv);
                        Debug.Log("Stop maple");
                    }
                }

            // check if user typed quit/done/stop
            
//        }
//        catch (Exception exception)
//        {
//            Debug.LogError(exception.Message);
//            //message += "Err: " + exception.Message + "\n";
//        }
    }

    private static string message = "Message: \n";

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height - 105, 300, 100));
        GUI.Box(new Rect(0, 0, 300, 100), "Lab player");

        if (GUI.Button(new Rect(10, 25, 80, 24), "Pause"))
            _isPlay = false;

        if (GUI.Button(new Rect(100, 25, 100, 24), "Play"))
        {
            List<PhysObject> physObjects = FindObjectsOfType(typeof (PhysObject)).OfType<PhysObject>().ToList();
            foreach (PhysObject physObject in physObjects)
            {
                List<AbstractProperty> properties = physObject.gameObject.GetComponentsInChildren<AbstractProperty>().OfType<AbstractProperty>().ToList();
                foreach (AbstractProperty property in properties)
                {
                    string propMapleCode = string.Format(
                        "{0}__{1}:={2}: ",
                        physObject.Identifier,
                        property.GetName(),
                        property.GetValue()
                        );
                    message += "Set var: " + propMapleCode + "\n";
                    MapleEngine.EvalMapleStatement(kv, Encoding.UTF8.GetBytes(propMapleCode));
                }
            }

            _isPlay = true;
        }

        if (GUI.Button(new Rect(210, 25, 80, 24), "Reset"))
        {
            _isPlay = false;
            foreach (PhysObject physObject in FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList())
                physObject.Reset();
        }

        GUI.EndGroup();

        if (message.Length > 500)
            message = "Message: \n";

        if (message.Length > 0)
            GUI.Label(new Rect(150, 20, 400, 500), message);
    }

    void OnApplicationQuit()
    {
        MapleEngine.StopMaple(kv);
    }

    IntPtr kv;

    private void StartMapleEngine()
    {
        MapleEngine.MapleCallbacks cb;
        byte[] err = new byte[2048];


        String[] argv = new String[2];
        argv[0] = "maple";
        argv[1] = "-A2";

        cb.textCallBack = cbText;
        cb.errorCallBack = cbError;
        cb.statusCallBack = cbStatus;
        cb.readlineCallBack = null;
        cb.redirectCallBack = null;
        cb.streamCallBack = null;
        cb.queryInterrupt = null;
        cb.callbackCallBack = null;

        try
        {
            kv = MapleEngine.StartMaple(2, argv, ref cb, IntPtr.Zero, IntPtr.Zero, err);
        }
        catch (DllNotFoundException e)
        {
            Console.WriteLine(e.ToString());
            Console.ReadKey();
            return;
        }
        catch (EntryPointNotFoundException e)
        {
            Console.WriteLine(e.ToString());
            Console.ReadKey();
            return;
        }

        if (kv.ToInt64() == 0)
        {
            Console.WriteLine("Fatal Error, could not start Maple: "
                    + System.Text.Encoding.UTF8.GetString(err, 0, Array.IndexOf(err, (byte)0))
                );
            return;
        }

        Console.WriteLine("    |\\^/|     OpenMaple");
        Console.WriteLine("._|\\|   |/|_. Copyright (c) Maplesoft, a division of Waterloo Maple Inc. 2009");
        Console.WriteLine(" \\OPENMAPLE/  All rights reserved. Maple and OpenMaple are trademarks of");
        Console.WriteLine(" <____ ____>  Waterloo Maple Inc.");
        Console.WriteLine("      |       ");

        MapleEngine.EvalMapleStatement(kv, Encoding.UTF8.GetBytes("plotsetup(maplet):"));
    }

    static string result = "";
    public static void cbText(IntPtr data, int tag, IntPtr output)
    {
        //Console.WriteLine("------------------------------------------------------------------------");
        result = Marshal.PtrToStringAnsi(output);
        //Console.WriteLine("[CALCULATE] \n{0}", result);
        message += result + "\n";
        message += "Start applying Maple response\n";
        result.Replace(".", "0.");
        new MapleParser().Process(result, FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList());
    }

    public static void cbError(IntPtr data, IntPtr offset, IntPtr msg)
    {
        string s = Marshal.PtrToStringAnsi(msg);
        //Console.WriteLine(s);
        Debug.LogError(s);
    }

    public static void cbStatus(IntPtr data, IntPtr used, IntPtr alloc, double time)
    {
        Debug.Log("cputime=" + time
          + "; memory used=" + used.ToInt64() + "kB"
          + " alloc=" + alloc.ToInt64() + "kB"
        );
    }
}
