using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAPSVolCnt : Beeper
{

	public enum Mode
	{
		Set,
		Multiply,
		Add
	}
	
	[Range(0.0f, 1.0f)] public float volume;
	public Mode mode;




	/// <summary>
	/// Subscribe to the BEEPER
	/// </summary>

	private void OnEnable()
	{
		if (myBeeper != null)
		{
			myBeeper.Beeped += HandleBeeped;
		}
	}
	/// <summary>
	/// Unsubscribe from the BEEPER
	/// </summary>
	private void OnDisable()
	{
		if (myBeeper != null)
		{
			myBeeper.Beeped -= HandleBeeped;
		}
	}

	public void HandleBeeped(double tickTime, int midiNoteNumber, float volume)
	{
		float newVolume = 0.0f;
		switch (mode)
		{
			case Mode.Set:
				newVolume = this.volume;
				break;
			case Mode.Multiply:
				newVolume = this.volume * volume;
				break;
			case Mode.Add:
				newVolume = this.volume + volume;
				break;
		}
		DoBeep(tickTime, midiNoteNumber, volume);
		


	}
}
