using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_Manager : MonoBehaviour {

    public int sceneToLoad;
    public loading load;

    public void sceneLoad(loading load, int sceneToLoad)
    {
        load = this.load;
        sceneToLoad = this.sceneToLoad;
        if (load == loading.loadASync && !SceneManager.GetSceneAt(sceneToLoad).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
        if (load == loading.loadOne && !SceneManager.GetSceneAt(sceneToLoad).isLoaded) 
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        if (load == loading.unload)
        {
            SceneManager.UnloadSceneAsync(sceneToLoad);
        }
    }

    void Awake()
    {

    }
}

public enum loading
{
    none,
    loadOne,
    loadASync,
    unload
}
