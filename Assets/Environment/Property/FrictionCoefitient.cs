using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Friction Coefitient")]
public class FrictionCoefitient : MonoBehaviour, IProperty
{
    private float _value = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetName()
    {
        return "FrictionCoefitient";
    }

    public object GetValue()
    {
        return _value;
    }

    public void SetValue(object obj)
    {
        _value = (float) obj;
    }
}
