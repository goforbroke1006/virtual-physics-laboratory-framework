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

    private PhysicsObjectsManager _physicsObjectsManager;
    private MathematicsModelView _mathematicsModelView;
    private ConfigurationManager _configurationManager;

    //public WatchTimer GlobalLabTimer;

    // Use this for initialization
    void Start()
    {
        _webConnector = (WebConnector)FindObjectOfType(typeof(WebConnector));
        ((ConfigurationManager)FindObjectOfType(typeof(ConfigurationManager))).SetDefaultConfig();
        _currentConfig =
            ((ConfigurationManager)FindObjectOfType(typeof(ConfigurationManager))).GetConfig();

        _mapleBuilder = new MapleBuilder(PhysicsObjectsManager.GetPhysicsObjects());
        _mapleParser = new MapleParser(PhysicsObjectsManager.GetPhysicsObjects());

        _physicsObjectsManager = (PhysicsObjectsManager) FindObjectOfType(typeof (PhysicsObjectsManager));
        _mathematicsModelView = (MathematicsModelView) FindObjectOfType(typeof (MathematicsModelView));
        _configurationManager = (ConfigurationManager)FindObjectOfType(typeof(ConfigurationManager));
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
        // Главное меню
        GUI.BeginGroup(new Rect(10, 10, Screen.width - 20, 45));
        GUI.Box(new Rect(0, 0, Screen.width - 20, 45), "");

        if (GUI.Button(new Rect(10, 5, 100, 35), "Физические\nобъекты"))
            _physicsObjectsManager.SetOpened(!_physicsObjectsManager.IsOpened());
        if (GUI.Button(new Rect(115, 5, 100, 35), "Мат.\nмодель"))
            _mathematicsModelView.SetOpened(!_mathematicsModelView.IsOpened());
        if (GUI.Button(new Rect(220, 5, 100, 35), "Менеждер\nконфигурации"))
            _configurationManager.SetOpened(!_configurationManager.IsOpened());


        if (GUI.Button(new Rect(Screen.width - 100 - 30, 5, 100, 35), "Свернуть\nвсе окна"))
        {
            _configurationManager.SetOpened(false);
            _physicsObjectsManager.SetOpened(false);
            _mathematicsModelView.SetOpened(false);
        }

        GUI.EndGroup();

        // Панель проигрывания
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
        try
        {
            //OutputConsole.GetInstance().AddMessage("Response length = " + resp.Length);
            _response = resp;
            _mapleParser.Process(_response);
        } catch(Exception exception)
        {
            OutputConsole.GetInstance().AddMessage(exception.Message);
        }
    }
}
