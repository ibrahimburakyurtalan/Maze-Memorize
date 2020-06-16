using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;

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
}
