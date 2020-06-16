using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
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
            case "url":
                Application.OpenURL("https://www.gurugamestudios.com");
                break;
            case "music":
                Application.OpenURL("https://eoun.com");
                break;
            case "free":
                Application.OpenURL("https://eoun.com/track/relaxed-time-travel");
                break;
            case "more":
                Application.OpenURL("https://play.google.com/store/apps/developer?id=gurugamestudios");
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
    #endregion LOCAL METHODS
}
