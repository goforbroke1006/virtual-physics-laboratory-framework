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
    public static bool IsMapleStarted;
    private static MapleEngine.MapleCallbacks _cb;
    private static IntPtr _kv = new IntPtr(0);

    private static List<string> _expressions;
    
    private static int _counter = 0;
    private static string _finalResult = "";
    private static String _tempResult = "";
    private static bool _returnResult = false;

    public static void Calculate(string mapleCode, LabPlayer labPlayer)
    {
        BeanManager.GetOutputConsole().AddMessage("Calculate with MAPLE");

        _expressions = GetExpressionsList(mapleCode);
        if (!IsMapleStarted)
            StartMaple();

        int attemps = 0;
        NextCalc();
        while (true)
        {
            if (_returnResult)
            {
                BeanManager.GetOutputConsole().AddMessage("Maple calculate is finished.");
                labPlayer.Response = _finalResult + _tempResult;
                _finalResult = "";
                _tempResult = "";
                _returnResult = false;
                _counter = 0;
                break;
            }
            if (_tempResult.Length > 0)
            {
                _finalResult += _tempResult;
                _tempResult = ""; NextCalc();
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
                _returnResult = true;
        }
        else _returnResult = true;
    }

    private static List<String> GetExpressionsList(String code)
    {
        List<string> list = new List<string>();
        MatchCollection collection = MapleCodeUtil.CodeItemRegex.Matches(code);
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
        BeanManager.GetOutputConsole().AddMessage("Start MAPLE");

        if (!IsMapleStarted) _kv = new IntPtr(-1);
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
            BeanManager.GetOutputConsole().AddMessage(exception.Message);
            return;
        }
        catch (EntryPointNotFoundException exception)
        {
            BeanManager.GetOutputConsole().AddMessage(exception.Message);
            return;
        }

        if (_kv.ToInt64() == 0)
        {
            BeanManager.GetOutputConsole().AddMessage("_kv.ToInt64() == 0");
            return;
        }

        MapleEngine.EvalMapleStatement(_kv, Encoding.ASCII.GetBytes("plotsetup(maplet):"));

        IsMapleStarted = true;
    }

    public static void StopMaple()
    {
        BeanManager.GetOutputConsole().AddMessage("Stop MAPLE");
        MapleEngine.StopMaple(_kv);
        IsMapleStarted = false;
    }

    private static void cbText(IntPtr data, int tag, IntPtr output)
    {
        _tempResult = Marshal.PtrToStringAnsi(output);
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