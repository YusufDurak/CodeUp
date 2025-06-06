using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneInitializer : EditorWindow
{
    [MenuItem("Window/CodeUp/Setup New Scene")]
    [MenuItem("Assets/Create/CodeUp/Setup New Scene")]
    public static void CreateNewScene()
    {
        // Create a new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Create a GameObject for our SceneSetup
        GameObject setupObj = new GameObject("SceneSetup");
        SceneSetup setup = setupObj.AddComponent<SceneSetup>();
        
        // Force the setup to run immediately
        setup.SendMessage("Awake", SendMessageOptions.DontRequireReceiver);

        // Save the scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainScene.unity");
        Debug.Log("Scene created successfully!");
    }

    [MenuItem("Window/CodeUp/Open Setup Window")]
    public static void ShowWindow()
    {
        GetWindow<SceneInitializer>("CodeUp Setup");
    }

    private void OnGUI()
    {
        GUILayout.Label("CodeUp Scene Setup", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Create New Scene"))
        {
            CreateNewScene();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("This will create a new scene with all the necessary UI elements for CodeUp.", MessageType.Info);
    }
} 