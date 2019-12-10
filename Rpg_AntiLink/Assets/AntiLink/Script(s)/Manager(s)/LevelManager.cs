using UnityEngine;
using UnityEngine.SceneManagement;

public enum eSpawnMapPos{Base, Left, Right, Down, Up};

public class LevelManager : Singleton<LevelManager>
{

    [Header("Loading Display")]
    [SerializeField]
    private GameObject m_LoadingScreen;     //The Object to enable/disable when the loading is on.

    [Header("Loading Parameters")]
    [SerializeField]
    private float m_MinLoadingTimeStandard = 3;     //The minimum time the loading need to lapse before disable the windows (paramaters).
    private float m_MinLoadingTime = 3;             //The minimum time (current one) the loading need to lapse before disable the windows.
    private float m_CurrentTime = 0;                //The current time of the loading.
    private bool m_LoadingIsDone = false;           //If the Loading is done or not

    private bool m_LoadingIsOver = false;           //If the loading screen (total loading) is done or not.
    public bool LoadingIsOver                       //Acces from anywhere if the loading is done or not.
    {
        get { return m_LoadingIsOver; }
    }

    private SceneData m_CurrentSceneInLoad;     //The scene data that is currently loading.
    private eSpawnMapPos m_SpawnPos;            //The position where the player spawn.

    protected override void Awake()
    {
        base.Awake();
        InitLevelManager();
    }

    private void Update()
    {
        CheckLoadingTime();
    }

    private void InitLevelManager()
    {
        m_LoadingScreen.SetActive(false);
        SceneManager.sceneLoaded += LoadingIsDone;
    }

    private void CheckLoadingTime()
    {
        if (m_CurrentTime < m_MinLoadingTime)
        {
            m_CurrentTime += Time.deltaTime;
        }
        else if (m_LoadingIsDone)
        {
            m_LoadingIsDone = false;
            FinishLoading();
        }
    }

    ///<summary> Start the loading of the next scene with next position </summary>
    public void LoadScene(SceneData aScene, eSpawnMapPos aSpawn)
    {
        if(Application.CanStreamedLevelBeLoaded(aScene.GetSceneName()))
        {
            m_SpawnPos = aSpawn;
            StartLoading(aScene.GetSceneName());
            m_CurrentSceneInLoad = aScene;
        }
        else
        {
            Debug.LogWarning("There's no scene with the name [" + aScene.GetSceneName() + "] in the Build of Unity!");
        }
    }


    private void StartLoading(string aScene)
    {
        m_LoadingScreen.SetActive(true);
        m_MinLoadingTime = m_MinLoadingTimeStandard;
        m_CurrentTime = 0f;
        m_LoadingIsDone = false;
        m_LoadingIsOver = false;

        SceneManager.LoadScene(aScene);
    }

    private void LoadingIsDone(Scene aScene, LoadSceneMode aMode)
    {
        m_LoadingIsDone = true;
        ManagePool();
    }

    private void FinishLoading()
    {
        GameManager.Instance.RespawnPlayer(m_SpawnPos);
        m_LoadingScreen.SetActive(false);
        m_LoadingIsOver = true;
        ManageMusic();
    }

    private void ManagePool()
    {
        if (m_CurrentSceneInLoad != null)
        {
            switch (m_CurrentSceneInLoad.GetPoolOption())
            {
                case eScenePool.WithPool:
                    {
                        if (m_CurrentSceneInLoad.GetScenePool() == null)
                        {
                            Debug.LogWarning("There's no pool Object in this SceneData you try to load!");
                            PoolManager.Instance.ErasePool();
                            break;
                        }
                        PoolManager.Instance.InitialiseNewPools(m_CurrentSceneInLoad.GetScenePool());
                        break;
                    }
                case eScenePool.WithoutPool:
                    {
                        PoolManager.Instance.ErasePool();
                        break;
                    }
            }
        }
    }

    private void ManageMusic()
    {
        switch (m_CurrentSceneInLoad.GetMusicOption())
        {
            case eSceneMusic.WithMusic:
                {
                    if (m_CurrentSceneInLoad.GetSceneMusic() == null)
                    {
                        Debug.LogWarning("There's no music in this SceneData you try to load!");
                        AudioManager.Instance.SetMusic(null, false);
                        break;
                    }
                    AudioManager.Instance.SetMusic(m_CurrentSceneInLoad.GetSceneMusic(), true);
                    break;
                }
            case eSceneMusic.WithoutMusic:
                {
                    AudioManager.Instance.SetMusic(null, false);
                    break;
                }
            case eSceneMusic.KeepOldMusic:
                {

                    break;
                }
        }
    }
}
