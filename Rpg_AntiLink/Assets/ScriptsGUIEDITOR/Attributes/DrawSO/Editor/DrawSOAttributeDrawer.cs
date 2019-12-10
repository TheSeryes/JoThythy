using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DrawSOAttribute))]
public class DrawSOAttributeDrawer : PropertyDrawer
{
    private bool m_IsExpanded;
    private int m_PropertyCount = 1;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            ScriptableObject so = property.objectReferenceValue as ScriptableObject;
            if (so != null)
            {
                GUI.Box(position, "");
                string labelName = (m_IsExpanded ? "▼ " : "▶ ") + label.text;
                GUIContent labelContent = new GUIContent(labelName, label.tooltip);
                EditorGUI.PropertyField(position, property, labelContent, true);
                Rect buttonRect = position;
                buttonRect.height = EditorGUIUtility.singleLineHeight;
                if (GUI.Button(buttonRect, "", GUI.skin.label))
                {
                    m_IsExpanded = !m_IsExpanded;
                }

                SerializedObject serializedObject = new SerializedObject(so);

                m_PropertyCount = 0;

                if (m_IsExpanded)
                {
                    EditorGUI.indentLevel += 2;
                    SerializedProperty props = serializedObject.GetIterator();
                    Rect propRect = position;
                    while (props.NextVisible(true))
                    {
                        
                        if (props.name == "m_Script")
                        {
                            continue;
                        }
                        
                        m_PropertyCount++;
                        propRect.y += EditorGUIUtility.singleLineHeight;
                        EditorGUI.PropertyField(propRect, props, true);
                    }

                    serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(so);
                    EditorGUI.indentLevel -= 2;
                }
                return;
            }
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + m_PropertyCount * EditorGUIUtility.singleLineHeight;
    }

}
