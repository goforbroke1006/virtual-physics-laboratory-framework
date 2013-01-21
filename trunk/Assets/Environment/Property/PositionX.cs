using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position X")]
public class PositionX : MonoBehaviour, IProperty
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
        return "PositionX";
    }

    public object GetValue()
    {
        return gameObject.transform.position.x;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.position = new Vector3(
            (float) obj,
            gameObject.transform.position.y,
            gameObject.transform.position.z
            );
    }
}
