using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    public static int maxFontSize = 40;
    public static int fontMargin = 16;

    public Image bodyImage;
    public Image infoImage;

    [HideInInspector]
    public uint bodyRadius;

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

    public static List<PersonController> people = new List<PersonController>();

    public void InitializePerson()
    {
        bodyImage.rectTransform.sizeDelta = new Vector2(bodyRadius * 2, bodyRadius * 2);
        InitializeText();
        InitializeColor();
        infoImage.gameObject.SetActive(false);
        people.Add(this);
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

    public static void HideAllInfoBoxes()
    {
        foreach(PersonController person in people)
        {
            person.infoImage.gameObject.SetActive(false);
        }
    }

    public void ShowInfoBox()
    {
        transform.SetAsLastSibling();
        infoImage.gameObject.SetActive(true);
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
        
        float newWidth = Mathf.Max(requirementText.preferredWidth, dealbreakerText.preferredWidth) + fontMargin;
        
        float sizeRatio = (bodyImage.rectTransform.rect.width / 100f);
        sizeRatio = Mathf.Max(sizeRatio, 0.4f);
        infoImage.transform.localScale = new Vector3(sizeRatio, sizeRatio, 1f);

        float newHeight = infoImage.rectTransform.sizeDelta.y;
        infoImage.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        float info_y = (bodyRadius + (newHeight * sizeRatio) / 2 + 5);
        if(transform.localPosition.y > 0)
        {
            infoImage.transform.localPosition = new Vector2(0f, -1 * info_y);
        } else
        {
            infoImage.transform.localPosition = new Vector2(0f, info_y);
        }
    }

    public void resizeQualities()
    {
        uint inscribed = (uint)(bodyRadius * Mathf.Sqrt(2));
        qualitiesText.rectTransform.sizeDelta = new Vector2(inscribed, inscribed);
        qualitiesText.fontSize = maxFontSize;
        while(qualitiesText.preferredHeight > qualitiesText.rectTransform.rect.height)
        {
            qualitiesText.fontSize -= 1;
        }
    }
}
