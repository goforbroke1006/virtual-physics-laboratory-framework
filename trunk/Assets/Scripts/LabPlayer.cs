using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LabPlayer : MonoBehaviour
{
    protected bool _isPlay = false;

    private WebConnector _webConnector;
    protected LabworkConfig _currentConfig;

    protected MapleBuilder _mapleBuilder;
    protected MapleParser _mapleParser;

    //public WatchTimer GlobalLabTimer;

    // Use this for initialization
    void Start()
    {
        _webConnector = (WebConnector)FindObjectOfType(typeof(WebConnector));
        ((LabworkConfigurationManager)FindObjectOfType(typeof(LabworkConfigurationManager))).SetDefaultConfig();
        _currentConfig =
            ((LabworkConfigurationManager)FindObjectOfType(typeof(LabworkConfigurationManager))).GetConfig();

        _mapleBuilder = new MapleBuilder(PhysicsObjectsManager.GetPhysicsObjects(), FormulasManager.GetFormulas());
        _mapleParser = new MapleParser(PhysicsObjectsManager.GetPhysicsObjects());
    }

    void Update()
    {
        if (_isPlay)
        {
            _mapleParser.Apply();

            List<SimpleTimer> timers = FindObjectsOfType(typeof(SimpleTimer)).OfType<SimpleTimer>().ToList();
            foreach (SimpleTimer timer in timers) 
                timer.AddTime(_currentConfig.Step);
        }
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height - 60, 400, 100));
        GUI.Box(new Rect(0, 0, 400, 55), "Проигрыватель лабораторной работы");

        if (GUI.Button(new Rect(0, 25, 100, 24), "Вычислить")) CalculateLab();
        if (_response.Length > 0)
        {
            if (GUI.Button(new Rect(100, 25, 100, 24), "Запустить")) PlayLab();
        }
        else GUI.Box(new Rect(100, 25, 100, 24), "Запустить");
        if (GUI.Button(new Rect(200, 25, 100, 24), "Пауза")) PauseLab();
        if (GUI.Button(new Rect(300, 25, 100, 24), "Сброс")) ResetLab();

        GUI.EndGroup();
    }

    void OnApplicationQuit()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MapleCalculator.StopMaple();
        }
    }

    public void CalculateLab()
    {
        if (Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            _webConnector.JsExternalCall(_mapleBuilder.GetCode_Labwork(_currentConfig));
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || 
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MapleCalculator.Calculate(_mapleBuilder.GetCode_Labwork(
                _currentConfig), 
                this);
        }
    }

    protected void PlayLab()
    {
        _mapleParser.Apply();
        _isPlay = true;
    }

    protected void PauseLab()
    {
        _isPlay = false;
    }

    protected void ResetLab()
    {
        _isPlay = false;
        _mapleParser.Apply(0);
    }

    private string _response = "";
    public void SetResponse(string resp)
    {
        _response = resp;
        _mapleParser.Process(_response);
        OutputConsole.GetInstance().AddMessage("Response length = " + _response.Length);
    }
}
