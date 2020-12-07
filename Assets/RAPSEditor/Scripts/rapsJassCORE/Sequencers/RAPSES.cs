using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rhapsody Audio Pattern System Euclidean Sequencer.
/// An Eulidean sequencer.
/// </summary>

public class RAPSES : Beeper
{
	public string myName;
	public bool isMuted;
	
	[Range(1, 32)] public int steps = 16;
	[Range(1, 32)] public int triggers = 4;

	private int currentStep;



	

	/// <summary>
	/// Subscribe to the beeper
	/// </summary>

	private void OnEnable()
	{
		if (myBeeper != null)
		{
			myBeeper.Beeped += HandleBeeped;
		}
	}
	/// <summary>
	/// Unsubscribe from the beeper
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
		if (!isMuted)
		{
			if (IsStepOn(currentStep, steps, triggers))
			{
				DoBeep(tickTime, midiNoteNumber, volume);
			}

			currentStep = (currentStep + 1) % steps;
		}
	}

	private static bool IsStepOn(int step, int numSteps, int numTriggers)
	{
		return (step * numTriggers) % numSteps < numTriggers;
	}
}
