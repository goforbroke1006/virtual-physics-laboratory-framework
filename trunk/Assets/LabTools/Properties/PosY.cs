using System.Globalization;
using UnityEngine;
using System.Collections;

//[AddComponentMenu("VPL Properties/Position Y")]
[AddComponentMenu("Физ.свойства (основные)/Координаты Y")]
public class PosY : PhysicsProperty
{
    public override string GetName()
    {
        return "PosY";
    }

    public override string GetValue()
    {
        return transform.position.y.ToString(CultureInfo.InvariantCulture);
    }

    public override void SetValue(string val)
    {
        transform.position = new Vector3(
            transform.position.x,
            float.Parse(val),
            transform.position.z
            );
    }
}
