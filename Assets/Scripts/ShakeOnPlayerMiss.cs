using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnPlayerMiss : MonoBehaviour
{
	public float hitAmplitudeGain = 2, hitFrequencyGain = 2, shakeTime = 1;
	CinemachineVirtualCamera vcam;
	CinemachineBasicMultiChannelPerlin noisePerlin;
	bool isShaking = false;
	float shakeTimeElapsed = 0f;

	void Awake()
	{
		PlayerEvents.playerDamaged += Shake;
		vcam = GetComponent<CinemachineVirtualCamera>();
		noisePerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}
	public void Shake(float damage, GameObject characterHit)
	{
		if(vcam.Follow == characterHit.transform)
		{
			isShaking = true;
			noisePerlin.m_AmplitudeGain = hitAmplitudeGain;
			noisePerlin.m_FrequencyGain = hitFrequencyGain;
		}
	}
	private void StopShake()
	{
		isShaking = false;
		noisePerlin.m_AmplitudeGain = 0;
		noisePerlin.m_FrequencyGain = 0;
	}
	private void Update()
	{
		if(isShaking)
		{
			shakeTimeElapsed += Time.deltaTime;
			if(shakeTimeElapsed > shakeTime)
			{
				StopShake();
			}	
		}
	}
}
