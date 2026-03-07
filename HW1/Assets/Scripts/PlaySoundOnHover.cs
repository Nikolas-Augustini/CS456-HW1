using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaySoundOnHover : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    public AudioSource audioSource;

    void Awake()
    {
        if (interactable == null)
            interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        interactable.hoverEntered.AddListener(OnHoverEnter);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (audioSource != null)
            audioSource.Play();
    }
}