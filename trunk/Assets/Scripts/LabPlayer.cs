using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class LabPlayer : MonoBehaviour
{
    private bool _isPlay;

    private LabworkConfig _currentConfig;

    private MapleBuilder _mapleBuilder;
    private MapleParser _mapleParser;

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer && !MapleCalculator.IsMapleStarted)
            MapleCalculator.StartMaple();

        BeanManager.GetConfigurationManager().SetDefaultConfig();
        _currentConfig = BeanManager.GetConfigurationManager().GetConfig();

        _mapleBuilder = new MapleBuilder();
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
            BeanManager.GetWebConnector().JsExternalCall(
                _mapleBuilder.GetCode_Labwork(_currentConfig, PhysicsObjectsManager.GetPhysicsObjects()));
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MapleCalculator.Calculate(
                _mapleBuilder.GetCode_Labwork(_currentConfig, PhysicsObjectsManager.GetPhysicsObjects()),
                this);
        }
    }

    public void PlayLab()
    {
        _mapleParser.Apply();
        _isPlay = true;
    }

    public void PauseLab()
    {
        _isPlay = false;
    }

    public void ResetLab()
    {
        _isPlay = false;
        _mapleParser.Apply(0);
    }

    private string _response = "";
    public string Response
    {
        get { return _response; }
        set
        {
            try
            {
                _response = value;
                _mapleParser.Process(_response);
            }
            catch (Exception exception)
            {
                BeanManager.GetOutputConsole().AddMessage(exception.Message);
            }
        }
    }
}
