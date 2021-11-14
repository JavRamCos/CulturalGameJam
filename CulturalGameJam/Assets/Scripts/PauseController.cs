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
	[SerializeField] private GameObject losePanel;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider vfxSlider;
	[SerializeField] private Image healthOne;
	[SerializeField] private Image healthTwo;
	[SerializeField] private Image healthThree;
	[SerializeField] private Image healthFour;
	[SerializeField] private Image healthFive;
	[SerializeField] private Sprite fullHeart;
	[SerializeField] private Sprite halfHeart;
	[SerializeField] private Sprite emptyHeart;
	private GameObject playerObj;
	private bool onPause;
	private bool canPause;

	public void Awake() {
		if (instance == null) {
			instance = this;
		}
		onPause = false;
		canPause = true;
		musicSlider.maxValue = 1.0f;
		vfxSlider.maxValue = 1.0f;
		musicSlider.value = PlayerPrefs.GetFloat("Music");
		vfxSlider.value = PlayerPrefs.GetFloat("VFX");
		hudPanel.SetActive(true);
		pausePanel.SetActive(false);
		optionsPanel.SetActive(false);
		losePanel.SetActive(false);
	}

	private void Update() {
		playerObj = GameObject.FindGameObjectWithTag("Player");
		PlayerHealth ph = playerObj.GetComponent<PlayerHealth>();
		if(ph != null) {
			int lives = ph.GetHealth();
			ShowPlayerHealth(lives);
        }
		if (Input.GetKeyDown(KeyCode.Escape) && canPause == true) {
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

	public void ShowPlayerHealth(int lives) {
		switch(lives) {
			case 0:
				healthOne.sprite = emptyHeart;
				healthTwo.sprite = emptyHeart;
				healthThree.sprite = emptyHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 1:
				healthOne.sprite = halfHeart;
				healthTwo.sprite = emptyHeart;
				healthThree.sprite = emptyHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 2:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = emptyHeart;
				healthThree.sprite = emptyHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 3:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = halfHeart;
				healthThree.sprite = emptyHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 4:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = emptyHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 5:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = halfHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 6:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = fullHeart;
				healthFour.sprite = emptyHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 7:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = fullHeart;
				healthFour.sprite = halfHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 8:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = fullHeart;
				healthFour.sprite = fullHeart;
				healthFive.sprite = emptyHeart;
				break;
			case 9:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = fullHeart;
				healthFour.sprite = fullHeart;
				healthFive.sprite = halfHeart;
				break;
			default:
				healthOne.sprite = fullHeart;
				healthTwo.sprite = fullHeart;
				healthThree.sprite = fullHeart;
				healthFour.sprite = fullHeart;
				healthFive.sprite = fullHeart;
				break;
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

	public void ShowLosePanel() {
		canPause = false;
		Time.timeScale = 0.0f;
		losePanel.SetActive(true);
		hudPanel.SetActive(false);
		pausePanel.SetActive(false);
		optionsPanel.SetActive(false);
    }

	public void Restart() {
		SceneManager.LoadScene("Scene");
    }

	public void ExitToMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
