using UnityEngine;

public class MenuButton : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets the animator component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information
        animator = GetComponent<Animator>();
        // See description 2 for information.
        EventManager.Instance.InitiateLevel.AddListener(LevelInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1) Animator is triggered with "Initial" parameter.
     */
    private void LevelInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("Initial");
        EventManager.Instance.InitiateLevel.RemoveListener(LevelInitiated);
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   OnClickedButton - Public
     * Description  :   1) Animator is triggered by using a boolean flag repetitively.
     *              :   2) Invokes "InitiateMenuButton" event.
     */
    public void OnClickedButton()
    {
        // See description 1 for information.
        if (!animatorState)
        {
            animator.SetTrigger("Begin");
        } else
        {
            animator.SetTrigger("End");
        }
        animatorState = !animatorState;
        AudioManager.Instance.Play("uiClick");

        // See description 2 for information.
        EventManager.Instance.InitiateMenu.Invoke();
    }
    #endregion LOCAL METHODS
}
