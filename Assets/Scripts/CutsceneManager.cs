using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public Material space;
    public Material sky;

    public Light sun;

    public FadeController fadeController;

    public PostProcessingProfile profile;
    PostProcessingBehaviour ppBehaviour;

    public GameObject Title;

    public AudioSource music;

    public Animator playerAnim;

	// Use this for initialization
	void Start () {
        RenderSettings.skybox = space;
        DynamicGI.UpdateEnvironment();
        ppBehaviour = GetComponent <PostProcessingBehaviour>();
	}

    public void Fade()
    {
        fadeController.FadeIn(false);
    }

    public void Cut()
    {
        RenderSettings.skybox = sky;
        DynamicGI.UpdateEnvironment();
        sun.intensity = 0.3f;
        ppBehaviour.profile = profile;
        fadeController.FadeIn(true);
    }

    public void AnimatePlayer()
    {
        playerAnim.SetTrigger("Action");
    }

    public void TitleScreen()
    {
        Title.SetActive(true);
    }

    public void FadeMusic()
    {
        StartCoroutine(DecreaseVolume());
    }

    IEnumerator DecreaseVolume()
    {
        for (int i = 0; i < 100; i++)
        {
            music.volume -= 0.01f;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void LoadMain()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
