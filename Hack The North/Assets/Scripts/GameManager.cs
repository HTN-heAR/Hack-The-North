using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gm = null;
    public bool loading;
    public Animator transition;
    public float sceneTransitionTime;
    public bool loadingNew;
    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        int noteSets;
        // Check number of saved note sets
        if (PlayerPrefs.HasKey("NoteSets"))
        {
            noteSets = PlayerPrefs.GetInt("NoteSets");
        }
        else
        {
            PlayerPrefs.SetInt("NoteSets", 0);
            noteSets = 0;
        }

        // Generate list of notes
        for (int i = 0; i < noteSets; i++)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called by menu to trigger game start
    public void Load(string scene)
    {
        if (loading) return;
        loading = true;
        StartCoroutine(LoadScene(scene));
    }

    // probably use this for menu later for animations but idk might be useful
    public void LoadWithDelay(string scene, float time)
    {
        if (loading) return;

        loading = true;
        StartCoroutine(Delay(scene, time));
    }
    // Delay coroutine for load with delay
    IEnumerator Delay(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(LoadScene(scene));
    }
    public void LoadInstant(string sceneName)
    {
        StartCoroutine(LoadSceneInstant(sceneName));
    }

    public IEnumerator LoadSceneInstant(string sceneName)
    {
        if (loading) yield break;
        loading = true;
        transition.SetTrigger("Cut");

        AudioListener.volume = 0;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);

        yield return null;
        gm = this; // Reset self as GameManager instance

        AudioListener.volume = 1;
        loading = false;
    }

    // Actually loads scene
    public IEnumerator LoadScene(string sceneName)
    {
        if (transition != null) transition.Play("Fade Out"); // Start transitioning scene out


        yield return new WaitForSeconds(sceneTransitionTime); // Wait for transition

        // Start loading scene
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        load.allowSceneActivation = false;
        while (!load.isDone) // Could make a loading bar here :shrug:
        {
            if (load.progress >= 0.9f)
            {
                load.allowSceneActivation = true;
            }

            yield return null;
        }
        load.allowSceneActivation = true;

        yield return null;

        // if (sceneName != "Main Menu") OnLevelLoad();

        //  Set volume
        // AudioListener.volume = volume;


        gm = this; // Reset self as GameManager instance

        if (transition != null) transition.Play("Fade In"); // Start transitioning scene back


        yield return new WaitForSeconds(sceneTransitionTime); // Wait for transition
        loading = false;
    }
}
