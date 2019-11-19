using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum eSceneMusic{WithMusic, WithoutMusic, KeepOldMusic};
public enum eScenePool{WithPool, WithoutPool, KeepOldPool};

[CreateAssetMenu(menuName="Data/SceneData", fileName="new SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField]
    private SceneAsset m_SceneObj;

    [SerializeField]
    private AudioClip m_SceneMusic;
    [SerializeField]
    private eSceneMusic m_MusicOption;

    [SerializeField]
    private PoolObjData m_ScenePool;
    [SerializeField]
    private eScenePool m_PoolOption;



    public string GetSceneName()
    {
        return m_SceneObj.name;
    }

    public AudioClip GetSceneMusic()
    {
        return m_SceneMusic;
    }

    public PoolObjData GetScenePool()
    {
        return m_ScenePool;
    }

    public eSceneMusic GetMusicOption()
    {
        return m_MusicOption;
    }

    public eScenePool GetPoolOption()
    {
        return m_PoolOption;
    }

}
