using UnityEngine;
using System.Collections;

public class ConfigurationManager : MonoBehaviour
{
    private LabworkConfig _config;

    // Use this for initialization
    void Start()
    {
        SetDefaultConfig();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, Screen.width - 10 - 30, 40, 30), "��������\n������������"))
        {
            //
        }
    }

    public LabworkConfig GetConfig()
    {
        return _config;
    }

    public void SetConfig(LabworkConfig config)
    {
        _config = config;
    }

    public void SetDefaultConfig()
    {
        LabworkConfig config = new LabworkConfig
        {
            Start = 0, 
            Finish = 100, 
            Step = 0.1f, 
            Current = 0
        };
        SetConfig(config);
    }
}
