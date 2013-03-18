using System;
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
        _windowPosition = new Rect(Screen.width / 4, Screen.height / 12, 500, 400);
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
        try
        {
            GUI.Label(new Rect(5, 25, 150, 23), "Начальное время (сек)");
            _tempStart = GUI.TextField(new Rect(155, 25, 75, 23), _tempStart);

            GUI.Label(new Rect(235, 25, 150, 23), "Конечное время (сек)");
            _tempFinish = GUI.TextField(new Rect(390, 25, 75, 23), _tempFinish);

            GUI.Label(new Rect(5, 50, 150, 23), "Текущее время (сек)");
            _tempCurrTime = GUI.TextField(new Rect(155, 50, 75, 23), _tempCurrTime);

            GUI.Label(new Rect(235, 50, 150, 23), "Временной шаг (сек)");
            _tempStep = GUI.TextField(new Rect(390, 50, 75, 23), _tempStep);

            GUI.Label(new Rect(10, 75, 250, 23), "Переменные по-умолчанию:");
            GUI.Label(new Rect(30, 90, 600, 160),
                "start - начальное время\n" +
                "finish - конечное время\n" +
                "step - временной шаг (велич. обратнопропорциональна скор. воспр.)\n" +
                "ctime - текущее время\n" +
                "calc_count - размерность полей (кол-во вычислений)\n" +
                "counter - номер текущего вычисления для полей\n" +
                "isplay - вычисления выполняются до тех пор пока переменная равна 1"
                );

            GUI.Label(new Rect(10, 205, 250, 23), "Дополнительные переменные (<имя_переменной>:=<значение>:) :");
            GetConfig().AdditionalVars = GUI.TextArea(
                new Rect(30, 225, 400, 70),
                GetConfig().AdditionalVars);

            GUI.Label(new Rect(10, 295, 250, 23), "Условие выхода:");
            GetConfig().EndingExpression = GUI.TextArea(
                new Rect(30, 315, 400, 50),
                GetConfig().EndingExpression);

            if (GUI.Button(new Rect(320, 370, 80, 23), "Отмена"))
                _isOpened = false;

            if (GUI.Button(new Rect(410, 370, 80, 23), "Сохранить"))
            {
                float tempFlStart;
                if (float.TryParse(_tempStart, out tempFlStart))
                    GetConfig().Start = tempFlStart;

                float tempFlFinish;
                if (float.TryParse(_tempFinish, out tempFlFinish))
                    GetConfig().Finish = tempFlFinish;

                float tempFlCurrent;
                if (float.TryParse(_tempCurrTime, out tempFlCurrent))
                    GetConfig().Current = tempFlCurrent;

                float tempFlStep;
                if (float.TryParse(_tempStep, out tempFlStep))
                    GetConfig().Step = tempFlStep;

                _isOpened = false;
            }
        }
        catch (Exception exception)
        {
            BeanManager.GetOutputConsole().AddMessage(exception.Message);
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
            _tempStart = GetConfig().Start.ToString(CultureInfo.InvariantCulture);
            _tempFinish = GetConfig().Finish.ToString(CultureInfo.InvariantCulture);
            _tempCurrTime = GetConfig().Current.ToString(CultureInfo.InvariantCulture);
            _tempStep = GetConfig().Step.ToString(CultureInfo.InvariantCulture);
        }
    }

    public LabworkConfig GetConfig()
    {
        return _config ?? (_config = GetDefaultConfig());
    }

    public void SetConfig(LabworkConfig config)
    {
        _config = config;
    }

    public LabworkConfig GetDefaultConfig()
    {
        LabworkConfig config = new LabworkConfig
        {
            Start = 0,
            Finish = 100,
            Step = 0.1f,
            Current = 0,
            AdditionalVars = "gravity:=9.8: \n" +
                             "normal_temperature:=20: \n",
            EndingExpression = ""
        };
        return config;
    }
}
