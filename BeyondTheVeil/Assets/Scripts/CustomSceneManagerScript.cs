using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    public string m_sceneName;
    public Camera m_currentCamera;
    public Vector3 m_level1SpawnPosition = new Vector3(0,0,0);

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //initialising member variables
        m_player = GameObject.Find("Player");
        m_sceneName = SceneManager.GetActiveScene().name;
        //loads player into scene if player should be in scene
        if (m_sceneName == "Level Select")
        {
            m_player.SetActive(false);
        }
        else
        {
            m_player.SetActive(true);
        }
        m_currentCamera = Camera.main;
    }


    public void StartSwapSceneCoroutine(string sceneName, Vector3 spawnPosition)//public function to call when loading a scene from another script
    {
        StartCoroutine(CR_LoadScene(sceneName, spawnPosition));
    }

    public void LoadLevel1()
    { 
        StartCoroutine(CR_LoadScene("Level 1", m_level1SpawnPosition));
    }

    IEnumerator CR_LoadScene(string sceneName, Vector3 spawnPosition)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)//doesnt finish loading scene until certain actions are done for smooth transitions
        {
            ;
        }
        m_player.transform.position = spawnPosition;
        yield return null;
    }
}
