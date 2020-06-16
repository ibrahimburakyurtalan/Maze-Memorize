using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private TextMeshProUGUI number = null;
    // Non-visible private attributes
    private Animator animator = null;
    private bool isInitial = true;


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
        EventManager.Instance.InitiateGame.AddListener(GameInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   GameInitiated - Private
     * Description  :   1) The number value is setting with the level value.
     *                  2) Animator is triggered with "Initial" parameter.
     */
    private void GameInitiated()
    {
        // See description 1 for information.
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro")) 
        {
            bool cap = DataManager.Instance.reachedProCap;
            if (cap) { number.text = "max"; }
            else { number.text = (DataManager.Instance.currentProLevel + 1).ToString(); }
        }
        else 
        {
            bool cap = DataManager.Instance.reachedCap;
            if (cap) { number.text = "max"; }
            else { number.text = (DataManager.Instance.currentLevel + 1).ToString(); }
        }
        // See description 2 for information.
        if (isInitial)
        {
            animator.SetTrigger("Initial");
            isInitial = false;
        }
        else
        {
            StartMaze();
        }
        
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   StartMaze - Private
     * Description  :   1) "InitiateLevel" event is invoked here.
     */
    private void StartMaze()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateLevel.Invoke();
    }
    #endregion LOCAL METHODS
}
