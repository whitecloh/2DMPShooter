using UnityEngine;

public class GatheringComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable == null) return;

        interactable.Interact(GetComponent<Character>());
    }
}
