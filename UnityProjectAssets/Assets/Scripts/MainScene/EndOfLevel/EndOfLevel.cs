using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private Maze maze = null;
    // Non-visible private attributes
    private BoxCollider2D boxCollider2D = null;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets box collider component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        boxCollider2D = GetComponent<BoxCollider2D>();
        // See description 2 for information.
        EventManager.Instance.InitiateLevel.AddListener(LevelInitiated);
    }

    /*
     * Unity method :   OnTriggerEnter2D - Private
     * Description  :   1)
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See description 1 for information.
        if (collision.tag == "charactertag")
        {
            EventManager.Instance.InitiateEnd.Invoke();
            AudioManager.Instance.Play("transient");
        }
        else
        {
            // Do nothing
        }
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1)
     *                  2)

     */
    private void LevelInitiated()
    {
        // See description 1 for information.
        SetCollider();
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   SetCollider - Private
     * Description  :   1)
     *                  2)

     */
    private void SetCollider()
    {
        // See description 1 for information.
        bool isCircle;
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro")) { isCircle = ((DataManager.Instance.currentProLevel / 2) % 2 == 0); }
        else { isCircle = ((DataManager.Instance.currentLevel / 2) % 2 == 0); }

        if (isCircle)
        {
            Quaternion target = Quaternion.Euler(maze.mazeAngle - new Vector3(0f, 0f, 5f));
            transform.rotation = target;
            transform.position = Vector3.zero;
            boxCollider2D.offset = new Vector2(0f, 6.5f);
        }
        else
        {
            Quaternion target = Quaternion.Euler(maze.mazeAngle);
            transform.rotation = target;
            transform.position = Vector3.zero;
            boxCollider2D.offset = new Vector2(0f, 7.2f);
        }
    }
    #endregion LOCAL METHODS
}
