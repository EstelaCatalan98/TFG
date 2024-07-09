using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    private bool isTweening;
    
    [SerializeField] Button nextButton;  
    [SerializeField] Button prevButton;  

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        isTweening = false;
        UpdateButtonVisibility();
    }

    public void Next()
    {
        if (isTweening) return;

        if (currentPage < maxPage)
        {
            isTweening = true;
            currentPage++;
            targetPos += pageStep;
            MovePage();
           
            UpdateButtonVisibility();  
        }
        
    }

    public void Previous()
    {
        if (isTweening) return;

        if (currentPage > 1)
        {
            isTweening = true;
            currentPage--;
            targetPos -= pageStep;
            MovePage();
       
            UpdateButtonVisibility();  
        }
        else
        {
           
        }
    }

    void MovePage()
    {
        LeanTween.moveLocal(levelPagesRect.gameObject, targetPos, tweenTime).setEase(tweenType).setOnComplete(() =>
        {
            isTweening = false;
            
        });
    }

    void UpdateButtonVisibility()
    {
        if (prevButton != null)
        {
            prevButton.gameObject.SetActive(currentPage > 1);
        }

        
        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(currentPage < maxPage);
        }
    }
}

