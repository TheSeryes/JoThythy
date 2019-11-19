using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ThityyTools;

public class ThityyToolsBar : BaseEditor
{
    #region Enum
    private enum eSceneToStart { currentScene, normalScene }
    #endregion

    #region Variable

    private eSceneToStart m_SceneToStart;
    private Texture m_DocTex;
    private Texture m_AppTex;
    private Texture m_SceneTex;

    #endregion

    [MenuItem("ThityyTools/ManagerToolsBar")]
    public static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(ThityyToolsBar));
    }

    protected override void OnGUI()
    {
        base.OnGUI();
        GetTex();
        ShowGUI();
    }

    private void GetTex()
    {
        m_DocTex = Resources.Load("DocumentationIcon") as Texture;
        m_AppTex = Resources.Load("AppLauncherIcon") as Texture;
        m_SceneTex = Resources.Load("DocumentationIcon") as Texture;
    }

    private void ShowGUI()
    {
        if (GUI.Button(new Rect(3, 35, 32, 32), m_DocTex))
        {

        }
        if (GUI.Button(new Rect(37, 35, 32, 32), m_AppTex))
        {

        }

        switch (m_SceneToStart)
        {
            case eSceneToStart.currentScene:
                {
                    if (GUI.Button(new Rect(69, 35, 32, 32), m_AppTex))
                    {
                        m_SceneToStart = eSceneToStart.normalScene;
                    }
                    break;
                }
            case eSceneToStart.normalScene:
                {
                    if (GUI.Button(new Rect(69, 35, 32, 32), m_DocTex))
                    {
                        m_SceneToStart = eSceneToStart.currentScene;
                    }
                    break;
                }
        }
        GUILayout.Space(25f);
        DrawSeparator(true);
    }
}
