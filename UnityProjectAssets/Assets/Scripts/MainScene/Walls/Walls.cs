using UnityEngine;

public class Walls : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private BoxCollider2D up = null;
    [SerializeField] private BoxCollider2D down = null;
    [SerializeField] private BoxCollider2D right = null;
    [SerializeField] private BoxCollider2D left = null;
    // Non-visible private constant attributes
    private const float referenceScreenY = 2960f;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets screen width and height values
     *                  2) Sets collider positions.
     */
    private void Start()
    {
        // See description 1 for information.
        float screenY = referenceScreenY / 100;
        float screenX = screenY * ((float)Screen.width / (float)Screen.height);

        // float screenX = (float) Screen.width / 100;     // 14.4
        // See description 2 for information.
        up.size = new Vector2(screenX, 1f);
        up.offset = new Vector2(0, (screenY / 2));

        down.size = new Vector2(screenX, 1f);
        down.offset = new Vector2(0, -(screenY / 2));

        right.size = new Vector2(1f, screenY);
        right.offset = new Vector2((screenX / 2), 0);

        left.size = new Vector2(1f, screenY);
        left.offset = new Vector2(-(screenX / 2), 0);
    }
    #endregion UNITY METHODS
}
