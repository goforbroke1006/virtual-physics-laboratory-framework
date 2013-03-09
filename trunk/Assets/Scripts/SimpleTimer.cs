using System.Globalization;
using UnityEngine;
using System.Collections;

public class SimpleTimer : MonoBehaviour
{
    public Vector2 Position;

    private Rect _windowPosition;// = new Rect(100, 10, 200, 100);
    private bool _isStarted;
    private float _ctime = 0.1f;

	// Use this for initialization
	void Start ()
	{
	    _windowPosition = 
            Position != new Vector2(0, 0) ? 
            new Rect(Position.x, Position.y, 200, 100) : new Rect(Screen.width - 200, Screen.height - 100, 200, 100);
	}

    // Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        _windowPosition = GUI.Window(7, _windowPosition, DoWindow, "Таймер");
    }

    void DoWindow(int id)
    {
//        if (_isStarted)
//            ((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("_ctime = " + _ctime);

        GUI.TextField(new Rect(5, 25, 90, 23), _ctime.ToString(CultureInfo.InvariantCulture));
        if (GUI.Button(new Rect(5, 50, 40, 23), "Старт")) 
            _isStarted = true;
        if (GUI.Button(new Rect(50, 50, 40, 23), "Стоп")) 
            _isStarted = false;
        if (GUI.Button(new Rect(95, 50, 40, 23), "Сброс"))
            _ctime = 0;


        GUI.DragWindow();
    }

    public void AddTime(float sec)
    {
        if (_isStarted)
        {
            _ctime += sec;
            //((OutputConsole)FindObjectOfType(typeof(OutputConsole))).AddMessage("_ctime = " + _ctime);
            Debug.Log("Add time. Current = " + _ctime);
        }
    }
}
