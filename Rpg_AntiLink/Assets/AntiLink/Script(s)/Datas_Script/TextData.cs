using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Data/TextData", fileName = "New TextData", order = 2)]
public class TextData : ScriptableObject
{
    [Serializable]
    public struct Talking
    {
        public int m_Choice;
        public string m_TextToShow;
        public string m_ButtonA;
        public string m_ButtonB;
        public List <int> m_NextDialogue;
    }

    [SerializeField]
    private List<Talking> m_TalkingStructs = new List<Talking>();

    public List<Talking> GetTalkingStructs
    {
        get{return m_TalkingStructs;}
    }
    

    
}
