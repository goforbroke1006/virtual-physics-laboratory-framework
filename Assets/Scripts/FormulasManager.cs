using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class FormulasManager : MonoBehaviour
{
    public Texture InfoTexture;

    private Rect _windowPosition = new Rect(Screen.width - 10 - 200, 10, 200, Screen.height / 2);
    private Formula _currentFormula = null;

    void OnGUI()
    {
        _windowPosition = GUI.Window(
            1,
            _windowPosition,
            DoPhysComponentsManagetWindowPosition,
            "Формулы");
    }

    void DoPhysComponentsManagetWindowPosition(int id)
    {
        if (_currentFormula != null)
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Текущий элемент: " + _currentFormula.Name);
        else
            GUI.Label(new Rect(10, 30, _windowPosition.width - 20, 24), "Текущий элемент: ");

        if (GetFormulas() != null)
        {
            int counter = 0;
            GUI.BeginScrollView(new Rect(10, 50, _windowPosition.width - 20, 300), new Vector2(0, 100), new Rect(0, 0, _windowPosition.width - 20, 300));
            foreach (Formula script in GetFormulas())
            {
                if (GUI.Button(new Rect(0, counter * 24, _windowPosition.width - 20, 24), script.Name))
                    this.SetCurrentUserScript(script);
                counter++;
            }
            GUI.EndScrollView();
        }

        // Draw INFO button
        GUI.DrawTexture(new Rect(10, _windowPosition.height - 40, 30, 30), InfoTexture);
        if (GUI.Button(new Rect(10, _windowPosition.height - 40, 30, 30), ""))
        {
            // TODO: show inforamtion about current Phys-Object
        }

        // Draw EDIT button
        GUI.DrawTexture(new Rect(45, _windowPosition.height - 40, 30, 30), InfoTexture);
        if (GUI.Button(new Rect(45, _windowPosition.height - 40, 30, 30), ""))
        {
            // TODO: show window for edit properties of current Phys-Object
        }

        GUI.DragWindow();
    }

    void SetCurrentUserScript(Formula formula)
    {
        _currentFormula = formula;
    }

    public static List<Formula> GetFormulas()
    {
        return FindObjectsOfType(typeof (Formula)).OfType<Formula>().ToList();
    }
}
