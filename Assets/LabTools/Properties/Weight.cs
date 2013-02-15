using System.Globalization;
using UnityEngine;

//[AddComponentMenu("VPL Properties/Weight")]
[AddComponentMenu("Физ.свойства (дополнительные)/Вес")]
public class Weight : PhysicsProperty
{
    public override string GetName()
    {
        return "Weight";
    }
}