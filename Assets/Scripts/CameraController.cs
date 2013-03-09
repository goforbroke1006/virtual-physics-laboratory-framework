using UnityEngine;

public class CameraController : MonoBehaviour 
{
    private bool _enabled;

    private bool _moveForward;
    private bool _moveBack;
    private bool _moveLeft;
    private bool _moveRight;
//    private bool moveUp = false;
//    private bool moveDown = false;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
            _enabled = true;
        if (Input.GetMouseButtonUp(1))
            _enabled = false;

        if (_enabled)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) _moveForward = true;
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) _moveForward = false;

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) _moveBack = true;
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) _moveBack = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) _moveLeft = true;
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) _moveLeft = false;

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) _moveRight = true;
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) _moveRight = false;
        }

        if (!_enabled)
        {
            _moveForward = false;
            _moveBack = false;
            _moveLeft = false;
            _moveRight = false;
//            moveUp = false;
//            moveDown = false;
        }

        if (_moveForward) transform.Translate(transform.rotation * transform.forward * Time.deltaTime);
        if (_moveBack) transform.Translate(transform.forward * -1 * Time.deltaTime);
        if (_moveLeft) transform.Translate(transform.right * -1 * Time.deltaTime);
        if (_moveRight) transform.Translate(transform.right * Time.deltaTime);
	}
}
