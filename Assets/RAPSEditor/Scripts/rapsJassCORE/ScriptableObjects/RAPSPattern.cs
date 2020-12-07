using System.Security;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RAPSPattern", menuName = "zrapsJASS/Pattern", order = 1)]
public class RAPSPattern : ScriptableObject
{

    public string patternName;

    public List<Beat> Beats;

    [System.Serializable]
    public class Beat
    {
        public bool Active;
        [Range(1, 7)]
        public int Octave = 4;
        [Range(0.0f, 1.0f)]
        public int ScaleTone =1;
        [Range(-1, 8)]
        public float Volume;
    }

    public void init(string pattern, List<Beat> beats)
    {
        patternName = pattern;
        Beats = beats;
    }

#if UNITY_EDITOR
    public List<Beat> GetBeats()
    {
        return Beats;
    }
#endif

}
