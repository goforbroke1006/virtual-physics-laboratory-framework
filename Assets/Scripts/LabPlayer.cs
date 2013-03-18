using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class LabPlayer : MonoBehaviour
{
    public bool IsPlay;

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer) // Application.platform == RuntimePlatform.WindowsEditor ||
            if (!MapleCalculator.IsMapleStarted)
                MapleCalculator.StartMaple();
    }

    void Update()
    {
        if (IsPlay)
        {
            BeanManager.GetMapleParser().Apply();
            BeanManager.GetMainGui().TimelineFloatValue = 
                BeanManager.GetMapleParser().Index * BeanManager.GetConfigurationManager().GetConfig().Step;

            List<SimpleTimer> timers = FindObjectsOfType(typeof(SimpleTimer)).OfType<SimpleTimer>().ToList();
            foreach (SimpleTimer timer in timers)
                timer.AddTime(BeanManager.GetConfigurationManager().GetConfig().Step);
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
        BeanManager.GetOutputConsole().AddMessage("Calculate Lab");

        if (Application.platform == RuntimePlatform.WindowsWebPlayer)
        {
            BeanManager.GetWebConnector().JsExternalCall(
                BeanManager.GetMapleBuilder().GetCode_Labwork(
                    BeanManager.GetConfigurationManager().GetConfig(),
                    PhysicsObjectsManager.GetPhysicsObjects()));
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MapleCalculator.Calculate(
                BeanManager.GetMapleBuilder().GetCode_Labwork(
                    BeanManager.GetConfigurationManager().GetConfig(),
                    PhysicsObjectsManager.GetPhysicsObjects()),
                this);
        }
    }

    public void PlayLab()
    {
        BeanManager.GetMapleParser().Apply();
        IsPlay = true;
    }

    public void PauseLab()
    {
        IsPlay = false;
    }

    public void ResetLab()
    {
        IsPlay = false;
        BeanManager.GetMapleParser().Apply(0);
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
                BeanManager.GetMapleParser().Process(_response);
            }
            catch (Exception exception)
            {
                BeanManager.GetOutputConsole().AddMessage(exception.Message);
            }
        }
    }
}
