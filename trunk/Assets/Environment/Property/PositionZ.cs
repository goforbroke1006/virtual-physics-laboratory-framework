using System.Globalization;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position Z")]
public class PositionZ : AbstractProperty
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
        return "PosZ";
    }

    public override string GetValue()
    {
        return gameObject.transform.position.z.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            float.Parse(val)
            );
    }
}
