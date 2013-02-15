using UnityEngine;
using System.Collections;

public class MatematicsModelView : MonoBehaviour
{
    public bool Visible;

    private Rect _windowPosition;
    private Vector2 _scrollViewVector = Vector2.zero;
    public PhysicsObject CurrentPhysicsObject { get; set; }

    // Use this for initialization
    void Start()
    {
        _windowPosition = new Rect(Screen.width / 4, Screen.height / 4, 200, Screen.height / 2);
    }

    void OnGUI()
    {
        if (Visible)
        {
            _windowPosition = GUI.Window(
                5,
                _windowPosition,
                DoWindow,
                "Мат. модель");
        }
    }

    void DoWindow(int id)
    {
        _scrollViewVector = GUI.BeginScrollView(new Rect(10, 10, _windowPosition.width - 20, _windowPosition.height - 20), _scrollViewVector, new Rect(0, 0, 160, 400));
        int index = 0;
        if (CurrentPhysicsObject != null)
        foreach (PhysicsProperty property in CurrentPhysicsObject.GetProperties())
        {
            GUI.Label(new Rect(5, 24 * index, 100, 24), property.GetName());
            property.SetValue(GUI.TextField(new Rect(110, 24 * index, 80, 24), property.GetValue()));
            index++;
        }
        GUI.EndScrollView();

        GUI.DragWindow();
    }
}
