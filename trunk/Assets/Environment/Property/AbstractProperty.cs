using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

public abstract class AbstractProperty : MonoBehaviour
{
    public abstract string GetName();

    public abstract string GetValue();
    public abstract void SetValue(string val);
}
