using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewPanel : MonoBehaviour
{
    #region LOCAL METHODS
    /*
     * Local method :   ColorLerp - Private
     * Description  :   1) 
     */
    public void OnClickedButton(string button)
    {
        AudioManager.Instance.Play("uiClick");

        switch (button)
        {
            case "back":
                gameObject.SetActive(false);
                break;
            case "rate":
                Application.OpenURL("market://details?id=" + Application.identifier);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
    #endregion LOCAL METHODS
}
