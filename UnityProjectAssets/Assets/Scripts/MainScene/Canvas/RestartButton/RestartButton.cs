using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private Animator iconAnimator = null;
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets the animator and button components.
     *                  2) Event listeners are created.
     */
    void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();    
        // See description 2 for information.
        EventManager.Instance.InitiateMenu.AddListener(MainMenuInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   MainMenuInitiated - Private
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
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   OnButtonClicked - Public    
     * Description  :   1) Icon animator is triggered with "Begin" parameter.
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        if (iconAnimator != null)
        {
            AudioManager.Instance.Play("transient");
            iconAnimator.SetTrigger("Begin");
            EventManager.Instance.InitiateAsyncGame.Invoke("restart");
        } else
        {
            // Error: Animator is null.
        }
    }
    #endregion LOCAL METHODS
}
