using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class AddPhysicsObjectWindow : EditorWindow
{
    private string _identifier = "";
    private string selectedPhysObj = "";
    private Vector2 scrollPos;

    private string idError;

    [MenuItem("Window/Добавление физических объектов")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof (AddPhysicsObjectWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Опции добавления объектов", EditorStyles.boldLabel);
        GUILayout.Space(10);

        _identifier = EditorGUILayout.TextField("Идентификатор", _identifier);

        idError = EditorPhysObjNameValidator.Validate(_identifier);
        if (idError.Length > 0)
            GUILayout.Label(idError, EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true), GUILayout.Height(100));
        foreach (string physObject in AvailPhysObjsRepository.Get().GetListPhysObjects())
        {
            if (GUILayout.Button(physObject))
                selectedPhysObj = physObject;
        }
        EditorGUILayout.EndScrollView();

        GUILayout.Label("Выбран: " + selectedPhysObj, EditorStyles.boldLabel);

        if (selectedPhysObj.Length > 0 && idError.Length == 0)
        {
            if (GUILayout.Button("Добавить"))
            {
                AvailPhysObjsRepository.Get().CreatePhysObjectByPrefabName(selectedPhysObj, _identifier);
                _identifier = String.Empty;
                selectedPhysObj = String.Empty;
            }
        }
        Repaint();
    }

    private static class EditorPhysObjNameValidator 
    {
         public static string Validate(string name)
         {
             string error = string.Empty;

             Regex onlyLettersRegex = new Regex(@"^[\p{L}]+$");

             if (!onlyLettersRegex.IsMatch(name))
             {
                 if (new Regex(@"\d").IsMatch(name))
                     error = "Использование чисел недопустимо.";
                 else if (new Regex(@"_").IsMatch(name))
                     error = "Символ '_'(нижн.подчеркивание) не допустим.";
                 else
                     error = "Используйте только символы 'a-z' и 'A-Z' (недопустимы ' '(пробел), '_'(нижн.подчеркивание) и спец.символы).";
             }

             return error;
         }
    }
}