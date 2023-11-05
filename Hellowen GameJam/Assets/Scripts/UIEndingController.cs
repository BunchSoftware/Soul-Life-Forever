using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndingController : MonoBehaviour
{
    [Header("Sound")]
    public MusicManager musicManager;
    public MusicManager soundManager;
    [SerializeField] private GameObject panels;
    [SerializeField] private Fade fade;

    private void Start()
    {
        Time.timeScale = 1;
        fade.FadeWhite();
        musicManager.gameObject.SetActive(true);
        musicManager.SoundResurrection(1f);
        soundManager.gameObject.SetActive(true);
    }

    private bool isActivePause = false;

    private void Update()
    {
        if (panels != null && Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < panels.transform.childCount; i++)
            {
                if (panels.transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    panels.transform.GetChild(i).gameObject.SetActive(false);
                }
                else if (panels.transform.GetChild(i).gameObject.tag == "Pause")
                {
                    panels.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isActivePause == false)
        {
            isActivePause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isActivePause == true)
        {
            isActivePause = false;
        }
    }

    public void LoadLevel(int buildIndex)
    {
        fade.currentIndexScene = buildIndex;
        musicManager.SoundDecay(1f);
        fade.FadeBlack();
    }
    public void ChangeTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }
    public void SetActiveUI(GameObject gameObject)
    {
        if (gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ApllicationQuit()
    {
        Application.Quit();
    }
}
