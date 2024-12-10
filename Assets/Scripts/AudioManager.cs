using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("-Audio Source-")]
	[SerializeField] AudioSource musicSource;
	[SerializeField] AudioSource SFXSource;

	[Header("-Audio Clip-")]
	public AudioClip background;
	public AudioClip shoot;
	public AudioClip hit;
	public AudioClip hitenemy;
	public AudioClip footstep;

	private void Start()
	{
		musicSource.clip = background;
		musicSource.Play();
	}
}
