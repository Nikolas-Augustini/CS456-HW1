using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeColorOnHover : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    public Renderer cubeRenderer;

    public Color hoverColor = Color.white;
    private Color originalColor;

    void Awake()
    {
        if (interactable == null)
            interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (cubeRenderer == null)
            cubeRenderer = GetComponent<Renderer>();

        originalColor = cubeRenderer.material.color;

        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        cubeRenderer.material.color = hoverColor;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        cubeRenderer.material.color = originalColor;
    }
}