using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoButton : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private Animator iconAnimator = null;
    [SerializeField] private GameObject infoPanel = null;
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;

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
     * Description  :   1) Icon animator is triggered with "Begin" parameter.
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        if (iconAnimator != null)
        {
            AudioManager.Instance.Play("uiClick");
            iconAnimator.SetTrigger("Begin");
            infoPanel.SetActive(true);
        }
        else
        {
            // Error: Animator is null.
        }
    }
}
