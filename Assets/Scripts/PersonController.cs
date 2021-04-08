using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonController : MonoBehaviour
{
    public static int maxFontSize = 40;

    public List<char> qualities;
    public List<char> requirements;
    public List<char> dealbreakers;

    public Text qualitiesText;

    void Start()
    {
        updateQualitiesText();
    }

    public void updateQualitiesText()
    {
        qualitiesText.text = "";
        for (int i = 0; i < qualities.Count - 1; i++)
        {
            char q = qualities[i];
            qualitiesText.text += q + " ";
        }
        qualitiesText.text += qualities[qualities.Count - 1];

        resizeQualities();
    }

    public void resizeQualities()
    {
        qualitiesText.fontSize = maxFontSize;
        while(qualitiesText.preferredHeight > qualitiesText.rectTransform.rect.height)
        {
            qualitiesText.fontSize -= 1;
        }
    }
}
