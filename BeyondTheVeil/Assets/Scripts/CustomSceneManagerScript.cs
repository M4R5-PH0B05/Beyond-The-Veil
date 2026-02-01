using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    public string m_sceneName;
    public Camera m_currentCamera;
    public Vector3 m_level1SpawnPosition = new Vector3(0, 0, 0);
    private Coroutine m_CR_LoadLevelRunning;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //initialising member variables
        m_player = GameObject.Find("Player");
        m_sceneName = SceneManager.GetActiveScene().name;
        //loads player into scene if player should be in scene
        m_currentCamera = Camera.main;
        CheckIfPlayerShouldBeActive();
    }

    /// <summary>
    /// Based on scene name enables or disables main player instance at the start of the scene
    /// </summary>
    void CheckIfPlayerShouldBeActive()
    {
        m_sceneName = SceneManager.GetActiveScene().name;
        if (m_sceneName == "Level Select")
        {
            m_player.SetActive(false);
        }
        else
        {
            m_player.SetActive(true);
        }
    }

    /// <summary>
    /// Called from other scripts to start the coroutine responsible for loading scenes 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="spawnPosition"></param>
    public void StartSwapSceneCoroutine(string sceneName, Vector3 spawnPosition)//public function to call when loading a scene from another script
    {
        if (m_CR_LoadLevelRunning == null)
        {
            m_CR_LoadLevelRunning = StartCoroutine(CR_LoadScene(sceneName, spawnPosition));
        }
    }

    /// <summary>
    /// Loads specificially Level 1 from a button 
    /// </summary>
    public void LoadLevel1()
    {
        if (m_CR_LoadLevelRunning == null)
        {
            m_CR_LoadLevelRunning = StartCoroutine(CR_LoadScene("Level 1", m_level1SpawnPosition));
        }
    }

    public void LoadLevel2()
    {
        if (m_CR_LoadLevelRunning == null)
        {
            m_CR_LoadLevelRunning = StartCoroutine(CR_LoadScene("Level 2", m_level1SpawnPosition));
        }
    }

    /// <summary>
    /// Async loads given scene while performing other functions during scene loading
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    IEnumerator CR_LoadScene(string sceneName, Vector3 spawnPosition)
    {
        var titleMusicObj = GameObject.Find("TitleMusic");
        SceneMusic titleMusic = titleMusicObj ? titleMusicObj.GetComponent<SceneMusic>() : null;

        if (ScreenFader.Instance != null)
        {
            // start music fade (if it exists) and fade to black
            if (titleMusic != null)
                titleMusic.FadeOut();

            yield return ScreenFader.Instance.FadeTo(1f);

            if (titleMusic != null)
                yield return new WaitForSecondsRealtime(titleMusic.FadeOutTime);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        // Re-find player in the new scene
        m_player = GameObject.Find("Player");
        if (m_player != null)
            m_player.transform.position = spawnPosition;

        CheckIfPlayerShouldBeActive();

        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeTo(0f);

        m_CR_LoadLevelRunning = null;
    }
}