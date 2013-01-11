using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class PhysicsObjectBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        try
        {
            Camera.current.GetComponent<EnvironmentManager>().AddElement(this.gameObject);
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
