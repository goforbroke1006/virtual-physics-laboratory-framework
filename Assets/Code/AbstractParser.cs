using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AbstractParser
{
    protected List<PhysicsObject> PhysicsObjects;

    public AbstractParser(List<PhysicsObject> physicsObjects)
    {
        PhysicsObjects = physicsObjects;
    }

    public abstract void Process(string data);
    public abstract void Apply();
    public abstract void Apply(int index);
    public abstract void ApplyVariable(string identifier, string propertyName, string propertyValue);
}