using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IProperty
{
    string GetName();
    
    Object GetValue();
    void SetValue(Object obj);
}
