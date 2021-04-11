using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject person;

    public uint radius;     // Radius of larger circle
    public uint minDist;       // Minimum distance between people

    public uint N;   // Number of people in simulation. Constrained to be even
    [Range(1, 26)]
    public uint U;   // Unique qualities to choose from. Represented by letters. Cannot exceed 26.
    public uint Q;   // Qualities possessed by each person. Cannot exceed U.
    public uint R;   // Requirements per person. Optimality of a match is determined by (r-d)/R, where r and d are the number of qualities from R and D present in a potential partner.
    public uint D;   // Dealbreakers per person. Cannot share qualities with R. R + D cannot exceed Q.

    public static char[] alphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    private uint personNum = 0;

    // For now, just verify that qualities do not contradict. Eventually, allow player to choose qualities.
    public void Start()
    {
        if (Verify())
        {
            Populate();
        }
        else
        {
            Debug.Log("Failed to run simulation.");
        }
    }

    public bool Verify()
    {
        if (N % 2 != 0)
        {
            Debug.Log("N must be an even number.");
            return false;
        }
        else if (N < 2 || N > 32)
        {
            Debug.Log("N must be between 2 and 32 inclusive.");
            return false;
        }
        else if (U < 1 || U > 26)
        {
            Debug.Log("U must be between 1 and 26 inclusive.");
            return false;
        }
        else if (Q > U)
        {
            Debug.Log("Q cannot exceed U.");
            return false;
        }
        else if (R + D > U)
        {
            Debug.Log("R + D cannot exceed U.");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Populate()
    {
        float pRad = ((float) radius * Mathf.Sin(Mathf.PI / N)) - ((float)minDist / 2);
        for (int i = 0; i < N; i++)
        {
            float angle = ((float)i / N) * (2 * Mathf.PI);
            CreatePerson(new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius, (uint)Mathf.Min(pRad, 50f));
        }
    }

    public void CreatePerson(Vector2 position, uint personRadius)
    {
        GameObject newPerson = Instantiate(person);
        newPerson.name = "Person " + ++personNum;
        newPerson.transform.SetParent(canvas.transform);
        newPerson.transform.localPosition = position;
        newPerson.transform.localScale = new Vector3(1f, 1f, 1f);

        PersonController pc = newPerson.GetComponent<PersonController>();

        pc.bodyRadius = personRadius;
        pc.bodyColor = randomSaturatedColor();
        pc.qualities = randomQualities();
        pc.requirements = randomRequirements();
        pc.dealbreakers = randomDealbreakers(pc.requirements);
        pc.InitializePerson();
    }

    public Color randomSaturatedColor()
    {
        Vector3 colorVec = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        int minPos = 0;
        int maxPos = 0;
        for (int i = 0; i < 3; i++)
        {
            colorVec[i] = Mathf.Abs(colorVec[i]);
            if (colorVec[i] < colorVec[minPos])
            {
                minPos = i;
            }
            else if (colorVec[i] > colorVec[maxPos])
            {
                maxPos = i;
            }
        }
        colorVec[minPos] = 0.25f;
        colorVec[maxPos] = 1f;

        return new Color(colorVec.x, colorVec.y, colorVec.z);
    }

    public List<char> randomQualities()
    {
        return randomSelection(Q, new List<char>());
    }

    public List<char> randomRequirements()
    {
        return randomSelection(R, new List<char>());
    }

    public List<char> randomDealbreakers(List<char> requirements)
    {
        return randomSelection(D, requirements);
    }

    public List<char> randomSelection(uint total, List<char> exclude)
    {
        if (total > U - exclude.Count)
        {
            Debug.Log("Attempting to select too many qualities");
            return new List<char>();
        }

        List<char> selection = new List<char>();

        List<char> choices = new List<char>();
        for (int i = 0; i < U; i++)
        {
            if(!exclude.Contains(alphabet[i]))
            {
                choices.Add(alphabet[i]);
            }
        }

        for (int i = 0; i < total; i++)
        {
            // Because total cannot be greater than U, this should never fail
            int choice = Random.Range(0, choices.Count);
            char quality = choices[choice];
            choices.RemoveAt(choice);
            selection.Add(quality);
        }

        selection.Sort();
        return selection;
    }
}
