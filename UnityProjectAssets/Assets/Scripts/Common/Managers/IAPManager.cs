using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    // Public static attributes
    public static IAPManager Instance;
    // Private static attributes
    private static IStoreController m_StoreController = null;
    private static IExtensionProvider m_StoreExtensionProvider = null;
    private static string kProductIDNonConsumable = "com.gurugamestudios.mazememorize.pro";
    // Non-visible public attributes
    [HideInInspector] public bool isIAPInitialized = false;
    [HideInInspector] public string price = null;
    [HideInInspector] public PurchaseFailureReason reason;
    // Non-visible private attributes
    private UnityAction passCallbackAction = null;
    private UnityAction failCallbackAction = null;

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
     * Description  :   1) Public static instance assignment.
     */
    private void Start()
    {
        // See description 1 for information.
        if (m_StoreController != null && m_StoreExtensionProvider != null) { return; }

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    /*
     * Unity method :   OnInitialized - Private
     * Description  :   1) Called when Unity IAP is ready to make purchases.
     */
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        isIAPInitialized = true;
        price = m_StoreController.products.WithID(kProductIDNonConsumable).metadata.localizedPrice + " " + m_StoreController.products.WithID(kProductIDNonConsumable).metadata.isoCurrencyCode;
    }

    /*
     * Unity method :   OnInitializeFailed - Private
     * Description  :   1) Called when Unity IAP encounters an unrecoverable initialization error.
     *                     Note that this will not be called if Internet is unavailable; Unity IAP
     *                     will attempt initialization until it becomes available.
     */
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        isIAPInitialized = false;
    }

    /*
     * Unity method :   PurchaseProcessingResult - Private
     * Description  :   1) Called when a purchase completes.
    *                      May be called at any time after OnInitialized().
     */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (passCallbackAction != null) { passCallbackAction(); }
        else { DataManager.Instance.SetPro(); }
        return PurchaseProcessingResult.Complete;
    }

    /*
     * Unity method :   OnPurchaseFailed - Private
     * Description  :   1) Called when a purchase fails.
     */
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        reason = p;
        failCallbackAction?.Invoke();
    }
    #endregion UNITY METHODS

    #region LOCAL METHODS
    /*
     * Local method :   Buy - Private
     * Description  :   1) Called when a purchase fails.
     */
    public bool Buy(UnityAction passAction, UnityAction failAction)
    {
        Product product = m_StoreController.products.WithID(kProductIDNonConsumable);

        if (product != null && product.availableToPurchase)
        {
            m_StoreController.InitiatePurchase(product);
            passCallbackAction = passAction;
            failCallbackAction = failAction;
            return true;
        }
        else
        {
            passCallbackAction = null;
            failCallbackAction = null;
            return false;
        }
    }
    #endregion LOCAL METHODS
}
