using UnityEngine;

public class RAPSTrigger : MonoBehaviour
{
    [Header("Who is the triggerer")]
    [SerializeField] private string whoTrigTagField;
    [SerializeField] private string bsName;
    [SerializeField] private bool isMuted;
    [SerializeField] private bool isES;
    [SerializeField] private bool changePattern;
    [SerializeField] private string PatternName;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    bool triggered;

  // Start is called before the first frame update
  void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (triggered)
        {
          
         
            boxCollider2D.enabled =false;
        }
    }
  
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == whoTrigTagField)
        {
           var RAPSDJ = collision.GetComponent<RAPSDJ>();
            if(!isES)
           ChangeMute(ref RAPSDJ);
            else
          ChangesMute(ref RAPSDJ);
            triggered = true;
            RAPSDJ = collision.gameObject.GetComponent<RAPSDJ>();
        }
    }


    private void ChangeMute(ref RAPSDJ RAPSDJ)
    {
        RAPSDJ.SetMute(bsName, isMuted);
    }
    private void ChangesMute(ref RAPSDJ RAPSDJ)
    {
        RAPSDJ.SetMuteES(bsName, isMuted);
    }


}
