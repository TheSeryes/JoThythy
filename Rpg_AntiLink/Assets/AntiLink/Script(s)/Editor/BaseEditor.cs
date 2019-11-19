
using UnityEngine;
using UnityEditor;

namespace ThityyTools
{
    public class BaseEditor : EditorWindow
    {

        private const string m_CompanyName = "Thityy Tools®";
        private Color32 m_CompanyColor01 = new Color32(35, 208, 185, 255);
        private Color32 m_CompanyColor02 = new Color32(31, 31, 31, 255);

        #region style variable

        protected Font m_CustomFont;
        protected GUIStyle m_TitleStyle;
        protected GUIStyle m_TitleCenterStyle;
        protected GUIStyle m_SubtitleStyle;
        protected GUIStyle m_RichTxtStyle;
        protected GUIStyle m_WarningStyle;
        protected GUIStyle m_HelpBoxStyle;

        #endregion

        #region style function

        protected virtual void OnGUI()
        {
            CreateStyle();
            DrawBanner();
        }

        private void CreateStyle()
    {
        m_CustomFont = Resources.Load("editorFont") as Font;
        m_TitleStyle = new GUIStyle(GUI.skin.label);
        m_TitleStyle.font = m_CustomFont;
        m_TitleStyle.fontSize = 14;
        m_TitleStyle.normal.textColor = Color.white;
        m_TitleStyle.alignment = TextAnchor.MiddleLeft;

        m_TitleCenterStyle = new GUIStyle(GUI.skin.label);
        m_TitleCenterStyle.font = m_CustomFont;
        m_TitleCenterStyle.fontSize = 14;
        m_TitleCenterStyle.normal.textColor = Color.white;
        m_TitleCenterStyle.alignment = TextAnchor.MiddleCenter;

        m_SubtitleStyle = new GUIStyle(GUI.skin.label);
        m_SubtitleStyle.fontSize = 12;
        m_SubtitleStyle.normal.textColor = m_CompanyColor02;
        m_SubtitleStyle.alignment = TextAnchor.MiddleLeft;
        m_SubtitleStyle.richText = true;

        m_RichTxtStyle = new GUIStyle(GUI.skin.label);
        m_RichTxtStyle.richText = true;

        m_WarningStyle = new GUIStyle();
        m_WarningStyle = GUI.skin.label;
        m_WarningStyle.richText = true;
        m_WarningStyle.fontSize = 12;
        m_WarningStyle.alignment = TextAnchor.MiddleCenter;

        m_HelpBoxStyle = GUI.skin.GetStyle("HelpBox");
        m_HelpBoxStyle.richText = true;
    }

    protected void DrawSeparator(bool aMain)
    {
        GUILayout.Space(10f);
        if(aMain)
            GUI.color = m_CompanyColor01;
        else
            GUI.color = m_CompanyColor02;
        EditorGUILayout.BeginVertical(GUI.skin.textField);
        EditorGUILayout.EndVertical();
        GUILayout.Space(10f);
        GUI.color = Color.white;
    }

    ///<summary> Write a title with a tools infos</summary>
    protected void DrawTitleWithTips(string aTitle, string aToolInfos)
    {
        GUI.color = m_CompanyColor02;
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = Color.white;
        DrawTitle(aTitle);
        DrawToolsInfo(aToolInfos, false, false);
        EditorGUILayout.EndVertical();
        DrawSeparator(true);
    }

    ///<summary> Write a title </summary>
    protected void DrawTitle(string aTitle)
    {
        GUI.color = m_CompanyColor01;
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = m_CompanyColor02;
        EditorGUILayout.LabelField(aTitle, m_TitleCenterStyle);
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
        m_TitleCenterStyle.normal.textColor = Color.white;
    }

    protected void DrawSubtitle(string aText)
    {
        EditorGUILayout.LabelField("<i>" + aText + "</i>", m_SubtitleStyle);
    }

    protected void DrawToolsInfo(string aText, bool aBold, bool aItalic)
    {
        GUI.color = m_CompanyColor02;
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = Color.white;

        if (aBold && !aItalic)
            EditorGUILayout.HelpBox("<b>" + aText + "</b>", MessageType.Info);
        else if (aItalic && !aBold)
            EditorGUILayout.HelpBox("<i>" + aText + "</i>", MessageType.Info);
        else if (aItalic && aBold)
            EditorGUILayout.HelpBox("<b><i>" + aText + "</i></b>", MessageType.Info);
        else
            EditorGUILayout.HelpBox(aText, MessageType.Info);

        EditorGUILayout.EndVertical();
    }

    private void DrawBanner()
    {
        GUI.color = Color.white;
        EditorGUILayout.BeginVertical(GUI.skin.button);
        GUI.color = m_CompanyColor02;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.color = m_CompanyColor01;
        EditorGUILayout.LabelField(m_CompanyName, m_TitleCenterStyle);
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }

    protected void DrawWarning(string aText)
    {
        EditorGUILayout.TextArea(aText, m_WarningStyle);
    }

    #endregion
    }

}

