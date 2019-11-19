using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneData))]
public class SceneData_Editor : BaseCustomEditor
{

    private SerializedProperty m_SceneObj;
    private SerializedProperty m_SceneMusic;
    private SerializedProperty m_ScenePool;
    private SerializedProperty m_MusicOption;
    private SerializedProperty m_PoolOption;

    private SerializedProperty m_DownSpawn;
    private SerializedProperty m_RightSpawn;
    private SerializedProperty m_UpSpawn;
    private SerializedProperty m_LeftSpawn;

    private SerializedProperty m_BaseSpawn;


    private string m_OldPath = "";

    public override void OnInspectorGUI()
    {
        base.OnGUI();
        GetDatas();
        ShowGUI();
        UpdateProperty();
    }

    private void GetDatas()
    {
        GetData(ref m_SceneObj, nameof(m_SceneObj));
        GetData(ref m_SceneMusic, nameof(m_SceneMusic));
        GetData(ref m_ScenePool, nameof(m_ScenePool));
        GetData(ref m_MusicOption, nameof(m_MusicOption));
        GetData(ref m_PoolOption, nameof(m_PoolOption));

        GetData(ref m_DownSpawn, nameof(m_DownSpawn));
        GetData(ref m_RightSpawn, nameof(m_RightSpawn));
        GetData(ref m_UpSpawn, nameof(m_UpSpawn));
        GetData(ref m_LeftSpawn, nameof(m_LeftSpawn));

        GetData(ref m_BaseSpawn, nameof(m_BaseSpawn));
    }

    private void ShowGUI()
    {
        DrawTitleWithTips("Scene Data", "Give all informations needed for the LevelManager to load the scene.");

        EditorGUILayout.BeginVertical(GUI.skin.button);
        DrawTitle("setting(s)");

        //---------------
        
        DrawSubtitle("Scene(s) Setting(s)");
        EditorGUILayout.PropertyField(m_SceneObj);
        if(m_OldPath != AssetDatabase.GetAssetPath(m_SceneObj.objectReferenceValue) && m_SceneObj.serializedObject != null)
        {
            TestScene();
        }

        //---------------

        DrawSeparator(false);

        //---------------
        
        DrawSubtitle("Music(s) Setting(s)");
        EditorGUILayout.PropertyField(m_MusicOption);
        if(m_MusicOption.enumValueIndex == (int)eSceneMusic.WithMusic)
        {
            EditorGUILayout.PropertyField(m_SceneMusic);
        }
        
        //---------------

        DrawSeparator(false);

        //---------------
        
        DrawSubtitle("Pool(s) Setting(s)");
        EditorGUILayout.PropertyField(m_PoolOption);
        if(m_PoolOption.enumValueIndex == (int)eScenePool.WithPool)
        {
            EditorGUILayout.PropertyField(m_ScenePool);
        }

        //---------------
        
        EditorGUILayout.EndVertical();
    }

    ///<summary> Check if the scene exist in the Build setting </summary>
    private void TestScene()
    {
        m_OldPath = AssetDatabase.GetAssetPath(m_SceneObj.objectReferenceValue);
        for(int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if(EditorBuildSettings.scenes[i].path == AssetDatabase.GetAssetPath(m_SceneObj.objectReferenceValue))
            {
                return;
            }
        }
        ShowWarningScene("The scene '" + m_SceneObj.objectReferenceValue.name + "' isn't in the Build Setting!");
    }

    ///<summary> Show to the player if the scene exist in the build setting and let him add it </summary>
    private void ShowWarningScene(string aWarningText)
    {
        bool choose = EditorUtility.DisplayDialog("WARNING!", aWarningText, "Add it", "Cancel");
        if(choose)
        {
            EditorBuildSettingsScene[] aScenes =  EditorBuildSettings.scenes;
            AddScene();
        }
        else
        {
            m_SceneObj.objectReferenceValue = null;
            m_OldPath = "";
        }
    }

    ///<summary> Add the new scene in the build setting </summary>
    private void AddScene()
    {
        List<EditorBuildSettingsScene> scenesValue = new List<EditorBuildSettingsScene>();
        for(int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            scenesValue.Add(EditorBuildSettings.scenes[i]);
        }

        scenesValue.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(m_SceneObj.objectReferenceValue), true));

        EditorBuildSettings.scenes = scenesValue.ToArray();
        EditorUtility.DisplayDialog("Scene Added", m_SceneObj.objectReferenceValue.name + " as been add in the build setting.", "ok");
    }

}
