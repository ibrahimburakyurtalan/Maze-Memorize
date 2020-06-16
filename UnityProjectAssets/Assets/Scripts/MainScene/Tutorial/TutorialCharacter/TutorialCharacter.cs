using UnityEngine;

public class TutorialCharacter : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private TutorialJoystick joystick = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private float acceleration = 0;
    [SerializeField] private bool movementEnable = false;

    // Non-visible private attributes
    private Animator animator = null;
    private Rigidbody2D rb2D = null;

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
    }

    /*
     * Unity method :   Update - Private
     * Description  :   1) 
     */
    void Update()
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
    #endregion LOCAL METHODS
}
