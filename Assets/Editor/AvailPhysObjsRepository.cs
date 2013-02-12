using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class AvailPhysObjsRepository
{
    private const string PATH_TO_PREFABS_FOLDER = "Assets/LabTools/Prefabs";
    private readonly List<string> _physObjects = new List<string>();

    private AvailPhysObjsRepository()
    {
        _physObjects.Add("Cube");
        _physObjects.Add("Cylinder");
        _physObjects.Add("Sphere");
        _physObjects.Add("Weight 1kg");
        _physObjects.Add("Weight 100g");
        _physObjects.Add("Weight 50g");
    }

    private static AvailPhysObjsRepository _instance = null;

    public static AvailPhysObjsRepository Get()
    {
        return _instance ?? (_instance = new AvailPhysObjsRepository());
    }

    public List<string> GetListPhysObjects()
    {
        return _physObjects;
    }

    public void CreatePhysObjectByPrefabName(string name, string id)
    {
        string path = string.Format("{0}/{1}.prefab", PATH_TO_PREFABS_FOLDER, name);
        GameObject gameObj = (GameObject)Object.Instantiate(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        gameObj.GetComponent<PhysicsObject>().Identifier = id;
    }
}
