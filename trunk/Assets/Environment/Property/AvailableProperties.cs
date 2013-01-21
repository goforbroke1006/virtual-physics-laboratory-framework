using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class AvailableProperties
{
    private readonly List<IProperty> _properties = new List<IProperty>();

    private AvailableProperties()
    {
        _properties.Add(new PositionX());
        _properties.Add(new PositionY());
        _properties.Add(new PositionZ());
        
        _properties.Add(new RotationX());
        _properties.Add(new RotationY());
        _properties.Add(new RotationZ());
    }

    private static AvailableProperties _instance = null;

    public static AvailableProperties Get()
    {
        return _instance ?? (_instance = new AvailableProperties());
    }
}
