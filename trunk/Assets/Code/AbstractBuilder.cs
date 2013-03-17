using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AbstractBuilder
{
    public AbstractBuilder() { }

    public abstract string GetCode_Labwork(LabworkConfig config, List<PhysicsObject> physicsObjects);

    public abstract string GetCode_DefineAdditionalVarsCode(string additionalVars);

    public abstract string GetCode_DefineVariableCode(List<PhysicsObject> physicsObjects);
    public abstract string GetCode_DefineFieldVariableCode(List<PhysicsObject> physicsObjects);
    public abstract string GetCode_PastedFormulasCode(List<PhysicsObject> physicsObjects);
    public abstract string GetCode_FillFieldWithVariableCode(List<PhysicsObject> physicsObjects);
    public abstract string GetCode_ReturnFieldVariableCode(List<PhysicsObject> physicsObjects);
}