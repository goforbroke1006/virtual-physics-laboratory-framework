using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("VPL Library Behavior/Acceleration (Ускорение)")]
public class AccelerationBehaviour : MonoBehaviour {

    public Vector3 Acceleration = new Vector3(0.01f, 0, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    try
	    {
	        VelocityBehaviour velocityBehaviour = (VelocityBehaviour) GetComponent(typeof (VelocityBehaviour));
	        velocityBehaviour.Velocity += Acceleration;
	    }
	    catch (Exception exception)
	    {
	        Debug.LogError("" + exception.Message);
	        throw;
	    }
	}
}
