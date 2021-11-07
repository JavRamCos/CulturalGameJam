using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
	public static SoundsController instance;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource vfxSource;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	private void Update() {
		musicSource.volume = PlayerPrefs.GetFloat("Music");
		vfxSource.volume = PlayerPrefs.GetFloat("VFX");
	}

	public void PlaySound(int sound) {
		switch (sound) {
			default:
				//vfxSource.PlayOneShot();
				break;
		}
	}
}
