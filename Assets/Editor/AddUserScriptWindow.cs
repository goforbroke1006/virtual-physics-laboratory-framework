using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AddUserScriptWindow : EditorWindow
{
    private string _name = "";
    private string _code = "";

    [MenuItem("Window/Add user script")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow(typeof(AddUserScriptWindow));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Add user script settings", EditorStyles.boldLabel);
        GUILayout.Space(10);

        _name = EditorGUILayout.TextField("Name", _name);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Code");
        _code = EditorGUILayout.TextArea(_code, GUILayout.MinWidth(300));
        GUILayout.EndHorizontal();

        if (_name.Length > 0 && _code.Length > 0)
            if (GUILayout.Button("Add"))
            {
                string path = "Assets/Environment/UserScript/UserScript.prefab";
                GameObject gameObj = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
                gameObj.GetComponent<UserScript>().Name = _name;
                gameObj.GetComponent<UserScript>().Code = _code;

                _name = "";
                _code = "";
            }
    }
}

// body_1__PositionY := body_1__PositionY - 0.05 : body_2__PositionY := body_2__PositionY - 0.05 :
