using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugtest : MonoBehaviour
{


    public float Inputaxisx=0;


    [SerializeField] private Beeper mybeeper;
	[SerializeField] private RAPSMetron myMetron;
	[SerializeField] private RAPSBS myBs;
    // Start is called before the first frame update
    void Start()
    {
        if(Inputaxisx==0)
		{
			myBs.isMuted = true;
		}
    }

	/// <summary>
	/// Subscribe to the ticker
	/// </summary>

	private void OnEnable()
	{
		if (mybeeper != null)
		{
			mybeeper.Beeped += HandleBeeped;
		}
	}
	/// <summary>
	/// Unsubscribe from the ticker
	/// </summary>
	private void OnDisable()
	{
		if (mybeeper != null)
		{
			mybeeper.Beeped -= HandleBeeped;
		}
	}

	private void HandleBeeped(double beepTime, int midiNoteNumber, float volume)
	{
		if(Inputaxisx !=0)
        {
		
			Debug.Log(Inputaxisx);
        }
	}



	// Update is called once per frame
	void Update()
    {
        Inputaxisx = Input.GetAxis("Horizontal");
		if (Inputaxisx != 0)
		{
			myBs.isMuted = false;
			Debug.Log(Inputaxisx);
		}
		if (Inputaxisx == 0)
		{
			myBs.isMuted = true;

		}

	
	}
}
