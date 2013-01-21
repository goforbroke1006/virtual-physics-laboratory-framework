using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    private class EditorPhysObjNameValidator 
    {
         public static string Validate(string name)
         {
             string error = string.Empty;

             int outInt;
             string f = "";
             if (name.Length > 0)
                 f = name.Substring(0, 1);
             
             if (name.Length == 0)
                 error = "Please type IDENTIFIER.";
             else if (name.IndexOf(" ", System.StringComparison.Ordinal) > -1)
                 error = "You must use letters, \nnumbers and \nsymbol '_' only. \nNot 'space' or specail symbols.";
             else if (name.IndexOf("__", System.StringComparison.Ordinal) > -1)
                 error = "Wrong symbol '__'.";
             else if (int.TryParse(f, out outInt))
                 error = "First symbol must be letter.";

             List<PhysObject> list = GameObject.FindObjectsOfType(typeof(PhysObject)).OfType<PhysObject>().ToList();
             foreach (PhysObject physObject in list)
                 if (physObject.Identifier == name && name.Length > 0)
                     error = "This name already exists.";

             return error;
         }
    }
}