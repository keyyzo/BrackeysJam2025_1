using UnityEngine;

public class TestItem : BaseItem
{
    [SerializeField] GameObject promptedText;

   

    bool isTextActive = false;

    MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        promptedText.SetActive(isTextActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdatedPromptedText();
    }

    public override void Interact()
    {
        Debug.Log("You have successfully interacted with this object");
        meshRenderer.material.color = Color.blue;
    }

    public override void CancelInteractionPrompt()
    {
        isTextActive = false;
    }

    public override void InteractionPrompt()
    {
        isTextActive = true;
    }

    void UpdatedPromptedText()
    {
        promptedText.SetActive(isTextActive);

        if (isTextActive == true)
        {
            promptedText.transform.forward = Camera.main.transform.forward;
        }
    }

    

}
