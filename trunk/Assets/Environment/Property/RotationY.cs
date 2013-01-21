using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Rotation Y")]
public class RotationY : MonoBehaviour, IProperty
{
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
        return "RotationY";
    }

    public object GetValue()
    {
        return gameObject.transform.rotation.y;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.rotation = new Quaternion(
            gameObject.transform.rotation.x,
            (float)obj,
            gameObject.transform.rotation.z,
            gameObject.transform.rotation.w
            );
    }
}
