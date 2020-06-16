using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    // Enumeration
    enum componentType { Material, Particle, Sprite, Image, Text, Trail };
    // Visible private attributes
    [SerializeField] private componentType component = componentType.Material;
    [SerializeField] private bool onlyOnce = false;
    [SerializeField] private bool isCover = false;
    [SerializeField] private bool wantLerp = false;
    [SerializeField] private bool proSens = false;

    #region UNITY METHODS
    /*
     * Unity method :   Start - Private
     * Description  :   1) Gets animator component.
     *                  2) Event listeners are created.
     */
    private void Start()
    {
        // See description 1 for information.
        if (!onlyOnce)
        {
            EventManager.Instance.InitiateGame.AddListener(GameInitiated);
            EventManager.Instance.InitiateBuy.AddListener(BuyInitiated);
        }
        // See description 2 for information.
        InstantColor(component, true);
    }
    #endregion UNITY METHODS

    #region EVENT METHODS
    /*
     * Event method :   FinishInitiated - Public
     * Description  :   1) 
     */
    private void GameInitiated()
    {
        if (wantLerp)
        {
            StartCoroutine(LerpColor(component));
        }
        else
        {
            InstantColor(component, false);
        }
    }

    /*
     * Event method :   BuyInitiated - Public
     * Description  :   1) 
     */
    private void BuyInitiated()
    {
        if (!proSens) { return; }

        if (wantLerp)
        {
            StartCoroutine(LerpColor(component));
        }
        else
        {
            InstantColor(component, false);
        }
    }
    #endregion EVENT METHODS

    #region LOCAL METHODS
    /*
     * Local method :   ColorLerp - Private
     * Description  :   1) 
     */
    private void InstantColor(componentType type, bool isDefault)
    {
        switch (type)
        {
            case componentType.Material:
                Material material = GetComponent<Image>().material;
                if (isDefault)
                {
                    material.SetColor("_StartColor", ColorManager.Instance.oldGradientStartColor);
                    material.SetColor("_EndColor", ColorManager.Instance.oldGradientEndColor);
                }
                else
                {
                    material.SetColor("_StartColor", ColorManager.Instance.newGradientStartColor);
                    material.SetColor("_EndColor", ColorManager.Instance.newGradientEndColor);
                }
                break;

            case componentType.Particle:
                ParticleSystem particleSystem = GetComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = particleSystem.main;
                if (isDefault)
                {
                    mainModule.startColor = (Color)ColorManager.Instance.oldMainColor;
                }
                else
                {
                    mainModule.startColor = (Color)ColorManager.Instance.newMainColor;
                }
                break;

            case componentType.Sprite:
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (isCover)
                {
                    if (isDefault)
                    {
                        spriteRenderer.color = ColorManager.Instance.oldCoverColor;
                    }
                    else
                    {
                        spriteRenderer.color = ColorManager.Instance.newCoverColor;
                    }
                }
                else
                {
                    if (isDefault)
                    {
                        spriteRenderer.color = ColorManager.Instance.oldMainColor;
                    }
                    else
                    {
                        spriteRenderer.color = ColorManager.Instance.newMainColor;
                    }
                }
                break;

            case componentType.Image:
                Image image = GetComponent<Image>();
                if (isCover)
                {
                    if (isDefault)
                    {
                        image.color = ColorManager.Instance.oldCoverColor;
                    }
                    else
                    {
                        image.color = ColorManager.Instance.newCoverColor;
                    }
                }
                else
                {
                    if (proSens)
                    {
                        bool isProActive = String.Equals(DataManager.Instance.pro, "faruk");

                        if (isProActive)
                        {
                            if (isDefault)
                            {
                                image.color = ColorManager.Instance.oldCoverColor;
                            }
                            else
                            {
                                image.color = ColorManager.Instance.newCoverColor;
                            }
                        }
                        else
                        {
                            if (isDefault)
                            {
                                image.color = ColorManager.Instance.oldMainColor;
                            }
                            else
                            {
                                image.color = ColorManager.Instance.newMainColor;
                            }
                        }
                    }
                    else
                    {
                        if (isDefault)
                        {
                            image.color = ColorManager.Instance.oldMainColor;
                        }
                        else
                        {
                            image.color = ColorManager.Instance.newMainColor;
                        }
                    }
                }
                break;
            case componentType.Text:
                TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
                if (isDefault)
                {
                    text.color = ColorManager.Instance.oldMainColor;
                }
                else
                {
                    text.color = ColorManager.Instance.newMainColor;
                }
                break;
            case componentType.Trail:
                TrailRenderer trail = GetComponent<TrailRenderer>();
                if (isDefault)
                {
                    trail.startColor = ColorManager.Instance.oldMainColor;
                    trail.endColor = ColorManager.Instance.oldMainColor;
                }
                else
                {
                    trail.startColor = ColorManager.Instance.newMainColor;
                    trail.endColor = ColorManager.Instance.newMainColor;
                }
                break;

            default:
                break;
        }
    }

    /*
     * Local method :   ColorLerp - Private
     * Description  :   1) 
     */
    private IEnumerator LerpColor(componentType type)
    {
        float elapsedTime = 0.0f;
        float totalTime = 0.8f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            switch (type)
            {
                case componentType.Material:
                    Material material = GetComponent<Image>().material;
                    Color32 startColor = Color32.Lerp(ColorManager.Instance.oldGradientStartColor, ColorManager.Instance.newGradientStartColor, (elapsedTime / totalTime));
                    Color32 endColor = Color32.Lerp(ColorManager.Instance.oldGradientEndColor, ColorManager.Instance.newGradientEndColor, (elapsedTime / totalTime));
                    material.SetColor("_StartColor", startColor);
                    material.SetColor("_EndColor", endColor);
                    break;
                case componentType.Particle:
                    ParticleSystem particleSystem = GetComponent<ParticleSystem>();
                    ParticleSystem.MainModule mainModule = particleSystem.main;
                    Color mainColorParticle = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                    mainModule.startColor = mainColorParticle;
                    break;
                case componentType.Sprite:
                    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                    if (isCover)
                    {
                        Color32 coverColorSprite = Color32.Lerp(ColorManager.Instance.oldCoverColor, ColorManager.Instance.newCoverColor, (elapsedTime / totalTime));
                        spriteRenderer.color = coverColorSprite;
                    }
                    else
                    {
                        Color32 mainColorSprite = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                        spriteRenderer.color = mainColorSprite;
                    }
                    break;
                case componentType.Image:
                    Image image = GetComponent<Image>();
                    if (proSens)
                    {
                        bool isProActive = String.Equals(DataManager.Instance.pro, "faruk");
                        if (isProActive)
                        {
                            Color32 coverColorImage = Color32.Lerp(ColorManager.Instance.oldCoverColor, ColorManager.Instance.newCoverColor, (elapsedTime / totalTime));
                            image.color = coverColorImage;
                        }
                        else
                        {
                            Color32 mainColorImage = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                            image.color = mainColorImage;
                        }
                    }
                    else
                    {
                        Color32 mainColorImage = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                        image.color = mainColorImage;
                    }
                    break;
                case componentType.Text:
                    TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
                    Color32 mainColorText = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                    text.color = mainColorText;
                    break;
                case componentType.Trail:
                    TrailRenderer trail = GetComponent<TrailRenderer>();
                    Color32 mainColorTrail = Color32.Lerp(ColorManager.Instance.oldMainColor, ColorManager.Instance.newMainColor, (elapsedTime / totalTime));
                    trail.startColor = mainColorTrail;
                    trail.endColor = mainColorTrail;
                    break;
                default:
                    break;
            }

            yield return null;
        }
    }
    #endregion LOCAL METHODS
}
