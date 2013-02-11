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

    public abstract string GetLabworkCode(LabworkConfig config);

    public abstract string GetDefineVariableCode();
    public abstract string GetDefineFieldVariableCode();
    public abstract string GetPastedFormulasCode();
    public abstract string GetFillFieldWithVariableCode();
    public abstract string GetReturnFieldVariableCode();
}