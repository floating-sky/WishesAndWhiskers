using UnityEngine;

public class SofaScriptCarer : MonoBehaviour
{
    [SerializeField] private Sprite damageSofa;
    [SerializeField] private Sprite newSofa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change the sofa image by index
    public void ChangeView(int ind)
    {
        switch(ind)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = damageSofa;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = newSofa;
                break;
        }
    }
}
