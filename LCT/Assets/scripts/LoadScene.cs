using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    [SerializeField] private GameObject progressCanvas;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressValue;
    public float FillSpeed = 0.5f;
    AsyncOperation loadingOperation;
    public float loadingProgress;
    public float targetProgress = 0;
    public ParticleSystem particleSys;

    void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
        loadingOperation.allowSceneActivation = false;
    }

    private void Update()
    {
        loadingProgress = loadingOperation.progress;
        IncrementProgress();

        if (progressBar.value < targetProgress)
        {
            progressBar.value += FillSpeed * Time.deltaTime;
            if (!particleSys.isPlaying)
            {
                particleSys.Play();
            }
        }

        loadingText.text = "Loading... " + (Mathf.RoundToInt(progressBar.value * 100)) + "%";
        Debug.Log(progressBar.value);

        if (progressBar.value == 1)
        {
            particleSys.Stop();
            loadingText.text = "Loading complete! Press E";
        }

        if (loadingOperation.progress == 0.9f && Input.GetKeyDown(KeyCode.E))
        {
            loadingOperation.allowSceneActivation = true;
        }
    }

    public void IncrementProgress()
    {
         this.targetProgress = progressBar.value + loadingProgress;
    }
}
