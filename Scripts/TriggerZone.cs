using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    [Header("Settings")]
    public bool triggerOnce = false;
    public string playerTag = "Player";

    [Header("Debug")]
    public Color gizmoColor = new Color(0f, 1f, 0f, 0.25f);

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (triggerOnce && hasTriggered) return;

        hasTriggered = true;
        onPlayerEnter.Invoke();
        Debug.Log("Player entered trigger: " + gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        onPlayerExit.Invoke();
        Debug.Log("Player exited trigger: " + gameObject.name);
    }

    // Draw the trigger zone in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.center, box.size);
            Gizmos.DrawWireCube(box.center, box.size);
        }

        SphereCollider sphere = GetComponent<SphereCollider>();
        if (sphere != null)
        {
            Gizmos.DrawSphere(
                transform.position + sphere.center,
                sphere.radius * transform.lossyScale.x
            );
        }
    }
}
