using UnityEngine;
using UnityEngine.UI;

public class ProButton : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private Animator iconAnimator = null;
    [SerializeField] private GameObject proPanel = null;
    [SerializeField] private Image icon = null;
    [SerializeField] private Sprite pro = null;
    [SerializeField] private Sprite reg = null;
    // Non-visible private attributes
    private Animator animator = null;
    private bool animatorState = false;
    private bool spriteFlag = false;
    private bool switchEnable = true;

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
        EventManager.Instance.InitiateRefreshPro.AddListener(RefreshProInitiated);
        // See descriotion 3 for information.
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro")) { spriteFlag = !spriteFlag; icon.sprite = reg; return; }
        icon.sprite = pro;
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
     * Local method :   RefreshProInitiated - Private
     * Description  :   1) 
     */
    private void RefreshProInitiated()
    {
        // See description 1 for information.
        switchEnable = true;
    }

    /*
     * Local method :   OnButtonClicked - Public    
     * Description  :   1) Icon animator is triggered with "Begin" parameter.
     */
    public void OnButtonClicked()
    {
        // See description 1 for information.
        if (iconAnimator == null) { return; }

        // See description 2 for information.
        AnimatorStateInfo stateInfo = iconAnimator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Initial")) { return; }

        // See description 2 for information.
        bool isProActivated = string.Equals(DataManager.Instance.pro, "faruk");
        if (!isProActivated)
        {
            AudioManager.Instance.Play("uiClick");
            iconAnimator.SetTrigger("Begin");

            bool isIAPActivated = IAPManager.Instance.isIAPInitialized;
            if (!isIAPActivated) { return; }

            proPanel.SetActive(true);
        }
        else
        {
            AudioManager.Instance.Play("uiClick");
            if (switchEnable)
            {
                switchEnable = false;
                iconAnimator.SetTrigger("Begin");
                spriteFlag = !spriteFlag;
                EventManager.Instance.InitiateAsyncGame.Invoke("pro");
                if (spriteFlag) { icon.sprite = reg; return; }
                icon.sprite = pro;
            }
        }         
    }
}
