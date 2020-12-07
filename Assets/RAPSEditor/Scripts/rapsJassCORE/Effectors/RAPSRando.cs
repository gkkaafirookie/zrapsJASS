using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rhapsody Audio Pattern System Randomizer.
/// use to add probalility to the funk.
/// </summary>

public class RAPSRando : Beeper
{



    [Range(0.0f, 1.0f)] public float probability;

  


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
		// roll the dice to see if we should play this note
		float rand = UnityEngine.Random.value;
		if (rand < probability)
		{
			DoBeep(tickTime, midiNoteNumber, volume);
		}

		
	}
}
