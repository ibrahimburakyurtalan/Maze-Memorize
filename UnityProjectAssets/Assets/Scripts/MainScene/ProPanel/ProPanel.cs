using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class ProPanel : MonoBehaviour
{
    // Visible private attributes
    [SerializeField] private GameObject proPanel = null;
    [SerializeField] private GameObject successfulPanel = null;
    [SerializeField] private GameObject failedPanel = null;
    [SerializeField] private TextMeshProUGUI priceText = null;
    [SerializeField] private TextMeshProUGUI reasonText = null;

    #region UNITY METHOD
    /*
     * Local method :   Start - Private
     * Description  :   1)
     */
    private void Start()
    {
        // See description 1 for information.
        priceText.text = "buy - " + IAPManager.Instance.price;
        Debug.Log(IAPManager.Instance.price);
    }
    #endregion UNITY METHOD

    #region LOCAL METHOD
    /*
     * Local method :   ColorLerp - Private
     * Description  :   1) 
     */
    public void OnClickedButton(string button)
    {
        // See description 1 for information.
        AudioManager.Instance.Play("uiClick");

        switch (button)
        {
            case "back":
                gameObject.SetActive(false);
                proPanel.SetActive(true);
                successfulPanel.SetActive(false);
                failedPanel.SetActive(false);
                break;
            case "buy":
                if (!IAPManager.Instance.Buy(PassCallback, FailCallback)) 
                {
                    reasonText.text = "Product unavailable";
                    proPanel.SetActive(false);
                    successfulPanel.SetActive(false);
                    failedPanel.SetActive(true);
                }
                break;
            default:
                gameObject.SetActive(false);
                proPanel.SetActive(true);
                successfulPanel.SetActive(false);
                failedPanel.SetActive(false);
                break;
        }
    }

    /*
     * Local method :   PassCallback - Private
     * Description  :   1) 
     */
    public void PassCallback()
    {
        // See description 1 for information.
        DataManager.Instance.SetPro();
        EventManager.Instance.InitiateBuy.Invoke();
        proPanel.SetActive(false);
        successfulPanel.SetActive(true);
        failedPanel.SetActive(false);
    }

    /*
     * Local method :   FailCallback - Private
     * Description  :   1) 
     */
    public void FailCallback()
    {
        // See description 1 for information.
        switch (IAPManager.Instance.reason)
        {
            case PurchaseFailureReason.PurchasingUnavailable:
                reasonText.text = "Purchasing unavailable";
                break;
            case PurchaseFailureReason.ExistingPurchasePending:
                reasonText.text = "Existing purchase pending";
                break;
            case PurchaseFailureReason.ProductUnavailable:
                reasonText.text = "Product unavailable";
                break;
            case PurchaseFailureReason.SignatureInvalid:
                reasonText.text = "Signature invalid";
                break;
            case PurchaseFailureReason.UserCancelled:
                reasonText.text = "User cancelled";
                break;
            case PurchaseFailureReason.PaymentDeclined:
                reasonText.text = "Payment declined";
                break;
            case PurchaseFailureReason.DuplicateTransaction:
                reasonText.text = "Duplicate transaction";
                break;
            case PurchaseFailureReason.Unknown:
                reasonText.text = "Unknown";
                break;
            default:
                reasonText.text = "Unknown";
                break;
        }
        proPanel.SetActive(false);
        successfulPanel.SetActive(false);
        failedPanel.SetActive(true);
    }
    #endregion LOCAL METHOD
}
