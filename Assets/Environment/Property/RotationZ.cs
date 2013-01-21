using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Rotation Z")]
public class RotationZ : MonoBehaviour, IProperty
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
        return "RotationZ";
    }

    public object GetValue()
    {
        return gameObject.transform.rotation.z;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.rotation = new Quaternion(
            gameObject.transform.rotation.x,
            gameObject.transform.rotation.y,
            (float)obj,
            gameObject.transform.rotation.w
            );
    }
}
