using UnityEngine;

public class LabPlayer : MonoBehaviour
{
    //public enum PlayerMode { Web, StdAndEdt }

    //public PlayerMode PlayMode = PlayerMode.Web;

    protected bool _isPlay = false;

    private WebConnector _webConnector;
    protected LabworkConfig _currentConfig;

    protected MapleBuilder _mapleBuilder;
    protected MapleParser _mapleParser;

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
        if (_isPlay) _mapleParser.Apply();
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height - 60, 400, 100));
        GUI.Box(new Rect(0, 0, 400, 55), "Lab player");

        if (GUI.Button(new Rect(0, 25, 100, 24), "Calculate")) CalculateLab();
        if (_response.Length > 0)
        {
            if (GUI.Button(new Rect(100, 25, 100, 24), "Play")) PlayLab();
        }
        else GUI.Box(new Rect(100, 25, 100, 24), "Play");
        if (GUI.Button(new Rect(200, 25, 100, 24), "Pause")) PauseLab();
        if (GUI.Button(new Rect(300, 25, 100, 24), "Reset")) ResetLab();

        GUI.EndGroup();
    }

    void OnApplicationQuit()
    {
        //if (PlayMode == PlayerMode.StdAndEdt)
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        MapleCalculator.StopMaple();
#endif
    }

    public void CalculateLab()
    {
        ((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("Calculate Lab");
        //        switch (PlayMode)
        //        {
        //            case PlayerMode.Web:
#if UNITY_WEBPLAYER
        ((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("Web");
        Debug.Log("UNITY_EDITOR - Calculation");
        _webConnector.ExternallCall(_mapleBuilder.GetLabworkCode(_currentConfig));
#endif
        //                break;
        //            case PlayerMode.StdAndEdt:

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        ((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("StdAndEdt");
        Debug.Log("UNITY_STANDALONE_WIN || UNITY_EDITOR - Calculation");
        MapleCalculator.Calculate(_mapleBuilder.GetLabworkCode(_currentConfig), this, (OutputConsole)FindObjectOfType(typeof(OutputConsole)));
#endif

        //                break;
        //        }
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

    protected string _response = "";
    public void SetResponse(string resp)
    {
        _response = resp;
        _mapleParser.Process(_response);
    }
}
