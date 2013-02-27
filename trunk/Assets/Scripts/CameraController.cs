using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private bool enabled = false;

    private bool moveForward = false;
    private bool moveBack = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
            enabled = true;
        if (Input.GetMouseButtonUp(1))
            enabled = false;

        if (enabled)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) moveForward = true;
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) moveForward = false;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) moveBack = true;
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) moveBack = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) moveLeft = true;
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) moveLeft = false;

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) moveRight = true;
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) moveRight = false;
        }

        if (!enabled)
        {
            moveForward = false;
            moveBack = false;
            moveLeft = false;
            moveRight = false;
            moveUp = false;
            moveDown = false;
        }

        if (moveForward) transform.Translate(transform.rotation * transform.forward * Time.deltaTime);
        if (moveBack) transform.Translate(transform.forward * -1 * Time.deltaTime);
        if (moveLeft) transform.Translate(transform.right * -1 * Time.deltaTime);
        if (moveRight) transform.Translate(transform.right * Time.deltaTime);
	}
}
