using UnityEngine;
using System.Collections;

public class BeanManager : MonoBehaviour
{
    void Start()
    {
    }

    public static PhysicsObjectsManager GetPhysicsObjectsManager()
    {
        return (PhysicsObjectsManager)FindObjectOfType(typeof(PhysicsObjectsManager));
    }

    public static WebConnector GetWebConnector()
    {
        return (WebConnector)FindObjectOfType(typeof(WebConnector));
    }

    public static ConfigurationManager GetConfigurationManager()
    {
        return (ConfigurationManager)FindObjectOfType(typeof(ConfigurationManager));
    }

    public static MathematicsModelView GetMathematicsModelView()
    {
        return (MathematicsModelView)FindObjectOfType(typeof(MathematicsModelView));
    }

    public static LabPlayer GetLabPlayer()
    {
        return (LabPlayer)FindObjectOfType(typeof(LabPlayer));
    }

    public static OutputConsole GetOutputConsole()
    {
        return (OutputConsole)FindObjectOfType(typeof(OutputConsole));
    }

    public static MainGui GetMainGui()
    {
        return (MainGui)FindObjectOfType(typeof(MainGui));
    }

    private static MapleBuilder _mapleBuilder;
    public static MapleBuilder GetMapleBuilder()
    {
        return _mapleBuilder ?? (_mapleBuilder = new MapleBuilder());
    }

    private static MapleParser _mapleParser;
    public static MapleParser GetMapleParser()
    {
        return _mapleParser ?? (_mapleParser = new MapleParser(PhysicsObjectsManager.GetPhysicsObjects()));
    }
}
