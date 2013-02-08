using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

public abstract class AbstractProperty : MonoBehaviour
{
    /*protected GameObject parent;

    // Use this for initialization
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }*/

    public abstract string GetName();

    public abstract string GetValue();
    public abstract void SetValue(string val);
}
