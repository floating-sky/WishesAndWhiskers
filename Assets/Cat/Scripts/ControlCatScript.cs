using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ControlCatScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CatLogicScript catLogic;
    [SerializeField] private SpriteRenderer background;
    public InputAction MoveAction;
    public float cameraMoveSpeed = 5f;
    private float bgMinX, bgMaxX, bgMinY, bgMaxY;

    private void Awake()
    {
        bgMinX = background.transform.position.x - background.bounds.size.x / 2f;
        bgMaxX = background.transform.position.x + background.bounds.size.x / 2f;

        bgMinY = background.transform.position.y - background.bounds.size.y / 2f;
        bgMaxY = background.transform.position.y + background.bounds.size.y / 2f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        catLogic = GameObject.FindGameObjectWithTag("CatLogic").GetComponent<CatLogicScript>();
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Start the control movement after the player closed the control window
        if(!(Time.timeScale == 0f)){
            Vector2 move = MoveAction.ReadValue<Vector2>();
            Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;
            transform.position = position;

            // Interact keyboard inputs
            if (Input.GetKey(KeyCode.E))                         // Interact items
            {
                catLogic.CatInteracted();
            }
        }


        float screenRightBoundary = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        float screenLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;

        if (transform.position.x > screenRightBoundary - 2f)
        {
            // Move camera right
            cam.transform.position = ClampCamera(cam.transform.position + new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0));
        }

        else if (transform.position.x < screenLeftBoundary + 2f)

        {
            // Move camera left
            cam.transform.position = ClampCamera(cam.transform.position - new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0));
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
