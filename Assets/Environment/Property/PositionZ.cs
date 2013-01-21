using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position Z")]
public class PositionZ : MonoBehaviour, IProperty
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
        return "PositionZ";
    }

    public object GetValue()
    {
        return gameObject.transform.position.x;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            (float)obj
            );
    }
}
