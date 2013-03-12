using UnityEngine;

public class DrawThread : MonoBehaviour
{
    public Transform SecondPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void OnPostRender() //OnPostRender
    {
        Debug.Log(string.Format("First point: {0} {1} {2}", 
            transform.position.x, transform.position.y, transform.position.z));
        Debug.Log(string.Format("Second point: {0} {1} {2}", 
            SecondPoint.transform.position.x, SecondPoint.transform.position.y, SecondPoint.transform.position.z));

        /*GL.Begin(GL.LINES);
        //GL.Color(new Color(0, 0, 0, 1));
        GL.Color(Color.black);
        GL.Vertex(transform.position);
        GL.Vertex(SecondPoint.transform.position);
        GL.End();*/

        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 1, 1, 0.5f));
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(10, 0, 0);
        GL.Vertex3(0, 10, 0);
        GL.Vertex3(10, 10, 0);
        GL.Color(new Color(0, 0, 0, 0.5f));
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 10, 0);
        GL.Vertex3(10, 0, 0);
        GL.Vertex3(10, 10, 0);
        GL.End();
    }
}
