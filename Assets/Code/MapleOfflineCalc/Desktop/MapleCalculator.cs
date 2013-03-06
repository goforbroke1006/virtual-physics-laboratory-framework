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

    private static List<String> _expressions;
    private static readonly Regex CodeItemRegex = new Regex(@"(print\(\'endl\'\)\;)|([a-zA-Z_]+\s=\sseq\([\w_\[\]\(\)\,\.\=\s]+[;$])|(while[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)\=\<\>\:]+end:)|([a-zA-Z_]+:=[a-zA-Z0-9\s_.,\+\-\*\/\[\]\(\)]+[:$])");
    private static int _counter = 0;
    private static String _finalResult = "";

    public static void Calculate(String code, LabPlayer labPlayer, OutputConsole console)
    {
        _labPlayer = labPlayer;
        _console = console;
        //_console.AddMessage("MapleCalculator - Calculate - Code for calculating: \n" + code);

        if (!_started)
            StartMaple();
        _expressions = GetExpressionsList(code);
        int attemps = 0;
        NextCalc();
        while (true)
        {
            if (_returnResult)
            {
                _console.AddMessage("FinalResult: \n" + _finalResult); 
                _labPlayer.SetResponse(_finalResult); break;
            }
            if (tempResult.Length > 0)
            {
                _finalResult += tempResult;
                //_console.AddMessage("Temp RESULT length = " + tempResult.Length);
                //_console.AddMessage("Final RESULT length = " + _finalResult.Length);
                tempResult = ""; NextCalc();
            }
            if (attemps >= 20) { NextCalc(); attemps = 0; }

            Thread.Sleep(50);
            attemps++;
        }
    }

    private static void NextCalc()
    {
        if (_expressions[_counter] != null)
        {
            _counter++;
            IntPtr val = MapleEngine.EvalMapleStatement(_kv, Encoding.ASCII.GetBytes(_expressions[_counter - 1]));
            if (MapleEngine.IsMapleStop(_kv, val).ToInt32() != 0)
                _returnResult = true; //return;
        }
        else _returnResult = true;
    }

    private static List<String> GetExpressionsList(String code)
    {
        List<String> list = new List<String>();
        MatchCollection collection = CodeItemRegex.Matches(code);
        foreach (Match match in collection)
        {
            String temp = "print('null')";
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
    private static String tempResult = "";
    private static void cbText(IntPtr data, int tag, IntPtr output)
    {
        tempResult = Marshal.PtrToStringAnsi(output);
        if (_counter == _expressions.Count)
            _returnResult = true;
    }

    private static void cbError(IntPtr data, IntPtr offset, IntPtr msg)
    {
        String s = Marshal.PtrToStringAnsi(msg);
        Console.WriteLine(s);
    }

    private static void cbStatus(IntPtr data, IntPtr used, IntPtr alloc, double time)
    {
        Console.WriteLine("cputime=" + time + "; memory used=" + used.ToInt64() + "kB" + " alloc=" + alloc.ToInt64() + "kB");
    }
}