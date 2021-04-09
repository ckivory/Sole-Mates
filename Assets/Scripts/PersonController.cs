using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PersonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static int maxFontSize = 40;
    public static int fontMargin = 16;

    public Image bodyImage;
    public Image infoImage;

    [HideInInspector]
    public Text qualitiesText;
    [HideInInspector]
    public Text requirementText;
    [HideInInspector]
    public Text dealbreakerText;

    public Color bodyColor;

    public List<char> qualities;
    public List<char> requirements;
    public List<char> dealbreakers;

    public void InitializePerson()
    {
        InitializeText();
        InitializeColor();
        infoImage.gameObject.SetActive(false);
    }

    public void InitializeText()
    {
        qualitiesText = bodyImage.gameObject.transform.GetComponentInChildren<Text>();
        requirementText = infoImage.gameObject.transform.GetChild(0).GetComponent<Text>();
        dealbreakerText = infoImage.gameObject.transform.GetChild(1).GetComponent<Text>();
        updateText(qualitiesText, qualities);
        resizeQualities();
        updateInfoBox();
    }

    public void InitializeColor()
    {
        bodyImage.color = bodyColor;
    }

    public void updateText(Text tc, List<char> letters)
    {
        tc.text = "";
        for (int i = 0; i < letters.Count - 1; i++)
        {
            char q = letters[i];
            tc.text += q + " ";
        }
        if(letters.Count > 0)
        {
            tc.text += letters[letters.Count - 1];
        }
    }

    public void updateInfoBox()
    {
        updateText(requirementText, requirements);
        updateText(dealbreakerText, dealbreakers);
        if(requirements.Count < 1)
        {
            dealbreakerText.transform.localPosition = Vector2.zero;
        }
        if(dealbreakers.Count > 0)
        {
            dealbreakerText.text = "¬(" + dealbreakerText.text + ")";
        } else
        {
            requirementText.transform.localPosition = Vector2.zero;
        }
        
        float newWidth = Mathf.Max(requirementText.preferredWidth, dealbreakerText.preferredWidth) + 16;
        infoImage.rectTransform.sizeDelta = new Vector2(newWidth, infoImage.rectTransform.sizeDelta.y);
    }

    public void resizeQualities()
    {
        qualitiesText.fontSize = maxFontSize;
        while(qualitiesText.preferredHeight > qualitiesText.rectTransform.rect.height)
        {
            qualitiesText.fontSize -= 1;
        }
    }

    // Figure out how to make it only work when you hover over the body, not any other part. Maybe attach a script to the body itself.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entering");
        transform.SetAsLastSibling();
        infoImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exiting");
        infoImage.gameObject.SetActive(false);
    }
}
