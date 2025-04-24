using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ControlCatScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CatLogicScript catLogic;
    [SerializeField] private SpriteRenderer background;
    private Vector2 moveInput;
    private Animator animator;

    public float cameraMoveSpeed = 10f;
    private float bgMinX, bgMaxX, bgMinY, bgMaxY;
    private float screenRightBoundary, screenLeftBoundary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgMinX = background.transform.position.x - background.bounds.size.x / 2f;
        bgMaxX = background.transform.position.x + background.bounds.size.x / 2f;
        bgMinY = background.transform.position.y - background.bounds.size.y / 2f;
        bgMaxY = background.transform.position.y + background.bounds.size.y / 2f;
        screenRightBoundary = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        screenLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;

        catLogic = GameObject.FindGameObjectWithTag("CatLogic").GetComponent<CatLogicScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        screenRightBoundary = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        screenLeftBoundary = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;

        // Start the control movement after the player closed the control window
        if (!(Time.timeScale == 0f)){
            Vector2 position = (Vector2)transform.position + moveInput * 3.0f * Time.deltaTime;
            transform.position = position;

            // Interact keyboard inputs
            if (Input.GetKeyDown(KeyCode.E))                         // Interact items
            {
                catLogic.CatInteracted();
            }
        }

        if (transform.position.x > screenRightBoundary - 5f)
        {
            // Move camera right
            cam.transform.position = ClampCamera(cam.transform.position + new Vector3(cameraMoveSpeed * Time.deltaTime, 0, 0));
        }
        else if (transform.position.x < screenLeftBoundary + 5f)
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

    public void Move(InputAction.CallbackContext context) 
    {
        animator.SetBool("isWalking", true);
        if (context.canceled) 
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}
