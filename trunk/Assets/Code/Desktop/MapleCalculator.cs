using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

class MapleCalculator
{
    private static bool _started = false;
    private static MapleEngine.MapleCallbacks _cb;
    private static IntPtr _kv = new IntPtr(0);
    private static LabPlayer _labPlayer;
    private static OutputConsole _console;

    public MapleCalculator()
    {
        if (!_started)
            StartMaple();
    }

    private static List<string> expressions;
    private static Regex _codeItemRegex = new Regex(@"(print\(\'endl\'\)\;)|([a-zA-Z_]+\s=\sseq\([\w_\[\]\(\)\,\.\=\s]+[;$])|(while[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)\=\<\>\:]+end:)|([a-zA-Z_]+:=[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)]+[:$])");
    private static int counter = 0;
    private static string Result = "";

    public static void Calculate(string code, LabPlayer labPlayer, OutputConsole console)
    {
        _labPlayer = labPlayer;
        _console = console;

        if (!_started)
            StartMaple();
        expressions = GetExpressionsList(code);
        int attemps = 0;
        NextCalc();
        while (true)
        {
            if (_returnResult) { _console.AddMessage("Result: \n" + Result); _labPlayer.SetResponse(Result); break;}
            if (tempResult.Length > 0) { Result += tempResult; tempResult = ""; NextCalc(); }
            if (attemps >= 20) { NextCalc(); attemps = 0; }

            Thread.Sleep(50);
            attemps++;
        }
    }

    private static void NextCalc()
    {
        if (expressions[counter] != null)
        {
            counter++;
            IntPtr val = MapleEngine.EvalMapleStatement(_kv, Encoding.ASCII.GetBytes(expressions[counter - 1]));
            if (MapleEngine.IsMapleStop(_kv, val).ToInt32() != 0)
                _returnResult = true; //return;
        }
        else _returnResult = true;
    }

    private static List<string> GetExpressionsList(string code)
    {
        List<string> list = new List<string>();
        MatchCollection collection = _codeItemRegex.Matches(code);
        foreach (Match match in collection)
        {
            string temp = "print('null')";
            if (match.Groups[1].Value.Length > 0) temp = match.Groups[1].Value;
            else if (match.Groups[2].Value.Length > 0) temp = match.Groups[2].Value;
            else if (match.Groups[3].Value.Length > 0) temp = match.Groups[3].Value;
            else if (match.Groups[4].Value.Length > 0) temp = match.Groups[4].Value;
            list.Add(temp);
        }
        return list;
    }

    public static void StartMaple()
    {
        if (!_started) _kv = new IntPtr(-1);
        byte[] err = new byte[2048];

        String[] argv = new String[2];
        argv[0] = "maple";
        argv[1] = "-A2";

        _cb.textCallBack = cbText;
        _cb.errorCallBack = cbError;
        _cb.statusCallBack = cbStatus;
        _cb.readlineCallBack = null;
        _cb.redirectCallBack = null;
        _cb.streamCallBack = null;
        _cb.queryInterrupt = null;
        _cb.callbackCallBack = null;

        try
        {
            _kv = MapleEngine.StartMaple(2, argv, ref _cb, IntPtr.Zero, IntPtr.Zero, err);
        }
        catch (DllNotFoundException exception)
        {
            Debug.Log(exception.Message);
            return;
        }
        catch (EntryPointNotFoundException exception)
        {
            Debug.Log(exception.Message);
            return;
        }

        if (_kv.ToInt64() == 0)
        {
            Debug.Log("_kv.ToInt64() == 0");
            return;
        }

        MapleEngine.EvalMapleStatement(_kv, Encoding.ASCII.GetBytes("plotsetup(maplet):"));

        _started = true;
    }

    public static void StopMaple()
    {
        MapleEngine.StopMaple(_kv);
    }

    private static bool _returnResult = false;
    private static string tempResult = "";
    private static void cbText(IntPtr data, int tag, IntPtr output)
    {
        tempResult = Marshal.PtrToStringAnsi(output);
        if (counter == expressions.Count)
            _returnResult = true;
    }

    private static void cbError(IntPtr data, IntPtr offset, IntPtr msg)
    {
        string s = Marshal.PtrToStringAnsi(msg);
        Console.WriteLine(s);
    }

    private static void cbStatus(IntPtr data, IntPtr used, IntPtr alloc, double time)
    {
        Console.WriteLine("cputime=" + time + "; memory used=" + used.ToInt64() + "kB" + " alloc=" + alloc.ToInt64() + "kB");
    }
}