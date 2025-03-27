using UnityEngine;

public class Loader : MonoBehaviour
{

    private ScriptableObject[] allScriptableObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadAllScriptableObjects();
    }

  private void LoadAllScriptableObjects()
    {
        // Load all ScriptableObject assets from the entire Resources folder
        allScriptableObjects = Resources.LoadAll<ScriptableObject>("SOInstance");

        if (allScriptableObjects.Length > 0)
        {
            Debug.Log($"Loaded {allScriptableObjects.Length} ScriptableObject assets.");
            foreach (var obj in allScriptableObjects)
            {
                Debug.Log($"Loaded: {obj.name} (Type: {obj.GetType()})");
            }
        }
        else
        {
            Debug.LogWarning("No ScriptableObjects found in Resources!");
        }
    }


}
