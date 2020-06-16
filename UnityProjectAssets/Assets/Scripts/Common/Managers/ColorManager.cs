using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // Non-visible public attributes
    [HideInInspector] public Color32 oldGradientStartColor = new Color32(111, 180, 166, 255);
    [HideInInspector] public Color32 oldGradientEndColor = new Color32(17, 45, 78, 255);
    [HideInInspector] public Color32 oldMainColor = new Color32(240, 236, 226, 255);
    [HideInInspector] public Color32 oldCoverColor = new Color32(251, 255, 193, 255);
    [HideInInspector] public Color32 newGradientStartColor = Color.black;
    [HideInInspector] public Color32 newGradientEndColor = Color.black;
    [HideInInspector] public Color32 newMainColor = Color.black;
    [HideInInspector] public Color32 newCoverColor = Color.black;
    // Non-visible private attributes
    private Color32[,] colors = {   { new Color32(111, 180, 166, 255), new Color32(17, 45, 78, 255) ,new Color32(240, 236, 226, 255), new Color32(251, 255, 193, 255) },
                                    { new Color32(255, 107, 53, 255), new Color32(0, 78, 137, 255), new Color32(239, 239, 208, 255), new Color32(255, 222, 124, 255) },
                                    { new Color32(76, 76, 71, 255), new Color32(193, 73, 83, 255), new Color32(229, 220, 197, 255), new Color32(143, 255, 158, 255) },
                                    { new Color32(240, 93, 94, 255), new Color32(15, 113, 115, 255), new Color32(231, 236, 239, 255), new Color32(255, 157, 247, 255) },
                                    { new Color32(85, 140, 140, 255), new Color32(130, 32, 74, 255), new Color32(239, 247, 255, 255), new Color32(232, 219, 125, 255) },
                                    { new Color32(239, 71, 111, 255), new Color32(38, 84, 124, 255), new Color32(252, 252, 252, 255), new Color32(133, 255, 195, 255) },
                                    { new Color32(128, 128, 128, 255), new Color32(84, 35, 68, 255), new Color32(251, 250, 248, 255), new Color32(161, 252, 255, 255) },
                                    { new Color32(175, 91, 91, 255), new Color32(24, 48, 89, 255), new Color32(246, 244, 243, 255), new Color32(161, 207, 255, 255) },
                                };
    // Public static attributes
    public static ColorManager Instance;

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
    }

    /*
     * Unity method :   Start - Private
     * Description  :   1) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateColor.AddListener(ColorInitiated);
        Instance.ColorInitiated(false);
    }
    #endregion UNITY METHODS


    #region EVENT METHODS
    /*
     * Event method :   ColorInitiated - Private
     * Description  :   1) Event listeners are created.
     */
    public void ColorInitiated(bool isSwitched)
    {
        string mode = DataManager.Instance.mode;
        if (string.Equals(mode, "buro"))
        {
            if (isSwitched)
            {
                Instance.oldGradientStartColor = Instance.colors[DataManager.Instance.currentLevel % 8, 0];
                Instance.oldGradientEndColor = Instance.colors[DataManager.Instance.currentLevel % 8, 1];
                Instance.oldMainColor = Instance.colors[DataManager.Instance.currentLevel % 8, 2];
                Instance.oldCoverColor = Instance.colors[DataManager.Instance.currentLevel % 8, 3];
            }
            else
            {
                Instance.oldGradientStartColor = Instance.colors[DataManager.Instance.previousProLevel % 8, 0];
                Instance.oldGradientEndColor = Instance.colors[DataManager.Instance.previousProLevel % 8, 1];
                Instance.oldMainColor = Instance.colors[DataManager.Instance.previousProLevel % 8, 2];
                Instance.oldCoverColor = Instance.colors[DataManager.Instance.previousProLevel % 8, 3];
            }

            Instance.newGradientStartColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 0];
            Instance.newGradientEndColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 1];
            Instance.newMainColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 2];
            Instance.newCoverColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 3];
        }
        else
        {
            if (isSwitched)
            {
                Instance.oldGradientStartColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 0];
                Instance.oldGradientEndColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 1];
                Instance.oldMainColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 2];
                Instance.oldCoverColor = Instance.colors[DataManager.Instance.currentProLevel % 8, 3];
            }
            else
            {
                Instance.oldGradientStartColor = Instance.colors[DataManager.Instance.previousLevel % 8, 0];
                Instance.oldGradientEndColor = Instance.colors[DataManager.Instance.previousLevel % 8, 1];
                Instance.oldMainColor = Instance.colors[DataManager.Instance.previousLevel % 8, 2];
                Instance.oldCoverColor = Instance.colors[DataManager.Instance.previousLevel % 8, 3];
            }

            Instance.newGradientStartColor = Instance.colors[DataManager.Instance.currentLevel % 8, 0];
            Instance.newGradientEndColor = Instance.colors[DataManager.Instance.currentLevel % 8, 1];
            Instance.newMainColor = Instance.colors[DataManager.Instance.currentLevel % 8, 2];
            Instance.newCoverColor = Instance.colors[DataManager.Instance.currentLevel % 8, 3];
        }
    }
    #endregion EVENT METHODS

}
