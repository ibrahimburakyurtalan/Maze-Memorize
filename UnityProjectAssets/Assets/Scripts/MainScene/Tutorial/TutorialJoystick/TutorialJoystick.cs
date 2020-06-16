using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialJoystick : MonoBehaviour
{
    // Public attributes.
    [HideInInspector] public Vector3 direction = Vector3.zero;

    // Visible private attributes.
    [SerializeField] private Transform cover = null;
    [SerializeField] private Transform navigator = null;

    // Non-visible private attributes.
    private bool touch = false;
    private enum TouchPhases { Idle, TouchBegin, Touch };
    private TouchPhases touchPhase = TouchPhases.Idle;
    private Vector3 initialTouchPosition = Vector3.zero;
    private Vector3 touchPosition = Vector3.zero;
    private Vector3 offset = Vector3.zero;
    private SpriteRenderer coverSprite;
    private SpriteRenderer navigatorSprite;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) 
     */
    private void Start()
    {
        // See description 1 for information.
        coverSprite = cover.GetComponent<SpriteRenderer>();
        navigatorSprite = navigator.GetComponent<SpriteRenderer>();
    }

    /*
     * Unity method :   Update - Private
     * Description  :   1)
     *                  2) 
     */
    private void Update()
    {
        // See description 1 for information.
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject())
#else
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#endif
            {
                touchPhase = TouchPhases.Idle;
                touch = false;
            }
            else
            {
                touchPhase = TouchPhases.TouchBegin;
                touch = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (touch)
            {
                touchPhase = TouchPhases.Touch;
            }
            else
            {
                touchPhase = TouchPhases.Idle;
            }
        }
        else
        {
            touchPhase = TouchPhases.Idle;
        }

        // See description 2 for information.
        switch (touchPhase)
        {
            case TouchPhases.Idle:
                coverSprite.enabled = false;
                navigatorSprite.enabled = false;
                direction = Vector3.zero;
                break;
            case TouchPhases.TouchBegin:
                initialTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                coverSprite.enabled = true;
                navigatorSprite.enabled = true;
                cover.transform.position = new Vector3(initialTouchPosition.x, initialTouchPosition.y, 0);
                navigator.transform.position = new Vector3(initialTouchPosition.x, initialTouchPosition.y, 0);
                direction = Vector3.zero;
                break;
            case TouchPhases.Touch:
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                coverSprite.enabled = true;
                navigatorSprite.enabled = true;
                navigator.transform.position = new Vector3(initialTouchPosition.x + direction.x * 1.5f, initialTouchPosition.y + direction.y * 1.5f, 0);
                offset = touchPosition - initialTouchPosition;
                direction = Vector3.ClampMagnitude(offset, 1.00f);
                break;
            default:
                coverSprite.enabled = false;
                navigatorSprite.enabled = false;
                direction = Vector3.zero;
                break;
        }
    }
    #endregion UNITY METHODS

}
