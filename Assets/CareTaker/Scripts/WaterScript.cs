using System;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public Boolean dragging = false;
    public Vector3 offset;
    public Canvas canvas;
    [SerializeField] private Boolean isBack = true;
    [SerializeField] private Sprite water;
    [SerializeField] private Sprite dropingWater;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
        zPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // When level 1 start
        if(!(Time.timeScale == 0f))
        {
            // Water follow the, else return to starting position
            if (dragging)
            {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            }
            if(!isBack)
            {
                transform.position = new Vector3(xPosition + canvas.transform.position.x, yPosition + canvas.transform.position.y, zPosition + canvas.transform.position.z);
                isBack = true;
            }
        }
    }

    public void changeWater()
    {
        GetComponent<SpriteRenderer>().sprite = water;
    }

    public void changeDropingWater()
    {
        GetComponent<SpriteRenderer>().sprite = dropingWater;
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
