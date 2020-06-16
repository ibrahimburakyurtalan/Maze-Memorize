using UnityEngine;

public class BackgroundAsync : MonoBehaviour
{
    void Awake()
    {
        float ratio = 2960 / (float) Screen.height;
        float newWidth = ratio * (float) Screen.width;
        float scale = newWidth / 1440;
        transform.localScale = new Vector3(scale, 1f, 1f);
    }
}
