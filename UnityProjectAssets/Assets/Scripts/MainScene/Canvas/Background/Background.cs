using UnityEngine;

public class Background : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private Transform canvas = null;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) The width value of gameobject is set as canvas width-height.
     */
    private void Start()
    {
        // See description 1 for information.
        RectTransform rectTransformCanvas = canvas.GetComponent<RectTransform>();
        RectTransform rectTransformBackground = transform.GetComponent<RectTransform>();
        Vector2 newWidthAndHeight = new Vector2(rectTransformCanvas.sizeDelta.x, rectTransformCanvas.sizeDelta.y);
        rectTransformBackground.sizeDelta = newWidthAndHeight;
    }
    #endregion UNITY METHODS

}
