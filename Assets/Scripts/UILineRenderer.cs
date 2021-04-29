using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    public float lineWidth = 3f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        // Make the rectangle a line that goes between two points and has a thickness
            // Just need to to be able to start and stop at any two points
        // Make multiple lines in a for loop

        // Vector2 startPos = Vector2.zero;
        // Vector2 endPos = new Vector2(10f, 10f);
        // float width = 3f;

        if(PersonController.people.Count > 0)
        {
            PersonController pc = PersonController.people[0];

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            Vector3 rectCenter = new Vector3(pc.transform.localPosition.x, pc.transform.localPosition.y);
            Debug.Log("Position: " + rectCenter);


            vertex.position = new Vector3(-1 * lineWidth / 2, 0);
            vh.AddVert(vertex);

            vertex.position = new Vector3(lineWidth / 2, 0);
            vh.AddVert(vertex);

            vertex.position = rectCenter + new Vector3(lineWidth / 2, 0f, 0f);
            vh.AddVert(vertex);

            vertex.position = rectCenter - new Vector3(lineWidth / 2, 0f, 0f);
            vh.AddVert(vertex);


            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }
    }
}
