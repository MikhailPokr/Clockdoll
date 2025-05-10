using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

public class AutoScene
{
    [InitializeOnLoad]
    public static class AutoLoadScene
    {
        private const string LastSceneKey = "LastActiveScenePath";

        static AutoLoadScene()
        {
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }

        private static void LoadDefaultScene(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:

                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                    string currentScenePath = EditorSceneManager.GetActiveScene().path;
                    EditorPrefs.SetString(LastSceneKey, currentScenePath);

                    EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");

                    break;
                case PlayModeStateChange.EnteredEditMode:

                    string lastScenePath = EditorPrefs.GetString(LastSceneKey, "LastActiveScenePath");

                    EditorSceneManager.OpenScene(lastScenePath);
                    break;
            }
        }
    }
}
