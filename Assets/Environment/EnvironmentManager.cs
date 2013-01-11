using UnityEditor;
using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
//	    Handles.PositionHandle(new Vector3(1, 1, 1), Quaternion.identity);

//        Gizmos.DrawLine(new Vector3(), new Vector3(10, 0, 0));
//        Gizmos.DrawLine(new Vector3(), new Vector3(0, 10, 0));
//        Gizmos.DrawLine(new Vector3(), new Vector3(0, 0, 10));
	}

    public void AddElement(GameObject obj)
    {
        Debug.Log("Select new component " + obj.name);
    }

    public float width = 32.0f;
    public float height = 32.0f;

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(), new Vector3(10, 0, 0));
        Gizmos.DrawLine(new Vector3(), new Vector3(0, 10, 0));
        Gizmos.DrawLine(new Vector3(), new Vector3(0, 0, 10));  
    }
}
