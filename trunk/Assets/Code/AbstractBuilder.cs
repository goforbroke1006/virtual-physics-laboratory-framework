using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class AbstractBuilder
{
    protected List<PhysicsObject> PhysicsObjects;
    protected List<Formula> Formulas;

    public AbstractBuilder(List<PhysicsObject> physicsObjects, List<Formula> formulas)
    {
        PhysicsObjects = physicsObjects;
        Formulas = formulas;
    }

    public abstract string GetCode_Labwork(LabworkConfig config);

    public abstract string GetCode_DefineVariableCode();
    public abstract string GetCode_DefineFieldVariableCode();
    public abstract string GetCode_PastedFormulasCode();
    public abstract string GetCode_FillFieldWithVariableCode();
    public abstract string GetCode_ReturnFieldVariableCode();
}