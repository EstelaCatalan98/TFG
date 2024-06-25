using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        isTweening = false;
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
            Debug.Log("Next Page: " + currentPage);
            Debug.Log("Target Position after Next: " + targetPos);
        }
        else
        {
            Debug.Log("Already at maxPage");
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
            Debug.Log("Previous Page: " + currentPage);
            Debug.Log("Target Position after Previous: " + targetPos);
        }
        else
        {
            Debug.Log("Already at first page");
        }
    }

    void MovePage()
    {
        Debug.Log("Start MovePage to Position: " + targetPos);
        LeanTween.moveLocal(levelPagesRect.gameObject, targetPos, tweenTime).setEase(tweenType).setOnComplete(() =>
        {
            isTweening = false;
            Debug.Log("MovePage complete. Current Position: " + levelPagesRect.localPosition);
        });
    }
}
