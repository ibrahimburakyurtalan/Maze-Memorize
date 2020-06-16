using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    // Public static attributes
    public static AdManager Instance;
    // Visible private attributes
    [SerializeField] bool testMode = false;
    // Constant private attributes
    private const string appleAppStoreId = "3584790";
    private const string googlePlayStoreId = "3584791";
    private const string interstitialAd = "video";
    private const string rewardedAd = "rewardedVideo";
    // Non-visible private attributes
    private Action callbackAction = null;

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
     * Description  :   1) 
     */
    private void Start()
    {
        // See description 1 for information.
        Advertisement.AddListener(this);

        if (testMode) { Advertisement.Initialize(googlePlayStoreId, testMode); return; };

        if (Application.platform == RuntimePlatform.IPhonePlayer)   
        {
            Advertisement.Initialize(appleAppStoreId, testMode);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize(googlePlayStoreId, testMode);
        }
        else
        {
            // Delete in future
        }
    }

    /*
     * Unity method :   OnUnityAdsDidFinish - Public
     * Description  :   1) 
     */
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == interstitialAd)
        {
            callbackAction?.Invoke();
            callbackAction = null;
        }
        else if (placementId == rewardedAd)
        {
            if (showResult == ShowResult.Finished) { callbackAction?.Invoke(); callbackAction = null; }
            else { callbackAction = null; }
        }
        else
        {
            callbackAction = null;
        }
    }

    /*
     * Unity method :   OnUnityAdsReady - Public
     * Description  :   1) 
     */
    public void OnUnityAdsReady(string placementId)
    {
    }

    /*
     * Unity method :   OnUnityAdsDidError - Public
     * Description  :   1) 
     */
    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("OnUnityAdsDidError: " + message);
    }

    /*
     * Unity method :   OnUnityAdsDidStart - Public
     * Description  :   1) 
     */
    public void OnUnityAdsDidStart(string placementId)
    {
    }
    #endregion UNITY METHODS

    #region LOCAL METHODS
    /*
     * Unity method :   CheckInterstitialAd - Public
     * Description  :   1) 
     */
    public bool CheckInterstitialAd()
    {
        // See description 1 for information.
        if (DataManager.Instance.playCounter % 4 != 0) { return false; }
        return true;
    }

    /*
     * Unity method :   PlayInterstitialAd - Public
     * Description  :   1) 
     */
    public bool PlayInterstitialAd(Action action)
    {
        if (!Advertisement.IsReady(interstitialAd)) { Debug.Log("Not ready: Interstitial"); return false; }
        callbackAction = action;
        Advertisement.Show(interstitialAd);
        return true;

    }

    /*
     * Unity method :   CheckRewardedAd - Public
     * Description  :   1) 
     */
    public bool CheckRewardedAd()
    {
        // See description 1 for information.
        if (!Advertisement.IsReady(rewardedAd)) { Debug.Log("Not ready: Rewarded"); return false; }
        return true;
    }

    /*
     * Unity method :   PlayRewardedAd - Public
     * Description  :   1) 
     */
    public bool PlayRewardedAd(Action action)
    {
        // See description 1 for information.
        if (!Advertisement.IsReady(rewardedAd)) { Debug.Log("Not ready: Rewarded"); return false; }
        callbackAction = action;
        Advertisement.Show(rewardedAd);
        return true;

    }
    #endregion LOCAL METHODS
}
