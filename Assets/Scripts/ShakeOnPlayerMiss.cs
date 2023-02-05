using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnPlayerMiss : MonoBehaviour
{
	public float hitAmplitudeGain, hitFrequencyGain, shakeTime;
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
	void OnDestroy()
	{
		PlayerEvents.playerDamaged -= Shake;
	}
	public void Shake(float damage, GameObject characterHit)
	{
		isShaking = true;
		noisePerlin.m_AmplitudeGain = hitAmplitudeGain;
		noisePerlin.m_FrequencyGain = hitFrequencyGain;
		Debug.Log($"shakee gain:{hitAmplitudeGain} freq:{hitAmplitudeGain}");
		
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
