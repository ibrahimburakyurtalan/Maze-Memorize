using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private Animator iconAnimator = null;
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;
    private bool iconAnimatorState = false;

    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets the animator component.
     *                  2) Event listeners are created.
     */
    void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        // See description 2 for information.
        EventManager.Instance.InitiateMenu.AddListener(MainMenuInitiated);
        // See description 3 for information.
        if (DataManager.Instance.sound == 1)
        {
            iconAnimatorState = false;
            iconAnimator.SetTrigger("On");
        }
        else
        {
            iconAnimatorState = true;
            iconAnimator.SetTrigger("Off");
        }
    }

    /*
     * Local method :   MainMenuInitiated - Private
     * Description  :   1) Animator is triggered by using a boolean flag repetitively.
     */
    private void MainMenuInitiated()
    {
        // See description 1 for information.
        if (!animatorState)
        {
            animator.SetTrigger("Begin");
        }
        else
        {
            animator.SetTrigger("End");
        }
        animatorState = !animatorState;
    }

    /*
     * Local method :   OnButtonClicked - Public    
     * Description  :   1) Icon animator is triggered by using a boolean flag repetitively.
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        if (!iconAnimatorState)
        {
            iconAnimator.SetTrigger("Begin");
            AudioManager.Instance.Mute(true);
        }
        else
        {
            iconAnimator.SetTrigger("End");
            AudioManager.Instance.Mute(false);
        }
        iconAnimatorState = !iconAnimatorState; 
    }
}

