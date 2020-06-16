using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaze : MonoBehaviour
{
    // Non-visible private attributes
    private Animator animator = null;
    private SpriteRenderer sprite = null;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets animator and sprite renderer components.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        // See description 2 for information.
        EventManager.Instance.InitiateGame.AddListener(GameInitiated);
        EventManager.Instance.InitiateLevel.AddListener(LevelInitiated);
        EventManager.Instance.InitiatePlay.AddListener(PlayInitiated);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   GameInitiated - Private
     * Description  :   1) 
     */
    private void GameInitiated()
    {
        // See description 1 for information.
        animator.SetTrigger("Begin");
    }

    /*
     * Event method :   LevelInitiated - Private
     * Description  :   1) 
     */
    private void LevelInitiated()
    {
        // See description 1 for information
        animator.SetTrigger("Start");
    }
    
    /*
     * Event method :   PlayInitiated - Private
     * Description  :   1) 
     *                  2) 
     */
    private void PlayInitiated()
    {
        // See description 1 for information
        animator.SetTrigger("Play");
        // See description 2 for information
        sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   InitiateGame - Private
     * Description  :   1) 
     */
    private void InitiateGame()
    {
        // See description 1 for information
        EventManager.Instance.InitiatePlay.Invoke();
    }
    #endregion LOCAL METHODS
}
