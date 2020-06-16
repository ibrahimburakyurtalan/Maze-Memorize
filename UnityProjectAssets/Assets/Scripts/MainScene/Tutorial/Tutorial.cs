using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private GameObject character = null;
    [SerializeField] private SpriteRenderer maze = null;
    [SerializeField] private SpriteRenderer mazePointers = null;
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
        EventManager.Instance.InitiateTutorial.AddListener(TutorialInitiated);
        EventManager.Instance.InitiateGame.AddListener(GameInitiated);
    }

    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Local method :   TutorialInitiated - Private
     * Description  :   1) Animator is triggered with "Begin" parameter.
     */
    private void TutorialInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("Begin");
    }

    /*
     * Local method :   BeginInititated - Private
     * Description  :   1) Gameobject will be destroyed.
     */
    private void GameInitiated()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateTutorial.RemoveListener(TutorialInitiated);
        Destroy(gameObject, 0.1f);
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   OnClickedNext - Public
     * Description  :   1) Animator is triggered here according to tutorial stage.
     */
    public void OnClickedNext(string buttonInfo)
    {
        // See description 1 for information.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (buttonInfo == "next")
        {
            AudioManager.Instance.Play("uiClick");
        }
        else if (buttonInfo == "finish")
        {
            AudioManager.Instance.Play("transient");
        }
        
        if (stateInfo.IsName("Stage1Idle"))
        {
            animator.SetTrigger("Stage1End");
        }
        else if (stateInfo.IsName("Stage2Begin"))
        {
            animator.SetTrigger("Stage2End");
        }
        else if (stateInfo.IsName("Stage3Idle"))
        {
            animator.SetTrigger("Stage3End");
        }
        else if (stateInfo.IsName("Stage4Begin"))
        {
            animator.SetTrigger("Stage4End");
        }
        else if (stateInfo.IsName("Stage5Begin"))
        {
            animator.SetTrigger("Stage5End");
        }
        else if (stateInfo.IsName("Stage6Begin"))
        {
            animator.SetTrigger("Stage6End");
        }
        else if (stateInfo.IsName("Stage7Begin"))
        {
            animator.SetTrigger("Stage7End");
        }
    }

    /*
     * Local method :   OnClickedSkip - Public
     * Description  :   1) 
     */
    public void OnClickedSkip()
    {
        // See description 1 for information.
        animator.SetTrigger("Skip");
        AudioManager.Instance.Play("transient");
    }

    /*
     * Local method :   ResetCharacter - Public
     * Description  :   1) 
     */
    public void ResetCharacter()
    {
        // See description 1 for information.
        Rigidbody2D rb2D = character.GetComponent<Rigidbody2D>();
        Transform transform = character.GetComponent<Transform>();
        // See description 2 for information.
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    /*
     * Local method :   MaskInteraction - Public
     * Description  :   1) 
     */
    public void MaskInteraction()
    {
        // See description 1 for information.
        maze.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        mazePointers.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    /*
     * Local method :   EndOfTutorial - Public
     * Description  :   1)
     *                  2) 
     */
    public void EndOfTutorial()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateLogo.Invoke();
        // See description 2 for information.
        EventManager.Instance.InitiateTutorial.RemoveListener(TutorialInitiated);
        Destroy(gameObject, 0.1f);
    }
    #endregion LOCAL METHODS
}
