using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioObj : MonoBehaviour {
	public AudioSource audioSource;
	public static float[] samples = new float[512];
	public static float[] freqBand = new float[8];


	public bool useMic;
	string selectedDevice;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		if(useMic)
		{
			if(Microphone.devices.Length > 0)
			{
				selectedDevice = Microphone.devices[0].ToString();
				audioSource.clip = Microphone.Start(selectedDevice, true, 999, AudioSettings.outputSampleRate);
			}
			else
			{
				useMic = false;
				Debug.Log("No Mic working");
			}
		}

		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		audioSource.GetSpectrumData (samples, 0, FFTWindow.BlackmanHarris);
		VisualizeAudio ();
	}

	void VisualizeAudio() {
		int count = 0, numSamples = 0;
		float avgFreq, total = 0;

		for (int i = 1; i <= 8; i++) {
			/* number of samples for the current band, goes up by 2^n */
			numSamples = (int)Mathf.Pow (2, i);

			/* Take the average of all the frequencies for that band */
			for (int j = 0; j < numSamples; j++) {
				total += samples [count] * (count + 1);
				count++;
			}
			avgFreq = total / numSamples;

			freqBand [i - 1] = avgFreq;
		}
	}
}
