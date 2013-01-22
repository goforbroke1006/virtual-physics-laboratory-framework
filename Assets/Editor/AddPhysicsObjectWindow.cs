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

    [MenuItem("Window/Add physics object")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof (AddPhysicsObjectWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Add physics object settings", EditorStyles.boldLabel);
        GUILayout.Space(10);

        /*GUILayout.BeginHorizontal();
        GUILayout.Label("Identifier");
        _identifier = GUILayout.TextField(_identifier);
        GUILayout.EndHorizontal();*/

        _identifier = EditorGUILayout.TextField("Identifier", _identifier);

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

        GUILayout.Label("Selected: " + selectedPhysObj, EditorStyles.boldLabel);

        if (selectedPhysObj.Length > 0 && idError.Length == 0)
        {
            if (GUILayout.Button("Add"))
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
                     error = "Numbers is not available.";
                 else if (new Regex(@"_").IsMatch(name))
                     error = "Underscore is not available.";
                 else
                     error = "Use only 'a-z' and 'A-Z' (not 'space', underscore and special symbols).";
             }

             return error;
         }
    }
}