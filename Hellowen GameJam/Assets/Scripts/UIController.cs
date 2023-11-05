using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Sound")]
    public MusicManager musicManager;
    public MusicManager soundManager;
    [Header("UI")]
    public Slider badScoreSlider;
    public Slider goodScoreSlider;
    public Text goodExplanations;
    public Text badExplanations;
    [SerializeField] private GameObject panels;
    [SerializeField] private Fade fade;
    [Header("Player")]
    [SerializeField] private PlayerMove playerMove;

    private void Start()
    {
        Time.timeScale = 1;
        fade.FadeWhite();
        goodExplanations.gameObject.SetActive(false);
        badExplanations.gameObject.SetActive(false);
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
            playerMove.Freeze(true);
            isActivePause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isActivePause == true)
        {
            playerMove.Freeze(false);
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
