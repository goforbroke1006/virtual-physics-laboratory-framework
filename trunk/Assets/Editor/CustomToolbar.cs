using UnityEditor;
using UnityEngine;
using System.Collections;

public class CustomToolbar : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject SpherePrefab;
    public GameObject CylinderPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Graphics

    private const string PATH_TO_PREFABS_FOLDER = "Assets/PhysicsObject/Prefab/";

    [MenuItem("VPL Library/Graphics (Графика)/Primitives (Примитивы)/Cube (Куб)")]
    public static void CreateCube()
    {
        EditorUtility.InstantiatePrefab(
            AssetDatabase.LoadAssetAtPath(PATH_TO_PREFABS_FOLDER + "Cube.prefab",
            typeof(PhysComponent)));
    }

    [MenuItem("VPL Library/Graphics (Графика)/Primitives (Примитивы)/Sphere (Сфера)")]
    public static void CreateSphere()
    {
        EditorUtility.InstantiatePrefab(
            AssetDatabase.LoadAssetAtPath(PATH_TO_PREFABS_FOLDER + "Sphere.prefab",
            typeof(PhysComponent)));
    }

    [MenuItem("VPL Library/Graphics (Графика)/Primitives (Примитивы)/Cylinder (Цилиндр)")]
    public static void CreateCylinder()
    {
        EditorUtility.InstantiatePrefab(
            AssetDatabase.LoadAssetAtPath(PATH_TO_PREFABS_FOLDER + "Cylinder.prefab",
            typeof(PhysComponent)));
    }

    [MenuItem("VPL Library/Graphics (Графика)/Material point (Материальная точка)")]
    public static void CreateMaterialPoint()
    {
    }

    [MenuItem("VPL Library/Graphics (Графика)/Weight 100g (Груз 100г)")]
    public static void CreateWeight100()
    {
    }

    #endregion

    #region Behaviors

    [MenuItem("VPL Library/Behavior (Поведение)/Velocity (Скорость)")]
    public static void AddVelocity()
    {
    }

    [MenuItem("VPL Library/Behavior (Поведение)/Acceleration (Ускорение)")]
    public static void AddAcceleration()
    {
    }

    [MenuItem("VPL Library/Behavior (Поведение)/Friction coefitient (Коефициент трения)")]
    public static void AddCoefitientFriction()
    {
    }

    #endregion
}
