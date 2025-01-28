using UnityEngine;
using UnityEngine.InputSystem;

public class FoodScript : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Camera mycamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60 * (int)Time.deltaTime;
        QualitySettings.vSyncCount = 10;
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;

        // If mouse overlaping the object
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool isHit = this.GetComponent<Collider2D>().OverlapPoint(point);

        // Translate camera position to screen position
        screenPosition.z = Camera.main.nearClipPlane + 1;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isHit)
        {
            if (Input.GetMouseButton(0))                                    // Drag the food, follow the mouse
            {
                transform.position = worldPosition;
            }else if (Input.GetMouseButtonUp(0))                            // After mouse released
            {
                transform.position = new Vector3(xPosition, yPosition, 0);
            }
        }
    }
}
