using System;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public Boolean dragging = false;
    public Vector3 offset;
    public Canvas canvas;
    [SerializeField] private Boolean isBack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Water follow the, else return to starting position
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        if(!isBack)
        {
            transform.position = new Vector3(xPosition + canvas.transform.position.x, yPosition + canvas.transform.position.y, 0);
            isBack = true;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        isBack = false;
    }
}
