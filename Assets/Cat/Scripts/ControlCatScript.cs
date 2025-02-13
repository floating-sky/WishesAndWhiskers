using UnityEngine;

public class ControlCatScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CatLogicScript catLogic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        catLogic = GameObject.FindGameObjectWithTag("CatLogic").GetComponent<CatLogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement keyboard inputs
        if(Input.GetKey(KeyCode.W))                         // Top
        {
            rb.AddForce(Vector3.up * 5);
        }
        if(Input.GetKey(KeyCode.A))                         // Left
        {
            rb.AddForce(Vector3.left * 5);
        }
        if(Input.GetKey(KeyCode.S))                         // Down
        {
            rb.AddForce(Vector3.down * 5);
        }
        if(Input.GetKey(KeyCode.D))                         // Right
        {
            rb.AddForce(Vector3.right * 5);
        }
        rb.linearVelocity = new Vector2(0, 0);

        // Interact keyboard inputs
        if(Input.GetKey(KeyCode.E))                         // Interact items
        {
            catLogic.CatInteracted();
        }
    }
}
