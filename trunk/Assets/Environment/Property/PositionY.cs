using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Properties/Native/Position Y")]
public class PositionY : MonoBehaviour, IProperty
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
        return "PositionY";
    }

    public object GetValue()
    {
        return gameObject.transform.position.y;
    }

    public void SetValue(object obj)
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            (float) obj,
            gameObject.transform.position.z
            );
    }
}
