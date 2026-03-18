using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    public float interactDistance = 3f;
    public string promptText = "Press E to interact";
    public UnityEvent onInteract;

    [Header("UI")]
    public static Interactable currentTarget;

    private static GUIStyle promptStyle;

    public void Interact()
    {
        onInteract.Invoke();
    }

    void Update()
    {
        // Find the player camera
        Camera cam = Camera.main;
        if (cam == null) return;

        float dist = Vector3.Distance(cam.transform.position, transform.position);

        // Check if player is looking at this object and within range
        if (dist <= interactDistance)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, interactDistance, ~0, QueryTriggerInteraction.Collide);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == gameObject ||
                    hit.collider.transform.IsChildOf(transform))
                {
                    currentTarget = this;

                    if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        Interact();
                    }
                    return;
                }
            }
        }

        // Clear target if this was the current one
        if (currentTarget == this)
            currentTarget = null;
    }

    void OnGUI()
    {
        if (currentTarget != this) return;

        if (promptStyle == null)
        {
            promptStyle = new GUIStyle(GUI.skin.box);
            promptStyle.fontSize = 35;
            promptStyle.alignment = TextAnchor.MiddleCenter;
            promptStyle.normal.textColor = Color.white;
        }

        float w = 300f;
        float h = 40f;
        Rect rect = new Rect(
            (Screen.width - w) / 2f,
            Screen.height * 0.65f,
            w, h
        );
        GUI.Box(rect, promptText, promptStyle);
    }
}
