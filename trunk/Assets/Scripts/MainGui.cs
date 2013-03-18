using System;
using UnityEngine;

public class MainGui : MonoBehaviour
{

    public float TimelineFloatValue { get; set; }
    private int _timelineIntValue = 0;
    //private Rect _timelineRect = new Rect(0, -10, 100, 30);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        // Главное меню
        Rect mainMenuRect = new Rect(10, 10, Screen.width - 20, 45);
        GUI.BeginGroup(mainMenuRect);
        GUI.Box(new Rect(0, 0, Screen.width - 20, 45), "");

        if (GUI.Button(new Rect(10, 5, 100, 35), "Физические\nобъекты"))
            BeanManager.GetPhysicsObjectsManager().SetOpened(!BeanManager.GetPhysicsObjectsManager().IsOpened());
        if (GUI.Button(new Rect(115, 5, 100, 35), "Мат.\nмодель"))
            BeanManager.GetMathematicsModelView().SetOpened(!BeanManager.GetMathematicsModelView().IsOpened());
        if (GUI.Button(new Rect(220, 5, 100, 35), "Менеждер\nконфигурации"))
            BeanManager.GetConfigurationManager().SetOpened(!BeanManager.GetConfigurationManager().IsOpened());

        if (GUI.Button(new Rect(mainMenuRect.width - 215, 5, 100, 35), "Консоль"))
            BeanManager.GetOutputConsole().Visible = !BeanManager.GetOutputConsole().Visible;
        if (GUI.Button(new Rect(mainMenuRect.width - 110, 5, 100, 35), "Свернуть\nвсе окна"))
        {
            BeanManager.GetPhysicsObjectsManager().SetOpened(false);
            BeanManager.GetMathematicsModelView().SetOpened(false);
            BeanManager.GetConfigurationManager().SetOpened(false);

            BeanManager.GetOutputConsole().Visible = false;
        }

        GUI.EndGroup();

        // Панель проигрывания
        Rect playingPanelGroupRect = new Rect(Screen.width / 2 - 200, Screen.height - 60, 400, 100);
        try
        {
            Rect timelineRect = new Rect(playingPanelGroupRect.x, playingPanelGroupRect.y - 30, playingPanelGroupRect.width, 10);

            TimelineFloatValue = GUI.HorizontalSlider(
                timelineRect,
                TimelineFloatValue,
                BeanManager.GetConfigurationManager().GetConfig().Start,
                BeanManager.GetConfigurationManager().GetConfig().Finish);
            if (!BeanManager.GetLabPlayer().IsPlay && BeanManager.GetMapleParser().HasFields())
            {
                _timelineIntValue = (int)Math.Round(TimelineFloatValue / BeanManager.GetConfigurationManager().GetConfig().Step);
                BeanManager.GetMapleParser().Apply(_timelineIntValue);
            }
        }
        catch (Exception exception)
        {
            BeanManager.GetOutputConsole().AddMessage(exception.Message);
        }
        GUI.BeginGroup(playingPanelGroupRect);

        GUI.Box(new Rect(0, 0, 400, 55), "Проигрыватель лабораторной работы");

        if (GUI.Button(new Rect(0, 25, 100, 24), "Вычислить"))
        {
            BeanManager.GetOutputConsole().AddMessage("Press button, Try Calculate");
            BeanManager.GetLabPlayer().CalculateLab();
        }
        if (BeanManager.GetLabPlayer().Response.Length > 0)
        {
            if (GUI.Button(new Rect(100, 25, 100, 24), "Запустить")) BeanManager.GetLabPlayer().PlayLab();
        }
        else GUI.Box(new Rect(100, 25, 100, 24), "Запустить");
        if (GUI.Button(new Rect(200, 25, 100, 24), "Пауза")) BeanManager.GetLabPlayer().PauseLab();
        if (GUI.Button(new Rect(300, 25, 100, 24), "Сброс")) BeanManager.GetLabPlayer().ResetLab();

        GUI.EndGroup();
    }
}
