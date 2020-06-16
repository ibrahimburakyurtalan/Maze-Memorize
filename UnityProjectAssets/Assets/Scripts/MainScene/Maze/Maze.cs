using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Maze : MonoBehaviour
{
    // Non-visible public attributes
    [HideInInspector] public Vector3 mazeAngle = Vector3.zero;
    // Visible private attributes
    [SerializeField] private SpriteRenderer maze = null;
    [SerializeField] private SpriteRenderer pointers = null;
    [SerializeField] private Sprite[] mazes = null;
    [SerializeField] private Sprite[] proMazes = null;
    [SerializeField] private Sprite[] mazePointers = null;
    [SerializeField] private Sprite end = null;
    // Non-visible private attributes
    private Animator animator = null;
    private PolygonCollider2D polygonCollider2D = null;
    private bool isMazeActive = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets animator component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        // See description 2 for information.
        EventManager.Instance.InitiateLevel.AddListener(LevelInitiated);
        EventManager.Instance.InitiateEnableMask.AddListener(MaskEnableInitiated);
        EventManager.Instance.InitiateDisableMask.AddListener(MaskDisableInitiated);
        EventManager.Instance.InitiateFinish.AddListener(FinishInitiated);
        EventManager.Instance.InitiateAsyncGame.AddListener(RestartInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1) Maze and pointers sprite rendereres are setting with the level value as index.
     *                  2) Animator is triggered with the "Begin" parameter.
     */
    private void LevelInitiated()
    {
        // See description 1 for information.
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro"))
        {
            bool cap = DataManager.Instance.reachedProCap;
            if (cap)
            {
                maze.sprite = end;
                pointers.enabled = false;
                mazeAngle = SetAngle(true);
            }
            else
            {
                maze.sprite = proMazes[DataManager.Instance.currentProLevel];
                pointers.enabled = true;
                pointers.sprite = mazePointers[(DataManager.Instance.currentProLevel / 2) % 2];
                mazeAngle = SetAngle(false);
            }

        }
        else
        {
            bool cap = DataManager.Instance.reachedCap;
            if (cap)
            {
                maze.sprite = end;
                pointers.enabled = false;
                mazeAngle = SetAngle(true);
            }
            else
            {
                maze.sprite = mazes[DataManager.Instance.currentLevel];
                pointers.enabled = true;
                pointers.sprite = mazePointers[(DataManager.Instance.currentLevel / 2) % 2];
                mazeAngle = SetAngle(false);
            }
        }
        
        Quaternion target = Quaternion.Euler(mazeAngle);
        transform.rotation = target;

        // See description 2 for information.
        animator.SetTrigger("Begin");
        isMazeActive = true;
    }

    /*
     * Event method :   MaskInititated - Private
     * Description  :   1) Mask interactions are setting as "Visible inside mask".
     *                  2) Polygon collider 2D component is added to maze game object.
     */
    private void MaskEnableInitiated()
    {
        // See description 1 for information.
        maze.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        pointers.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        // See description 2 for information.
        polygonCollider2D = maze.gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
    }

    /*
     * Event method :   MaskInititated - Private
     * Description  :   1) Mask interactions are setting as "None".
     *                  2) Polygon collider 2D component is removed from the game object.
     */
    private void MaskDisableInitiated()
    {
        // See description 1 for information.
        maze.maskInteraction = SpriteMaskInteraction.None;
        pointers.maskInteraction = SpriteMaskInteraction.None;
        // See description 2 for information.
        Destroy(polygonCollider2D);
    }

    /*
     * Event method :   FinishInitiated - Private
     * Description  :   1) 
     */
    private void FinishInitiated()
    {
        // See description 1 for information.
        maze.maskInteraction = SpriteMaskInteraction.None;
        pointers.maskInteraction = SpriteMaskInteraction.None;
        animator.SetTrigger("End");
        isMazeActive = false;

    }

    /*
     * Event method :   RestartInitiated - Private
     * Description  :   1) 
     */
    private void RestartInitiated(string eventType)
    {
        // See description 1 for information.
        if (isMazeActive)
        {
            switch (eventType)
            {
                case "restart":
                    animator.SetTrigger("Restart");
                    break;
                    
                case "next":
                    animator.SetTrigger("Next");
                    break;
                    
                case "previous":
                    animator.SetTrigger("Previous");
                    break;

                case "pro":
                    animator.SetTrigger("Pro");
                    break;
            }
            isMazeActive = false;
        }
        else
        {
            // Do nothing
        }


    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   SetAngle - Private
     * Description  :   1) 
     */
    private Vector3 SetAngle(bool reset)
    {
        if (reset) { Vector3 returnVal = Vector3.zero; return returnVal; }

        bool upOrDown = (Random.value > 0.5f);
        if (upOrDown)
        {
            float angle = Random.Range(-50f, 60f);
            return new Vector3(0f, 0f, angle);
        }
        else
        {
            float angle = Random.Range(130f, 240f);
            return new Vector3(0f, 0f, angle);
        }
    }

    /*
     * Local method :   NextLevel - Private
     * Description  :   1) 
     */
    private void NextLevel()
    {
        string mode = DataManager.Instance.mode;
        DataManager.Instance.SetLevel("increment", mode);
        DataManager.Instance.IncrementPlayCounter();

        bool isProActivated = string.Equals(DataManager.Instance.pro, "faruk");
        if (isProActivated) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdPlayable = AdManager.Instance.CheckInterstitialAd();
        if (!isAdPlayable) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdReady = AdManager.Instance.PlayInterstitialAd(AdCallback);
        if (!isAdReady) { EventManager.Instance.InitiateGame.Invoke(); return; }

    }

    /*
     * Local method :   Restart - Private
     * Description  :   1) 
     */
    private void Restart()
    {
        string mode = DataManager.Instance.mode;
        DataManager.Instance.SetLevel("dontTouch", mode);
        DataManager.Instance.IncrementPlayCounter();

        bool isProActivated = string.Equals(DataManager.Instance.pro, "faruk");
        if (isProActivated) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdPlayable = AdManager.Instance.CheckInterstitialAd();
        if (!isAdPlayable) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdReady = AdManager.Instance.PlayInterstitialAd(AdCallback);
        if (!isAdReady) { EventManager.Instance.InitiateGame.Invoke(); return; }

    }

    /*
     * Local method :   Restart - Private
     * Description  :   1) 
     */
    private void Next()
    {
        string mode = DataManager.Instance.mode;
        DataManager.Instance.SetLevel("next", mode);
        DataManager.Instance.IncrementPlayCounter();

        bool isProActivated = string.Equals(DataManager.Instance.pro, "faruk");
        if (isProActivated) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdPlayable = AdManager.Instance.CheckInterstitialAd();
        if (!isAdPlayable) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdReady = AdManager.Instance.PlayInterstitialAd(AdCallback);
        if (!isAdReady) { EventManager.Instance.InitiateGame.Invoke(); return; }

    }

    /*
     * Local method :   Restart - Private
     * Description  :   1) 
     */
    private void Previous()
    {
        string mode = DataManager.Instance.mode;
        DataManager.Instance.SetLevel("previous", mode);
        DataManager.Instance.IncrementPlayCounter();

        bool isProActivated = string.Equals(DataManager.Instance.pro, "faruk");
        if (isProActivated) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdPlayable = AdManager.Instance.CheckInterstitialAd();
        if (!isAdPlayable) { EventManager.Instance.InitiateGame.Invoke(); return; }

        bool isAdReady = AdManager.Instance.PlayInterstitialAd(AdCallback);
        if (!isAdReady) { EventManager.Instance.InitiateGame.Invoke(); return; }

    }

    /*
     * Local method :   Pro - Private
     * Description  :   1) 
     */
    private void Pro()
    {
        DataManager.Instance.SetMode();
        EventManager.Instance.InitiateGame.Invoke();
    }

    /*
     * Local method :   AdCallback - Public
     * Description  :   1) 
     *                  2) 
     */
    private void AdCallback()
    {
        EventManager.Instance.InitiateGame.Invoke();
    }

    /*
     * Local method :   RefreshPro - Public
     * Description  :   1) 
     *                  2) 
     */
    private void RefreshPro()
    {
        EventManager.Instance.InitiateRefreshPro.Invoke();
    }

    #endregion LOCAL METHODS

}
