using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class UserScript : MonoBehaviour
{
    private PhysComponent[] Members;
    public string Code = "";

    // Use this for initialization
    void Start()
    {
        Members = 
            gameObject.transform.
            parent.gameObject.transform.transform.
            parent.gameObject.
            GetComponentInChildren<PhysComponentsManager>().
            GetComponentsInChildren<PhysComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Apply MC - " + Code);

        // Send Maple-code to Maple-CORE
        foreach (PhysComponent obj in Members)
        {
            obj.transform.position += new Vector3(0.001f, 0.001f, 0.001f);
        }
    }
}
