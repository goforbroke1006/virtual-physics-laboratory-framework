using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position X")]
public class PositionX : AbstractProperty
{
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
        if (viewInfo)
        {
            //Vector3 point = Camera.current.WorldToScreenPoint(gameObject.transform.position);
            GUI.Label(new Rect(200, 200, 200, 50), "PositionX = " + GetValue());
            //viewInfo = false;
        }
    }

    private bool viewInfo = false;
    void OnMouseOver()
    {
        viewInfo = true;
    }

    public override string GetName()
    {
        return "PosX";
    }

    public override object GetValue()
    {
        return gameObject.transform.position.x;
    }

    public override void SetValue(object obj)
    {
        // old position x
        Debug.Log(string.Format("old x {0} {1} {2}", 
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z
            ));

        gameObject.transform.position.Set(float.Parse(obj.ToString()),
            gameObject.transform.position.y,
            gameObject.transform.position.z);
        gameObject.transform.position = new Vector3(
            float.Parse(obj.ToString()),
            gameObject.transform.position.y,
            gameObject.transform.position.z
            );

        // new position x
        Debug.Log(string.Format("new x {0} {1} {2}",
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z
            ));
    }
}
