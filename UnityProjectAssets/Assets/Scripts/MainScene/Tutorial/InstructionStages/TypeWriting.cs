using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriting : MonoBehaviour
{
    // Visible private attributes.
    [SerializeField] private TextMeshProUGUI textComponent = null;
    [SerializeField] private string message = null;
    [SerializeField] private float period = 0;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Coroutine starts.
     */
    private void Start()
    {
        // See description 1 for information.
        StartCoroutine(Type());
    }
    #endregion UNITY METHODS

    #region LOCAL METHODS
    /*
     * Local method :   EndOfAnimation - Private
     * Description  :   1) The input text will be written letter by letter in this loop.

     */
    private IEnumerator Type()
    {
        // See description 1 for information.
        string text = null;
        for(int i = 0 ; i <= message.Length ; i++)
        {
            text = message.Substring(0, i);
            textComponent.text = text;
            yield return new WaitForSeconds(period);
        }
    }
    #endregion LOCAL METHODS

}
