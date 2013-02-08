using System.Globalization;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position Y")]
public class PositionY : AbstractProperty
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetName()
    {
        return "PosY";
    }

    public override string GetValue()
    {
        return gameObject.transform.position.y.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            float.Parse(val),
            gameObject.transform.position.z
            );
    }
}
