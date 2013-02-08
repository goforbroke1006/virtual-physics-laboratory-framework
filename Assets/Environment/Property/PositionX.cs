using System;
using System.Globalization;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position X")]
public class PositionX : AbstractProperty
{
    // Use this for initialization
    void Start()
    {
        //
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

    public override string GetValue()
    {
        return gameObject.transform.position.x.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        // old position x
        Debug.Log(string.Format("old x {0} {1} {2}", 
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z
            ));

        /*gameObject.transform.position.Set(float.Parse(val),
            gameObject.transform.position.y,
            gameObject.transform.position.z);*/
        gameObject.transform.position = new Vector3(
            float.Parse(val),
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
