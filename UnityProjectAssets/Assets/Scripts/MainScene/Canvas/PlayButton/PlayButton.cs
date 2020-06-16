using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    // Non-visible private attributes.
    private Animator animator = null;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets the animator component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        // See description 2 for information.
        EventManager.Instance.InitiateLogo.AddListener(LogoInitiated);
        EventManager.Instance.InitiateBegin.AddListener(TutorialInitiated);
        EventManager.Instance.InitiateBegin.AddListener(BeginInitiated);
    }

    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Local method :   LogoInitiated - Private
     * Description  :   1) Animator is triggered with "Begin" parameter.
     */
    private void LogoInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("Begin");
    }

    /*
     * Local method :   TutorialInitiated - Private
     * Description  :   1) Animator is triggered with "End" parameter.
     */
    private void TutorialInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("End");
    }

    /*
     * Local method :   BeginInititated - Private
     * Description  :   1) Animator is triggered with "End" parameter.
     */
    private void BeginInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("End");
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   OnClickedButton - Public
     * Description  :   1) "InitiateBegin".
     */
    public void OnClickedButton()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateBegin.Invoke();
        AudioManager.Instance.Play("uiClick");
    }

    /*
     * Local method :   EndOfAnimation - Private
     * Description  :   1) Checks tutorial boolean.
     *                  2) "InitiateGame" event will be invoked.
     *                  3) If tutorial boolean returns false "InitiateTutorial" event will be invoked.
     */
    private void EndOfAnimation()
    {
        // See description 1 for information.
        if (DataManager.Instance.tutorial)
        {
            // See description 2 for information.
            EventManager.Instance.InitiateGame.Invoke();
        }
        else
        {
            // See description 3 for information.
            EventManager.Instance.InitiateTutorial.Invoke();
            DataManager.Instance.SetTutorial();
        }
    }
    #endregion LOCAL METHODS
}
