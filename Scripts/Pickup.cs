using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int scoreValue = 1;
    public bool bobUpDown = true;
    public bool spin = true;
    public float bobSpeed = 2f;
    public float bobHeight = 0.25f;
    public float spinSpeed = 90f;

    [Header("Feedback")]
    public AudioClip pickupSound;

    [Header("UI")]
    public bool showScore = true;
    public int scoreFontSize = 35;

    private Vector3 startPosition;
    private static GUIStyle scoreStyle;
    private static int lastFontSize;

    // Static counter for all pickups
    public static int totalCollected = 0;
    public static int totalPickups = 0;

    void Start()
    {
        startPosition = transform.position;
        totalPickups++;
    }

    void OnDestroy()
    {
        totalPickups--;
    }

    void Update()
    {
        // Floating animation
        if (bobUpDown)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(
                startPosition.x, newY, startPosition.z
            );
        }

        // Spinning animation
        if (spin)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }

    // Call this from the Interactable's OnInteract event
    public void Collect()
    {
        totalCollected += scoreValue;
        Debug.Log("Collected! Total: " + totalCollected);

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        gameObject.SetActive(false);
    }

    void OnGUI()
    {
        if (!showScore) return;

        if (scoreStyle == null || lastFontSize != scoreFontSize)
        {
            scoreStyle = new GUIStyle(GUI.skin.label);
            scoreStyle.fontSize = scoreFontSize;
            scoreStyle.fontStyle = FontStyle.Bold;
            scoreStyle.normal.textColor = Color.white;
            lastFontSize = scoreFontSize;
        }

        GUI.Label(
            new Rect(40, 40, 300, 60),
            "Collected: " + totalCollected,
            scoreStyle
        );
    }
}
