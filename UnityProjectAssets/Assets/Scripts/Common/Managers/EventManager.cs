using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventWithStringArgument : UnityEvent<string>
{
}

[System.Serializable]
public class UnityEventWithBoolArgument : UnityEvent<bool>
{
}

public class EventManager : MonoBehaviour
{
    // Public static attributes
    public static EventManager Instance;
    // Non-visible public attributes
    // Synchronous
    [HideInInspector] public UnityEvent InitiateLogo = null;
    [HideInInspector] public UnityEvent InitiateBegin = null;
    [HideInInspector] public UnityEvent InitiateTutorial = null;
    [HideInInspector] public UnityEvent InitiateGame = null;
    [HideInInspector] public UnityEvent InitiateLevel = null;           // Can restart
    [HideInInspector] public UnityEvent InitiatePlay = null;
    [HideInInspector] public UnityEvent InitiateEnableMask = null;      
    [HideInInspector] public UnityEvent InitiateEnd = null;
    [HideInInspector] public UnityEvent InitiateDisableMask = null;
    [HideInInspector] public UnityEvent InitiateFinish = null;
    // Asynchronous
    [HideInInspector] public UnityEvent InitiateMenu = null;
    [HideInInspector] public UnityEventWithStringArgument InitiateAsyncGame = null;
    [HideInInspector] public UnityEventWithBoolArgument InitiateColor = null;
    [HideInInspector] public UnityEvent InitiateExpand = null;
    [HideInInspector] public UnityEvent InitiateBuy = null;
    [HideInInspector] public UnityEvent InitiateRefreshPro = null;

    /*
     * Unity Method :   Awake - Private
     * Description  :   1) Public static instance assignment.
     *                  2) Events are created.
     *                      # InitiateLogo          -> Indicates the starting of the first scene.
     *                          $ Invokers          -> Tutorial, 
     *                      # InitiateBegin         -> Indicates the starting of the game.
     *                          $ Invokers          -> PlayButton
     *                      # InitiateTutorial      -> Indicates the beginning of the tutorial.
     *                          $ Invokers          -> PlayButton
     *                      # InitiateGame          -> Indicates the beginning of the gameplay.
     *                          $ Invokers          -> Tutorial, PlayButton
     *                      # InitiateLevel         -> Indicates the beginning of the level.
     *                          $ Invokers          -> Level
     *                      # InititatePlay         -> Indicate the begining of the playing.
     *                          $ Invokers          -> ReadyButton
     *                      # InitiateMask          -> Indicate the begining of the masking.
     *                          $ Invokers          -> Character
     *                      # InitiateEnd           -> Indicate the begining of the end.
     *                          $ Invokers          -> EndOfLevel
     *                      # InitiateFinish        -> Indicate the begining of the finish.
     *                          $ Invokers          -> NextButton
     *                      # InitiateMenuButton    -> Indicates the beginning of the main menu.
     *                  3) Application maximum frame rate is set 300.
     */
    private void Awake()
    {
        // See description 1 for information.
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // See description 2 for information
        InitiateLogo = new UnityEvent();
        InitiateBegin = new UnityEvent();
        InitiateTutorial = new UnityEvent();
        InitiateGame = new UnityEvent();
        InitiateLevel = new UnityEvent();
        InitiatePlay = new UnityEvent();
        InitiateEnableMask = new UnityEvent();
        InitiateEnd = new UnityEvent();
        InitiateDisableMask = new UnityEvent();
        InitiateFinish = new UnityEvent();
        InitiateMenu = new UnityEvent();
        InitiateAsyncGame = new UnityEventWithStringArgument();
        InitiateColor = new UnityEventWithBoolArgument();
        InitiateExpand = new UnityEvent();
        InitiateBuy = new UnityEvent();
        InitiateRefreshPro = new UnityEvent();
        // See description 3 for information
        Application.targetFrameRate = 300;
    }
}
