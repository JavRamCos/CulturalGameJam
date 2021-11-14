using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider vfxSlider;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource vfxSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private Image backgroundImg;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        if (!PlayerPrefs.HasKey("Music")) {
            PlayerPrefs.SetFloat("Music", 0.5f);
        }
        if (!PlayerPrefs.HasKey("VFX")) {
            PlayerPrefs.SetFloat("VFX", 0.8f);
        }
        musicSlider.maxValue = 1.0f;
        vfxSlider.maxValue = 1.0f;
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        vfxSlider.value = PlayerPrefs.GetFloat("VFX");
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        backgroundImg.enabled = true;
    }

    private void Update() {
        musicSource.volume = PlayerPrefs.GetFloat("Music");
        vfxSource.volume = PlayerPrefs.GetFloat("VFX");
    }

    private void PlayClickSound() {
        vfxSource.PlayOneShot(clickSound);
    }

    public void PlayGame() {
        PlayClickSound();
    }

    public void OptionsSelected() {
        PlayClickSound();
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMainFromOptions() {
        PlayClickSound();
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        vfxSlider.value = PlayerPrefs.GetFloat("VFX");
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void SaveSettings() {
        PlayClickSound();
        float music = musicSlider.value;
        float vfx = vfxSlider.value;
        PlayerPrefs.SetFloat("Music", music);
        PlayerPrefs.SetFloat("VFX", vfx);
    }

    public void CreditsSelected() {
        PlayClickSound();
        backgroundImg.enabled = false;
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMainFromCredits() {
        PlayClickSound();
        backgroundImg.enabled = true;
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ExitSelected() {
        PlayClickSound();
        Application.Quit();
    }
}
