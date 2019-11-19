using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SceneData m_SceneData;
    [SerializeField]
    private AudioClip m_AudioClip;
    

    
    public void StartGame()
    {
        AudioManager.Instance.Play2DSFX(m_AudioClip,Vector2.zero);
        LevelManager.Instance.LoadScene(m_SceneData,eSpawnMapPos.Base);
    }

}
