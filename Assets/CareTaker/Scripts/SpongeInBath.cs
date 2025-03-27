using System;
using UnityEngine;

public class SpongeInBath : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public float zPosition;
    public Boolean dragging = false;
    public Vector3 offset;
    public Canvas canvas;
    [SerializeField] private Boolean isBack = true;

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
        // Sponge follow the, else return to starting position
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        if (!isBack)
        {
            transform.position = new Vector3(xPosition, yPosition, zPosition);
            isBack = true;
        }
    }

    public void setIsBack(Boolean value)
    {
        isBack = value;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter in sponge");
    }
}
