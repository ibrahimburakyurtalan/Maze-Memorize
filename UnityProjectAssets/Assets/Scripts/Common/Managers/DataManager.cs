using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Public static attributes
    public static DataManager Instance;
    // Non-visible public attributes
    [HideInInspector] public string pro = null;     // lol for no, "faruk" for yes
    [HideInInspector] public string mode = null;    // melo for reg, buro for pro.
    [HideInInspector] public bool review = false;
    [HideInInspector] public bool tutorial = false;
    [HideInInspector] public bool reachedCap = false;
    [HideInInspector] public bool reachedProCap = false;
    [HideInInspector] public int levelCap = 160;
    [HideInInspector] public int levelProCap = 80;
    [HideInInspector] public int currentLevel = 0;
    [HideInInspector] public int currentProLevel = 0;
    [HideInInspector] public int previousLevel = 0;
    [HideInInspector] public int previousProLevel = 0;
    [HideInInspector] public int maxLevel = 0;
    [HideInInspector] public int maxProLevel = 0;
    [HideInInspector] public int sound = 1;
    [HideInInspector] public int playCounter = 0;
    // Private constant attributes
    private const string keyPro = "KEY_PRO";
    private const string keySound = "KEY_SOUND";
    private const string keyTutorial = "KEY_TUTORIAL";
    private const string keyReview = "KEY_REVIEW";
    private const string keyMode = "KEY_MODE";
    private const string keyLevel = "KEY_LEVEL";
    private const string keyProLevel = "KEY_PROLEVEL";
    private const string keyMaxLevel = "KEY_MAXLEVEL";
    private const string keyProMaxLevel = "KEY_PROMAXLEVEL";

    #region UNITY METHODS
    /*
     * Unity method :   Awake - Private
     * Description  :   1) Public static instance assignment.
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
        Instance.pro = Instance.GetStringKey(keyPro, "lol");
        Instance.mode = Instance.GetStringKey(keyMode, "melo");
        Instance.review = Instance.GetBooleanKey(keyReview);
        Instance.tutorial = Instance.GetBooleanKey(keyTutorial);
        Instance.currentLevel = Instance.GetIntegerKey(keyLevel, true);
        Instance.currentProLevel = Instance.GetIntegerKey(keyProLevel, true);
        Instance.maxLevel = Instance.GetIntegerKey(keyMaxLevel, true);
        Instance.maxProLevel = Instance.GetIntegerKey(keyProMaxLevel, true);
        Instance.sound = Instance.GetIntegerKey(keySound, false);

        // See description 3 for information
        if (string.Equals(Instance.mode, "buro"))
        {
            if (Instance.currentProLevel >= levelProCap) { reachedProCap = true; }
            else { reachedProCap = false; }
        }
        else
        {
            if (Instance.currentLevel >= levelCap) { reachedCap = true; }
            else { reachedCap = false; }
        }

    }
    #endregion UNITY METHODS

    #region LOCAL METHODS
    /*
     * Local Method :   GetKey - Private
     * Return       :   Boolean
     * Description  :   1) Checks PlayerPrefs has a parameter key.
     */
    private bool GetBooleanKey(string key)
    {
        // See description 1 for information.
        if (PlayerPrefs.HasKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
     * Local Method :   GetKey - Private
     * Return       :   Integer
     * Description  :   1) Checks PlayerPrefs has a parameter key.
     */
    private int GetIntegerKey(string key, bool isZero)
    {
        // See description 1 for information.
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            if (isZero)
            {
                PlayerPrefs.SetInt(key, 0);
                PlayerPrefs.Save();
                return 0;
            }
            else
            {
                PlayerPrefs.SetInt(key, 1);
                PlayerPrefs.Save();
                return 1;
            }
        }
    }

    /*
     * Local Method :   GetStringKey - Private
     * Return       :   String
     * Description  :   1) Checks PlayerPrefs has a parameter key.
     */
    private string GetStringKey(string key, string def)
    {
        // See description 1 for information.
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            PlayerPrefs.SetString(key, def);
            PlayerPrefs.Save();
            return def;
        }
    }

    /*
     * Local Method :   SetTutorial - Public
     * Description  :   1) Tutorial key is set and saved. Variable is updated.
     */
    public void SetTutorial()
    {
        // See description 1 for information.
        PlayerPrefs.SetInt(keyTutorial, 0);
        PlayerPrefs.Save();
        Instance.tutorial = true;
    }

    /*
     * Local Method :   SetReview - Public
     * Description  :   1) Tutorial key is set and saved. Variable is updated.
     */
    public void SetReview()
    {
        // See description 1 for information.
        PlayerPrefs.SetInt(keyReview, 0);
        PlayerPrefs.Save();
        Instance.review = true;
    }

    /*
     * Local Method :   SetLevel - Public
     * Description  :   1) Level key is set and saved. Variable is updated.
     */
    public void SetLevel(string setType, string mode)
    {
        // See description 1 for information.
        if (string.Equals(mode, "buro")) { Instance.previousProLevel = Instance.currentProLevel; }
        else { Instance.previousLevel = Instance.currentLevel; }

        switch (setType)
        {
            case "increment":
                if (string.Equals(mode, "buro"))
                {
                    Instance.currentProLevel++;

                    if (Instance.maxProLevel < Instance.currentProLevel)
                    {
                        Instance.maxProLevel = Instance.currentProLevel;
                        PlayerPrefs.SetInt(keyProMaxLevel, Instance.maxProLevel);
                    }

                    if (Instance.currentProLevel >= Instance.levelProCap)
                    {
                        reachedProCap = true;
                    }
                    else
                    {
                        reachedProCap = false;
                    }
                }
                else
                {
                    Instance.currentLevel++;

                    if (Instance.maxLevel < Instance.currentLevel)
                    {
                        Instance.maxLevel = Instance.currentLevel;
                        PlayerPrefs.SetInt(keyMaxLevel, Instance.maxLevel);
                    }

                    if (Instance.currentLevel >= Instance.levelCap)
                    {
                        reachedCap = true;
                    }
                    else
                    {
                        reachedCap = false;
                    }
                }
                break;

            case "next":
                if (string.Equals(mode, "buro"))
                {
                    if (Instance.maxProLevel == Instance.levelProCap)
                    {
                        Instance.currentProLevel++;

                        if (Instance.currentProLevel > Instance.maxProLevel)
                        {
                            Instance.currentProLevel = 0;
                            reachedProCap = false;
                        }
                        else if (Instance.currentProLevel == Instance.maxProLevel) {
                            reachedProCap = true;
                        }
                        else
                        {
                            reachedProCap = false;
                        }
                    }
                    else
                    {
                        Instance.currentProLevel++;
                        reachedProCap = false;
                        if (Instance.currentProLevel > Instance.maxProLevel)
                        {
                            Instance.currentProLevel = 0;
                        }
                        else
                        {
                            // nop
                        }
                    }
                }
                else
                {
                    if (Instance.maxLevel == Instance.levelCap)
                    {
                        Instance.currentLevel++;

                        if (Instance.currentLevel > Instance.maxLevel)
                        {
                            Instance.currentLevel = 0;
                            reachedCap = false;
                        }
                        else if (Instance.currentLevel == Instance.maxLevel)
                        {
                            reachedCap = true;
                        }
                        else
                        {
                            reachedCap = false;
                        }
                    }
                    else
                    {
                        Instance.currentLevel++;
                        reachedCap = false;
                        if (Instance.currentLevel > Instance.maxLevel)
                        {
                            Instance.currentLevel = 0;
                        }
                        else
                        {
                            // nop
                        }
                    }
                }
                break;

            case "previous":
                if (string.Equals(mode, "buro"))
                {
                    if (Instance.maxProLevel == Instance.levelProCap)
                    {
                        if (Instance.currentProLevel == 0)
                        {
                            Instance.currentProLevel = Instance.maxProLevel;
                            reachedProCap = true;
                        }
                        else
                        {
                            Instance.currentProLevel--;
                            reachedProCap = false;
                        }
                    }
                    else
                    {
                        reachedProCap = false;
                        if (Instance.currentProLevel == 0)
                        {
                            Instance.currentProLevel = Instance.maxProLevel;
                        }
                        else
                        {
                            Instance.currentProLevel--;
                            
                        }
                    }
                }
                else
                {
                    if (Instance.maxLevel == Instance.levelCap)
                    {
                        if (Instance.currentLevel == 0)
                        {
                            Instance.currentLevel = Instance.maxLevel;
                            reachedCap = true;
                        }
                        else
                        {
                            Instance.currentLevel--;
                            reachedCap = false;
                        }
                    }
                    else
                    {
                        reachedCap = false;
                        if (Instance.currentLevel == 0)
                        {
                            Instance.currentLevel = Instance.maxLevel;
                        }
                        else
                        {
                            Instance.currentLevel--;

                        }
                    }
                }
                break;

            default:
                // No operation
                break;
        }

        if (string.Equals(mode, "buro")) { PlayerPrefs.SetInt(keyProLevel, Instance.currentProLevel); }
        else { PlayerPrefs.SetInt(keyLevel, Instance.currentLevel); }
        PlayerPrefs.Save();
        EventManager.Instance.InitiateColor.Invoke(false);
    }

    /*
     * Local Method :   SetSound - Public
     * Description  :   1) Sound key is set and saved. Variable is updated.
     */
    public void SetSound(bool mute)
    {
        // See description 1 for information.
        if(mute)
        {
            Instance.sound = 0;
        }
        else
        {
            Instance.sound = 1;
        }

        PlayerPrefs.SetInt(keySound, Instance.sound);
        PlayerPrefs.Save();
    }

    /*
     * Local Method :   SetPro - Public
     * Description  :   1) Pro key is set and saved. Variable is updated.
     */
    public void SetPro()
    {
        // See description 1 for information.
        Instance.pro = "faruk";
        PlayerPrefs.SetString(keyPro, "faruk");
        PlayerPrefs.Save();
    }

    /*
     * Local Method :   SetMode - Public
     * Description  :   1) Mode key is set and saved. Variable is updated.
     */
    public void SetMode()
    {
        // See description 1 for information.
        if (string.Equals(mode, "buro")) { Instance.mode = "melo"; }
        else { Instance.mode = "buro"; }

        PlayerPrefs.SetString(keyMode, Instance.mode);
        PlayerPrefs.Save();
        EventManager.Instance.InitiateColor.Invoke(true);
    }

    /*
     * Local Method :   SetPlayCounter - Public
     * Description  :   1) Sound key is set and saved. Variable is updated.
     */
    public void IncrementPlayCounter()
    {
        // See description 1 for information.
        Instance.playCounter++;
    }

    #endregion LOCAL METHODS
}
