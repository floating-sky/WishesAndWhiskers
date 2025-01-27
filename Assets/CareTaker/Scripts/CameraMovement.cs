using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private SpriteRenderer background;

    private float bgMinX, bgMaxX, bgMinY, bgMaxY;

    private Vector3 dragOrgin;

    private void Awake()
    {
        bgMinX = background.transform.position.x - background.bounds.size.x / 2f;
        bgMaxX = background.transform.position.x + background.bounds.size.x / 2f;

        bgMinY = background.transform.position.y - background.bounds.size.y / 2f;
        bgMaxY = background.transform.position.y + background.bounds.size.y / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            dragOrgin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) 
        {
            Vector3 diff = dragOrgin - cam.ScreenToWorldPoint(Input.mousePosition);

            //cam.transform.position += diff;
            cam.transform.position = ClampCamera(cam.transform.position + diff);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition) 
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = bgMinX + camWidth;
        float maxX = bgMaxX - camWidth;
        float minY = bgMinY + camHeight;
        float maxY = bgMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
