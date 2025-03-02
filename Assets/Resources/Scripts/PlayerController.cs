using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private JoystickController joystick;
    private InventaryScript inventary;
    private LoadingSceneScript loadingScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<JoystickController>();
        inventary = FindObjectOfType<InventaryScript>();
        loadingScript = FindObjectOfType<LoadingSceneScript>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        var moveInput = joystick.GetInput();
        var direction = new Vector2(moveInput.x, moveInput.y);
        rb.velocity = direction * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var field = other.gameObject;
        if (field.CompareTag("Item"))
        {
            var item = field.transform.parent.gameObject;
            var cell = inventary.Find—ellForItem(item.GetComponent<CreatureType>().yourPrefab);
            if (cell != null)
            {
                cell.GetComponent<—ellScript>().AddItemCell(item.GetComponent<CreatureType>().yourPrefab);
                loadingScript.RemoveObjectScene(item);
                Destroy(item);
            }
        }
    }
}