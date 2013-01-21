using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class PhysObject : MonoBehaviour
{
    public string Identifier;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        try
        {
            gameObject.transform.parent.gameObject.GetComponent<PhysObjectsManager>().SetCurrentComponent(this);
        } catch(Exception exception)
        {
            Debug.LogError("PhysicsObjectBehaviour - OnMouseOver() - " + this.name + " - " + exception.Message);
        }
        //((EnvironmentManager)transform.parent.gameObject.GetComponent<EnvironmentManager>()).AddElement(this.gameObject);
        //GetComponent<EnvironmentManager>().AddElement(this.gameObject);
        
    }

    void OnSceneGUI()
    {
        Handles.PositionHandle(new Vector3(), Quaternion.identity);
    }
}
