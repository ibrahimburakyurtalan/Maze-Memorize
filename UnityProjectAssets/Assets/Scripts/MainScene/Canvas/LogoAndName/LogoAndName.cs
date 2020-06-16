using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAndName : MonoBehaviour
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
}
