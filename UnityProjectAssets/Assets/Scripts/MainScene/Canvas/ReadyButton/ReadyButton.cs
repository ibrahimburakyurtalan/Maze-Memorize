using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private bool isReadyButtonActive = false;
    private bool isMenuActive = false;
    private bool doesGameOrRestartStart = false;

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
        EventManager.Instance.InitiateLevel.AddListener(LevelInitiated);
        EventManager.Instance.InitiateMenu.AddListener(MainMenuInitiated);
        EventManager.Instance.InitiateAsyncGame.AddListener(RestartInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS

    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1) 
     */
    private void LevelInitiated()
    {
        // See description 1 for information
        doesGameOrRestartStart = false;
        if (isReadyButtonActive)
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
                string mode = DataManager.Instance.mode;
                bool cap;
                if (string.Equals(mode, "buro"))
                { 
                    cap = DataManager.Instance.reachedProCap;
                }
                else
                {
                    cap = DataManager.Instance.reachedCap;
                }
                     
                if (cap) { return; }

                animator.SetTrigger("Begin");
                isReadyButtonActive = true;
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

        string mode = DataManager.Instance.mode;
        bool cap;
        if (string.Equals(mode, "buro"))
        {
            cap = DataManager.Instance.reachedProCap;
        }
        else
        {
            cap = DataManager.Instance.reachedCap;
        }

        if (cap) { return; }

        if (doesGameOrRestartStart)
        {
            // Do nothing
        }
        else
        {
            // See description 2 for information.
            if (isReadyButtonActive)
            {
                animator.SetTrigger("End");
                isReadyButtonActive = false;
            }
            else
            {
                animator.SetTrigger("Begin");
                isReadyButtonActive = true;
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
        if (isReadyButtonActive)
        {
            animator.SetTrigger("End");
            isReadyButtonActive = false;
        }
        else
        {
            doesGameOrRestartStart = true;
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
        if (isReadyButtonActive)
        {
            AudioManager.Instance.Play("uiClick");
            animator.SetTrigger("End");
            isReadyButtonActive = false;
            doesGameOrRestartStart = true;
            EventManager.Instance.InitiatePlay.Invoke();
        }
    }
    #endregion LOCAL METHODS
}
