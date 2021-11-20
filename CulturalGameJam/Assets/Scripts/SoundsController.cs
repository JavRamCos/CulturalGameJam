using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
	public static SoundsController instance;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource vfxSource;
	[SerializeField] private AudioClip jumpingSound;
	[SerializeField] private AudioClip throwingSound;
	[SerializeField] private AudioClip dashSound;
	[SerializeField] private AudioClip playerHitSound;
	[SerializeField] private AudioClip enemyHitSound;
	[SerializeField] private AudioClip powerUpSound;

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
			case 1:
				vfxSource.PlayOneShot(jumpingSound);
				break;
			case 2:
				vfxSource.PlayOneShot(throwingSound);
				break;
			case 3:
				vfxSource.PlayOneShot(dashSound);
				break;
			case 4:
				vfxSource.PlayOneShot(playerHitSound);
				break;
			case 5:
				vfxSource.PlayOneShot(enemyHitSound);
				break;
			case 6:
				vfxSource.PlayOneShot(powerUpSound);
				break;
			default:
				//vfxSource.PlayOneShot();
				break;
		}
	}
}
