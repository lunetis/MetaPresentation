using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public GameObject helpForPresentor;
    public GameObject helpForAudience;
    
    public bool isForPresentor = true;
    
    // Start is called before the first frame update
    public void ToggleHelp()
    {
        if(PresentationController.IsHost()==true)
        {
            helpForPresentor?.SetActive(!helpForPresentor.activeSelf);
        }
        else
        {
            helpForAudience?.SetActive(!helpForAudience.activeSelf);
        }
    }
}
