using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BonusBalloonItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    public Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    public BonusBalloonType bonusBalloonType;

    public Vector3 yer;

    [SerializeField]
    bool drag = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        yer = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.isBonusesUseble)
        {
            Debug.Log(GameManager.Instance.isBonusesUseble);
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            turnOffLayerMask("UI");
            turnOffLayerMask("Balloon");
            yer = rectTransform.anchoredPosition;
            Debug.Log("Drag Baþladý...");
            drag = true;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.isBonusesUseble)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.isBonusesUseble)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            turnOnLayerMask("UI");
            turnOnLayerMask("Balloon");
            rectTransform.anchoredPosition = yer;
            Debug.Log("Drag Bitti...");
            drag = false;

            if (eventData != null)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current) { pointerId = -1, };
                pointerData.position = eventData.position;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag.Equals("Block"))
                    {
                        result.gameObject.GetComponent<Block>().OnDropRaycast(this.gameObject);
                    }
                }
            }

        }
    }
    public void turnOffLayerMask(string layerMaskName)
    {
        Physics2DRaycaster p2drc = Camera.main.GetComponent<Physics2DRaycaster>();
        LayerMask layerMask = p2drc.eventMask; LayerMask disableLayerMask = 1 << LayerMask.NameToLayer(layerMaskName); p2drc.eventMask = layerMask & ~disableLayerMask;
    }
    public void turnOnLayerMask(string layerMaskName)
    {
        Physics2DRaycaster p2drc = Camera.main.GetComponent<Physics2DRaycaster>(); 
        LayerMask layerMask = p2drc.eventMask; LayerMask enableLayerMask = 1 << LayerMask.NameToLayer(layerMaskName); p2drc.eventMask = layerMask | enableLayerMask;
    }
    public void YerineDon()
    {
        if (!drag && GameManager.Instance.isBonusesUseble)
        {
            rectTransform.anchoredPosition = yer;
        }
    }
    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");
        if(!drag && GameManager.Instance.isBonusesUseble)
        {
            UIManager.Instance.hand.GetComponent<Animator>().SetTrigger("BonusTutorialTrigger");
            PlayerPrefs.GetString("BonusBallTutorial", "False");
        }
    }

    void OnDisable()
    {

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            turnOnLayerMask("UI");
            turnOnLayerMask("Balloon");
            rectTransform.anchoredPosition = yer;

    }

}

public enum BonusBalloonType
{
    bonusBalloon1,
    bonusBalloon2,
    bonusBalloon3,
    bonusBalloon4,
    bonusBalloon5,
    bonusBalloon6,
    bonusBalloon7,
    bonusBalloon8,
    bonusBalloon9,
    bonusBalloon10,
    bonusBalloon11,
    bonusBalloon12,
    bonusBalloon13,
    bonusBalloon14,
    bonusBalloon15,
    bonusBalloon16,
    bonusBalloon17,
    bonusBalloon18,
    bonusBalloon19,
    bonusBalloon20,
    bonusBalloon21,
    bonusBalloon22,
    bonusBalloon23,
    bonusBalloon24,
    bonusBalloon25
}
