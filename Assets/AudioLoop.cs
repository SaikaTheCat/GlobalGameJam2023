using UnityEngine;

public class AudioLoop : MonoBehaviour
{
	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
	}
}
