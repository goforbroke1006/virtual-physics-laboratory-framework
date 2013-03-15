using System.Globalization;
using UnityEngine;
using System.Collections;

public class ConfigurationManager : MonoBehaviour, IChildrenWindow
{
    private LabworkConfig _config;
    //private LabworkConfig _tempConfig;

    private bool _isOpened;
    private Rect _windowPosition;

    // Use this for initialization
    void Start()
    {
        SetDefaultConfig();
        _windowPosition = new Rect(Screen.width / 4, Screen.height / 12, Screen.width / 2, Screen.height / 8 * 7);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (_isOpened)
        {
            _windowPosition = GUI.Window(
                2,
                _windowPosition,
                DoWindow,
                "Конфигурация");
        }
    }

    private string _tempStart;
    private string _tempFinish;
    private string _tempCurrTime;
    private string _tempStep;

    public void DoWindow(int id)
    {
        GUI.Label(new Rect(10, 25, 150, 23), "Начальное время (сек)");
        _tempStart = GUI.TextField(new Rect(160, 25, 75, 23), _tempStart);

        GUI.Label(new Rect(_windowPosition.width / 2, 25, 150, 23), "Конечное время (сек)");
        _tempFinish = GUI.TextField(new Rect(_windowPosition.width / 2 + 160, 25, 75, 23), _tempFinish);

        GUI.Label(new Rect(10, 50, 150, 23), "Текущее время (сек)");
        _tempCurrTime = GUI.TextField(new Rect(160, 50, 75, 23), _tempCurrTime);

        GUI.Label(new Rect(_windowPosition.width / 2, 50, 150, 23), "Временной шаг (сек)");
        _tempStep = GUI.TextField(new Rect(_windowPosition.width / 2 + 160, 50, 75, 23), _tempStep);

        GUI.Label(new Rect(10, 75, 250, 23), "Переменные по-умолчанию:");
        GUI.Label(new Rect(30, 95, _windowPosition.width - 40, _windowPosition.height/3),
            "start - начальное время\n" +
            "finish - конечное время\n" +
            "step - временной шаг (величина обратнопропорциональна скорости воспроизведения)\n" +
            "ctime - текущее время\n" +
            "calc_count - размерность полей (кол-во вычислений)\n" +
            "counter - номер текущего вычисления для полей\n" +
            "isplay - вычисления выполняются до тех пор пока переменная равна 1"
            );

        GUI.Label(new Rect(10, 95 + _windowPosition.height / 3 - 40 + 10, 250, 23), "Дополнительные переменные:");
        _config.AdditionalVars = GUI.TextArea(
            new Rect(30, 95 + _windowPosition.height / 3 - 40 + 33, _windowPosition.width - 40, _windowPosition.height / 3),
            _config.AdditionalVars);

        GUI.Label(new Rect(10, 95 + _windowPosition.height / 3 - 40 + 10, 250, 23), "Условие выхода:");

        if (GUI.Button(new Rect(10, _windowPosition.height - 35, 80, 23), "Отмена"))
            _isOpened = false;

        if (GUI.Button(new Rect(100, _windowPosition.height - 35, 80, 23), "Сохранить"))
        {
            float tempFlStart;
            if (float.TryParse(_tempStart, out tempFlStart))
                _config.Start = tempFlStart;

            float tempFlFinish;
            if (float.TryParse(_tempFinish, out tempFlFinish))
                _config.Finish = tempFlFinish;

            float tempFlCurrent;
            if (float.TryParse(_tempCurrTime, out tempFlCurrent))
                _config.Current = tempFlCurrent;

            float tempFlStep;
            if (float.TryParse(_tempStep, out tempFlStep))
                _config.Step = tempFlStep;

            _isOpened = false;
        }

        GUI.DragWindow();
    }

    public bool IsOpened()
    {
        return _isOpened;
    }

    public void SetOpened(bool opened)
    {
        _isOpened = opened;
        if (_isOpened)
        {
            _tempStart = _config.Start.ToString(CultureInfo.InvariantCulture);
            _tempFinish = _config.Finish.ToString(CultureInfo.InvariantCulture);
            _tempCurrTime = _config.Current.ToString(CultureInfo.InvariantCulture);
            _tempStep = _config.Step.ToString(CultureInfo.InvariantCulture);
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
            Current = 0,
            AdditionalVars = "gravity:=9.8: \n" +
                             "normal_temperature:=20: \n"
        };
        SetConfig(config);
    }
}
