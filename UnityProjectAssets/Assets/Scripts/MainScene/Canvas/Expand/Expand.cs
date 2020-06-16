using System;
using System.Collections;
using UnityEngine;

public class Expand : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private Coroutine timerCoroutine = null;
    private bool isEnumeratorBegin = false;
    private bool isAnimationBegin = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets animator component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        // See description 2 for information.
        EventManager.Instance.InitiatePlay.AddListener(PlayInitiated);
        EventManager.Instance.InitiateEnd.AddListener(EndInitiated);
        EventManager.Instance.InitiateAsyncGame.AddListener(RestartInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   PlayInitiated - Private
     * Description  :   1) Random time value is generated.
     *                  2) Timer coroutine is started.
     */
    private void PlayInitiated()
    {
        // See description 1 for information.
        int time = UnityEngine.Random.Range(8, 13);
        // See description 2 for information.
        timerCoroutine = StartCoroutine(Timer(time));
        isEnumeratorBegin = true;
    }

    /*
     * Event method :   EndInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void EndInitiated()
    {
        // See description 1 for information.
        if (isEnumeratorBegin)
        {
            StopCoroutine(timerCoroutine);
            if (isAnimationBegin)
            {
                animator.SetTrigger("End");
            }
            else
            {
                // Do nothing.
            }
            isEnumeratorBegin = false;
            isAnimationBegin = false;
        }
        else
        {

        }
    }

    /*
     * Event method :   RestartInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void RestartInitiated(string eventType)
    {
        // See description 1 for information.
        if (isEnumeratorBegin)
        {
            StopCoroutine(timerCoroutine);
            if (isAnimationBegin)
            {
                animator.SetTrigger("End");
            }
            else
            {
                // Do nothing.
            }
            isEnumeratorBegin = false;
            isAnimationBegin = false;
        }
        else
        {

        }
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   PlayInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private IEnumerator Timer(int timeInSeconds)
    {
        // See description 1 for information.
        int timePassing = 0;
        while (timePassing < timeInSeconds)
        {
            timePassing++;
            yield return new WaitForSeconds(1);
        }
        // See description 2 for information.
        bool isAdReady = AdManager.Instance.CheckRewardedAd();
        if (!isAdReady) { yield break; }

        animator.SetTrigger("Begin");
        isAnimationBegin = true;
    }

    /*
     * Local method :   OnButtonClick - Public
     * Description  :   1) 
     *                  2) 
     */
    public void OnButtonClick ()
    {
        // See description 1 for information.
        if (isEnumeratorBegin)
        {
            // TODO: Change this statement with the ad version.
            AudioManager.Instance.Play("uiClick");
            StopCoroutine(timerCoroutine);
            bool isAdReady = AdManager.Instance.PlayRewardedAd(AdCallback);
            if (!isAdReady)
            {
                if (isAnimationBegin)
                {
                    animator.SetTrigger("End");

                }
                else
                {
                    // Do nothing.
                }
                isEnumeratorBegin = false;
                isAnimationBegin = false;
            }
        } 
        else
        {
            // Do nothing
        }
    }

    /*
     * Local method :   AdCallback - Public
     * Description  :   1) 
     *                  2) 
     */
    private void AdCallback()
    {
        if (isAnimationBegin)
        {
            animator.SetTrigger("End");

        }
        else
        {
            // Do nothing.
        }
        isEnumeratorBegin = false;
        isAnimationBegin = false;
        EventManager.Instance.InitiateExpand.Invoke();
    }
    #endregion LOCAL METHODS
}
