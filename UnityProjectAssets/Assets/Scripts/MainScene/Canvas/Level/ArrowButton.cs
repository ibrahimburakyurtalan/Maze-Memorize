using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private bool isNext = false;

    #region LOCAL METHODS
    /*
     * Local method :   OnButtonClicked - Public    
     * Description  :   1) Icon animator is triggered with "Begin" parameter.
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        AudioManager.Instance.Play("uiClick");
        if (isNext)
        {
            EventManager.Instance.InitiateAsyncGame.Invoke("next");
        }
        else
        {
            EventManager.Instance.InitiateAsyncGame.Invoke("previous");
        }
        
    }   
    #endregion LOCAL METHODS
}
