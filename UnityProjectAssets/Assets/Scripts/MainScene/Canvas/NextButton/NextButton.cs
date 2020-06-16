using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private bool isNextButtonActive = false;
    private bool isMenuActive = false;
    private bool doesEndOrRestartStart = true;

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
        EventManager.Instance.InitiateMenu.AddListener(MainMenuInitiated);
        EventManager.Instance.InitiateEnd.AddListener(EndInitiated);
        EventManager.Instance.InitiateAsyncGame.AddListener(RestartInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS

    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1) 
     */
    private void EndInitiated()
    {
        // See description 1 for information
        doesEndOrRestartStart = false;
        if (isNextButtonActive)
        {
            // Do nothing
        }
        {
            if (isMenuActive)
            {
                // Do nothing
            }
            else
            {
                animator.SetTrigger("Begin");
                isNextButtonActive = true;
            }
        }
    }

    /*
     * Local method :   MainMenuInitiated - Private
     * Description  :   1) 
     *                  2) Animator is triggered by using a boolean flag repetitively.
     */
    private void MainMenuInitiated()
    {
        isMenuActive = !isMenuActive;

        if (doesEndOrRestartStart)
        {
            // Do nothing
        }
        else
        {
            // See description 2 for information.
            if (isNextButtonActive)
            {
                animator.SetTrigger("End");
                isNextButtonActive = false;
            }
            else
            {
                animator.SetTrigger("Begin");
                isNextButtonActive = true;
            }
        }
    }

    /*
     * Local method :   RestartInitiated - Private
     * Description  :   1) 
     *                  2)
     */
    private void RestartInitiated(string eventType)
    {
        // See description 2 for information.
        if (isNextButtonActive)
        {
            animator.SetTrigger("End");
            isNextButtonActive = false;
        }
        else
        {
            doesEndOrRestartStart = true;
        }
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   OnButtonClicked - Public    
     * Description  :   1) 
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        if (isNextButtonActive)
        {
            AudioManager.Instance.Play("uiClick");
            animator.SetTrigger("End");
            isNextButtonActive = false;
            doesEndOrRestartStart = true;
            EventManager.Instance.InitiateFinish.Invoke();
        }
    }
    #endregion LOCAL METHODS
}
