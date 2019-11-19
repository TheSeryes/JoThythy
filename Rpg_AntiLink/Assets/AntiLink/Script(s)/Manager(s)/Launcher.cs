using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private SceneData m_Scene;

    private void Start()
    {
        LevelManager.Instance.LoadScene(m_Scene, eSpawnMapPos.Base);
    }
}
