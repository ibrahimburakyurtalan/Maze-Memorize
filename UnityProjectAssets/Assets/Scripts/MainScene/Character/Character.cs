using UnityEngine;

public class Character : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private Joystick joystick = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private float acceleration = 0;
    [SerializeField] private bool movementEnable = false;
    [SerializeField] private Maze maze = null;

    // Non-visible private attributes
    private Animator animator = null;
    private Rigidbody2D rb2D = null;
    private bool isCharacterActive = false;
    private bool isExpandActive = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets animator and sprite renderer components.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        // See description 2 for information.
        EventManager.Instance.InitiateGame.AddListener(GameInitiated);
        EventManager.Instance.InitiatePlay.AddListener(PlayInitiated);
        EventManager.Instance.InitiateEnd.AddListener(EndInitiated);
        EventManager.Instance.InitiateAsyncGame.AddListener(RestartInitiated);
        EventManager.Instance.InitiateExpand.AddListener(ExpandInitiated);
    }

    /*
     * Unity method :   Update - Private
     * Description  :   1) 
     */
    private void Update()
    {
        // See description 1 for information
        if (movementEnable && joystick != null)
        {
            if (Input.GetMouseButton(0))
            {
                Rotate(joystick.direction);
                SpeedUp(joystick.direction);
            }
        }
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   GameInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void GameInitiated()
    {
        // See description 1 for information
        ResetCharacter();
    }

    /*
     * Event method :   PlayInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void PlayInitiated()
    {
        // See description 1 for information
        bool isCircle;
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro")) { isCircle = ((DataManager.Instance.currentProLevel / 2) % 2 == 0); }
        else { isCircle = ((DataManager.Instance.currentLevel / 2) % 2 == 0); }
        
        if (isCircle)
        {
            Quaternion target = Quaternion.Euler(maze.mazeAngle + new Vector3(0f, 0f, 159f));
            transform.rotation = target;
        }
        else
        {
            Quaternion target = Quaternion.Euler(maze.mazeAngle + new Vector3(0f, 0f, 180f));
            transform.rotation = target;
        }
        
        animator.SetTrigger("Begin");
        isCharacterActive = true;
    }

    /*
     * Event method :   EndInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void EndInitiated()
    {
        // See description 1 for information
        if (isExpandActive)
        {
            animator.SetTrigger("ExpandEnd");
        }
        else
        {
            animator.SetTrigger("End");
        }
        isExpandActive = false;
        isCharacterActive = false;
    }

    /*
     * Event method :   RestartInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void RestartInitiated(string eventType)
    {
        if (isCharacterActive)
        {
            if (isExpandActive)
            {
                animator.SetTrigger("ExpandEnd");
            }
            else
            {
                animator.SetTrigger("End");
            }
            isExpandActive = false;
            isCharacterActive = false;
        }
        else
        {
            // Do nothing.
        }
    }

    /*
     * Event method :   ExpandInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void ExpandInitiated()
    {
        animator.SetTrigger("Expand");
        isExpandActive = true;
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   Rotate - Private
     * Description  :   1) 
     */
    private void Rotate(Vector3 position)
    {
        if (position != Vector3.zero)
        {
            Vector3 currentTouchPosition = position - Vector3.zero;
            float newAngle = Mathf.Atan2(currentTouchPosition.y, currentTouchPosition.x) * Mathf.Rad2Deg;
            Quaternion angle = Quaternion.Euler(0, 0, (newAngle - 90));
            transform.rotation = angle;
        }
    }

    /*
     * Local method :   SpeedUp - Private
     * Description  :   1) 
     */
    private void SpeedUp(Vector3 speedInfo)
    {
        if (speedInfo != Vector3.zero)
        {
            if (rb2D.velocity.magnitude < speed)
            {
                rb2D.AddForce(transform.up * acceleration);
            }
        }
    }

    /*
     * Local method :   EnableMask - Private
     * Description  :   1) "InitiateEnableMask" event is invoked here.
     */
    private void EnableMask()
    {
        EventManager.Instance.InitiateEnableMask.Invoke();
    }

    /*
     * Local method :   DisableMask - Private
     * Description  :   1) "InitiateDisableMask" event is invoked here.
     */
    private void DisableMask()
    {
        EventManager.Instance.InitiateDisableMask.Invoke();
    }

    /*
     * Local method :   ResetCharacter - Private
     * Description  :   1) Resets character transform.
     *                  2) 
     */
    private void ResetCharacter()
    {
        // See description 1 for information.
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        Transform transform = GetComponent<Transform>();
        // See description 2 for information.
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    #endregion LOCAL METHODS
}
