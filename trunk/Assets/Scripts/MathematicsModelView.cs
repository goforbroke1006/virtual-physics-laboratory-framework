using UnityEngine;
using System.Collections;

public class MathematicsModelView : MonoBehaviour, IChildrenWindow
{
    private bool _isOpened;
    private Rect _windowPosition;

    private Vector2 _scrollViewVector = Vector2.zero;
    public PhysicsObject CurrentPhysicsObject { get; set; }

    // Use this for initialization
    void Start()
    {
        _windowPosition = new Rect(220, 60, 200, Screen.height / 2);
    }

    void OnGUI()
    {
        if (_isOpened)
        {
            _windowPosition = GUI.Window(
                1,
                _windowPosition,
                DoWindow,
                "Мат. модель");
        }
    }

    public void DoWindow(int id)
    {
        _scrollViewVector = GUI.BeginScrollView(new Rect(10, 20, _windowPosition.width - 20, _windowPosition.height - 30), _scrollViewVector, new Rect(0, 0, 160, 400));
        int index = 0;
        if (CurrentPhysicsObject != null)
            foreach (BasicPhysicsProperty property in CurrentPhysicsObject.GetProperties())
            {
                GUI.Label(new Rect(5, 24 * index, 100, 24), property.GetName());
                property.SetValue(GUI.TextField(new Rect(110, 24 * index, 80, 24), property.GetValue()));
                index++;
            }
        GUI.EndScrollView();

        GUI.DragWindow();
    }

    public bool IsOpened()
    {
        return _isOpened;
    }

    public void SetOpened(bool opened)
    {
        _isOpened = opened;
    }
}
