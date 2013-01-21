using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Rotation X")]
public class RotationX : MonoBehaviour, IProperty
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
        return "RotationX";
    }

    public object GetValue()
    {
        return gameObject.transform.rotation.x;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.rotation = new Quaternion(
            (float) obj,
            gameObject.transform.rotation.y,
            gameObject.transform.rotation.z,
            gameObject.transform.rotation.w
            );
    }
}
