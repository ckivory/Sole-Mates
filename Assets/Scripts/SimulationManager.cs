using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public uint N;   // Number of people in simulation. Constrained to be even
    [Range(1, 26)]
    public uint U;   // Unique qualities to choose from. Represented by letters. Cannot exceed 26.
    public uint Q;   // Qualities possessed by each person. Cannot exceed U.
    public uint R;   // Requirements per person. Optimality of a match is determined by (r-d)/R, where r and d are the number of qualities from R and D present in a potential partner.
    public uint D;   // Dealbreakers per person. Cannot share qualities with R. R + D cannot exceed Q.

    // For now, just verify that qualities do not contradict. Eventually, allow player to choose qualities.
    public void Start()
    {
        if(N % 2 != 0)
        {
            Debug.Log("N must be an even number.");
        }
        else if(U < 1 || U > 26)
        {
            Debug.Log("U must be between 1 and 26 inclusive.");
        }
        else if (Q > U)
        {
            Debug.Log("Q cannot exceed U.");
        }
        else if(R + D > Q)
        {
            Debug.Log("R + D cannot exceed Q.");
        }
        else
        {
            Debug.Log("Ready to begin populating simulation!");
        }
    }

}
