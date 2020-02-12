using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PrefabsDistributor
{
    // Created by Turkenberg - 02/12/2020

    // How many rows the assets shall be spanned out ?
    public static int numberOfRows = 4;

    // Spacing between the prefabs
    public static float interval = 2;

    private static GameObject[] prefabs;
    private static Vector3[][] positions;
    private static int numberOfColumns;

    //public static bool layOnStart;
    //public static bool layDone;

    [MenuItem("Assets/Lay prefabs in scene")]
    static void LayAllAssets()
    {

        //prefabs = Resources.LoadAll<GameObject>("Prefabs");

        prefabs = ArrayOfPrefabsAtPath(AssetDatabase.GetAssetPath(Selection.activeObject));

        Debug.Log("[Prefabs Distributor] Number of prefabs found: " + prefabs.Length);

        if (prefabs.Length == 0)
        {
            Debug.Log("[Prefabs Distributor] no prefabs found in Resources/Prefabs folder");
            return;
        }

        positions = ComputePositions();

        GameObject parentGo = new GameObject();
        parentGo.transform.position = Vector3.zero;
        parentGo.transform.rotation = Quaternion.identity;
        parentGo.name = "New Prefabs Layout";

        //Object.Instantiate(parentGo);

        LayObjects(parentGo.transform);
    }

    [MenuItem("Assets/Lay prefabs in scene", true)]
    static bool LayAllAssetsValidation()
    {
        string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
        FileAttributes attr = File.GetAttributes(filePath);
        return ((attr & FileAttributes.Directory) == FileAttributes.Directory && (AssetDatabase.FindAssets("t:prefab", new string[] { filePath }).Length > 0));
    }

    static GameObject[] ArrayOfPrefabsAtPath(string selectedPath)
    {
        string[] assetsPaths = AssetDatabase.FindAssets("t:prefab", new string[] { selectedPath });

        List<GameObject> results = new List<GameObject>();

        foreach (string assetPath in assetsPaths)
        {
             results.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assetPath)));
        }

        return results.ToArray();
    }

    static Vector3[][] ComputePositions()
    {
        Vector3[][] result;

        numberOfColumns = Mathf.CeilToInt((float)prefabs.Length / (float)numberOfRows);

        // we now know that grid is numberOfColumns * numberOfRows ; centered on asset position
        // Initialize jagged arrray:
        result = new Vector3[numberOfRows][];

        for (int i = 0; i < numberOfRows - 1; i++)
        {
            result[i] = new Vector3[numberOfColumns];
        }

        result[numberOfRows - 1] = new Vector3[prefabs.Length % numberOfColumns];

        Vector3 startPosition = 
            numberOfRows * interval * 0.5f * Vector3.forward +
            numberOfColumns * interval * 0.5f * Vector3.right;

        // All positions[] are now initialized with the start position
        // Let's go through all and give them the proper coordinates

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < result[i].Length; j++)
            {
                result[i][j] = startPosition +
                    Vector3.forward * interval * i -
                    Vector3.right * interval * j;
            }
        }

        return result;
    }

    static void LayObjects(Transform parent)
    {
        int index = 0;
        GameObject currentGameObject = prefabs[index];

        foreach (Vector3[] positionArray in positions)
        {
            foreach (Vector3 position in positionArray)
            {
                Object.Instantiate(currentGameObject, position, Quaternion.identity, parent);
                index++;
                if (index < prefabs.Length)
                {
                    currentGameObject = prefabs[index];
                }
            }
        }

        //layDone = true;
    }
}
