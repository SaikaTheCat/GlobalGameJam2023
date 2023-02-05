using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lane : MonoBehaviour
{
	public List<Melanchall.DryWetMidi.MusicTheory.NoteName> noteRestriction;
	public List<KeyCode> input;
	public GameObject notePrefab;
	List<Note> notes = new List<Note>();
	public List<double> timeStamps = new List<double>();
	[SerializeField] List<Transform> arrowContainer;
	[SerializeField] List<InstanceWithImage> arrow;
	public int scene;
	int spawnIndex = 0;
	int inputIndex = 0;

	// Start is called before the first frame update
	void Start()
	{

	}
	public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
	{
		foreach (var note in array)
		{
			if (note.NoteName == noteRestriction[0] || note.NoteName == noteRestriction[1] || note.NoteName == noteRestriction[2] || note.NoteName == noteRestriction[3])
			{
				var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
				timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
			}
		}
	}
	private bool IsDead()
	{
		bool isDead = HealthManager.livesLeft == 0;
		Debug.Log($"livesleft {HealthManager.livesLeft}");
		return (isDead);
	}

	// Update is called once per frame
	void Update()
	{
		var isDead = IsDead();
		Debug.Log($"is dead = {isDead}");
		if (isDead)
		{
			SceneManager.LoadScene(scene);
			Destroy(gameObject);
		}
		if (spawnIndex < timeStamps.Count)
		{
			if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
			{
				var note = Instantiate(notePrefab, transform);
				notes.Add(note.GetComponent<Note>());
				note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
				spawnIndex++;
			}
		}

		if (inputIndex < timeStamps.Count)
		{
			double timeStamp = timeStamps[inputIndex];
			double marginOfError = SongManager.Instance.marginOfError;
			double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

			if (Input.GetKeyDown(input[0]))
			{
				KeyPressed(audioTime, timeStamp, marginOfError, input[0]);
				Instantiate(arrow[1], arrowContainer[1].position, arrow[1].transform.rotation);
				Debug.Log($"input: {input[0]}");
			}
			else if (Input.GetKeyDown(input[1]))
			{
				KeyPressed(audioTime, timeStamp, marginOfError, input[1]);
				Instantiate(arrow[2], arrowContainer[2].position, arrow[2].transform.rotation);
				Debug.Log($"input: {input[1]}");
			}
			else if (Input.GetKeyDown(input[2]))
			{
				KeyPressed(audioTime, timeStamp, marginOfError, input[2]);
				Instantiate(arrow[0], arrowContainer[3].position, arrow[0].transform.rotation);
				Debug.Log($"input: {input[2]}");
			}
			else if (Input.GetKeyDown(input[3]))
			{
				KeyPressed(audioTime, timeStamp, marginOfError, input[3]);
				Instantiate(arrow[3], arrowContainer[0].position, arrow[3].transform.rotation);
				Debug.Log($"input: {input[3]}");
			}
			if (timeStamp + marginOfError <= audioTime)
			{
				Miss();
				
				print($"Missed {inputIndex} note");
				inputIndex++;
			}
		}

	}
	private void KeyPressed(double audioTime, double timeStamp, double marginOfError, KeyCode keyCode)
	{
		if (Math.Abs(audioTime - timeStamp) < marginOfError)
		{
			Hit();
			FindObjectOfType<RootController>().MakeAMove(keyCode);
			print($"Hit on {inputIndex} note");
			Destroy(notes[inputIndex].gameObject);
			inputIndex++;
		}
		else
		{
			print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
		}
	}
	private void Hit()
	{
		ScoreManager.Hit();
	}
	public void Miss()
	{
		PlayerEvents.playerDamaged.Invoke(1f, gameObject);
		ScoreManager.Miss();
		HealthManager.HealthMinus();
		HealthManager.healthMinusTriggered = true;
	}
}
