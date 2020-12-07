using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAPSDJ : MonoBehaviour
{

    [SerializeField] public List<RAPSBS>availableBS;

    [SerializeField] public List<RAPSES> availableES;

    [SerializeField] private List<bool> activeBS;
    [SerializeField] private List<bool> activeES;

 

    /// <summary>
    /// use to innitalize stuff
    /// </summary>
    private void OnEnable()
    {
        foreach ( RAPSBS rAPSBS in availableBS)
        {
            activeBS.Add(rAPSBS.isMuted);
        }
    }

   /// <summary>
   /// use to mute and unmute 
   /// </summary>
   /// <param name="bsName"> The BS we want to mute</param>
   /// <param name="isMute"> Is muted or not</param>
   public void SetMute(string bsName, bool isMute)
    {
        RAPSBS rAPSBS = availableBS.Find(x => x.myName == bsName);
        rAPSBS.isMuted = isMute;
    }

    /// <summary>
    /// use to mute and unmute 
    /// </summary>
    /// <param name="bsName"> The BS we want to mute</param>
    /// <param name="isMute"> Is muted or not</param>
    public void SetMuteES(string bsName, bool isMute)
    {
        RAPSES rAPSBS = availableES.Find(x => x.myName == bsName);
        rAPSBS.isMuted = isMute;
    }

   

#if UNITY_EDITOR
    public List<RAPSBS> GetBS()
    {
        return availableBS;
    }

    public List<RAPSES> GetES()
    {
        return availableES;
    }
#endif


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
