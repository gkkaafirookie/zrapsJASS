using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SimpleOnBeatColorShifter : Beeper
{
      [Header("Config")]
   
     public bool PingPong; 
    
    
    private Color Color1;
    

    [SerializeField] private Color[] ColorShift;

    bool colorshifted;

    Color[] newColor = new Color[2];

     private Renderer myRenderer;

  	/// <summary>
	/// Subscribe to the beeper
	/// </summary>

	private void OnEnable()
	{
		if (myBeeper != null)
		{
			myBeeper.Beeped += HandleBeeped;
		}

        myRenderer = GetComponent<Renderer>();
        Color1 = myRenderer.material.color;
       newColor[0] = Color1;
       newColor[1] = ColorShift[1];
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
       
        if(PingPong)
        {
              if(!colorshifted)
                {
                  newColor[0] = ColorShift[0];
                  newColor[1] = Color1;
                  colorshifted = true;
                }
                else
                {
                    newColor[0] = ColorShift[0];
                    newColor[1] = Color1;
                    colorshifted = false;
                }
        }
        else
        {
        if(!colorshifted)
                {
                  newColor[0] = ColorShift[0];
                  newColor[1] = Color1;
                  colorshifted = true;
                }
        else
         {
        if(ColorShift.Length <2) { ColorShift[1] = Color1;}
         newColor[0] = ColorShift[1];
        newColor[1] = Color1;
                  
        colorshifted = false;
        }
      }
        
        
        
		
	}


void Update()
{

      myRenderer.material.SetColor("_Color", Color.Lerp(newColor[0], newColor[1], myBeeper.Temperature));
}
  

	
}
