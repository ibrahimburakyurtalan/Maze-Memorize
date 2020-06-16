using UnityEngine;

public class Review : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private GameObject ReviewPanel = null;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        EventManager.Instance.InitiateGame.AddListener(GameInitiated);
    }

    #endregion UNITY METHODS

    #region EVENT METHODS

    /*
     * Local method :   GameInitiated - Private
     * Description  :   1) 
     */
    private void GameInitiated()
    {
        // See description 1 for information.
        int playNumber = DataManager.Instance.playCounter;
        if (playNumber < 10) { return; }

        bool isReviewShown = DataManager.Instance.review;
        if (isReviewShown) { EventManager.Instance.InitiateGame.RemoveListener(GameInitiated); return; }

        ReviewPanel.SetActive(true);
        DataManager.Instance.SetReview();
        EventManager.Instance.InitiateGame.RemoveListener(GameInitiated);
    }
    #endregion EVENT METHODS
}
