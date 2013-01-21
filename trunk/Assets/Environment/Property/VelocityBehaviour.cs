using UnityEngine;
using UnityEditor;
using System.Collections;

[AddComponentMenu("VPL Library Behavior/Velocity (Скорость)")]
public class VelocityBehaviour : MonoBehaviour
{
    public Vector3 Velocity = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Velocity;
    }
}
