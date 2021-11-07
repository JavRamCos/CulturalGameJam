using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	public static PauseController instance;

	[SerializeField] private GameObject hudPanel;
	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject optionsPanel;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider vfxSlider;
	private bool onPause;

	public void Awake() {
		if (instance == null) {
			instance = this;
		}
		onPause = false;
		musicSlider.maxValue = 1.0f;
		vfxSlider.maxValue = 1.0f;
		musicSlider.value = PlayerPrefs.GetFloat("Music");
		vfxSlider.value = PlayerPrefs.GetFloat("VFX");
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (onPause) {
				onPause = false;
				ResumeGameplay();
			} else {
				onPause = true;
				Time.timeScale = 0.0f;
				hudPanel.SetActive(false);
				pausePanel.SetActive(true);
			}
		}
	}

	public void ResumeGameplay() {
		pausePanel.SetActive(false);
		optionsPanel.SetActive(false);
		hudPanel.SetActive(true);
		Time.timeScale = 1.0f;
	}

	public void OptionsSelected() {
		pausePanel.SetActive(false);
		optionsPanel.SetActive(true);
	}

	public void BackToPause() {
		musicSlider.value = PlayerPrefs.GetFloat("Music");
		vfxSlider.value = PlayerPrefs.GetFloat("VFX");
		optionsPanel.SetActive(false);
		pausePanel.SetActive(true);
	}

	public void SaveSettings() {
		float music = musicSlider.value;
		float vfx = vfxSlider.value;
		PlayerPrefs.SetFloat("Music", music);
		PlayerPrefs.SetFloat("VFX", vfx);
	}

	public void ExitToMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
