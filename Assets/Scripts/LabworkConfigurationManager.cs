using UnityEngine;
using System.Collections;

public class LabworkConfigurationManager : MonoBehaviour
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
        LabworkConfig config = new LabworkConfig();
        config.Start = 0;
        config.Finish = 50;
        config.Step = 0.1f;
        config.Current = 0;
        SetConfig(config);
    }
}
