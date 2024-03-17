using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] float swipeSpeed = 5f;

    private int currentPageIndex = 0;
    private Vector2 targetPosition;

    void Start()
    {
        UpdateButtonInteractivity();
    }

    void UpdateButtonInteractivity()
    {
        nextButton.interactable = currentPageIndex < content.childCount - 1;
        prevButton.interactable = currentPageIndex > 0;
    }

    public void NextPage()
    {
        if (currentPageIndex < content.childCount - 1)
        {
            currentPageIndex++;
            targetPosition = new Vector2(-currentPageIndex * content.rect.width, content.anchoredPosition.y);
            UpdateButtonInteractivity();
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            targetPosition = new Vector2(-currentPageIndex * content.rect.width, content.anchoredPosition.y);
            UpdateButtonInteractivity();
        }
    }

    void Update()
    {
        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, swipeSpeed * Time.deltaTime);
    }
}
