using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Sprite shep;
    private Sprite goomba;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shep = sprites[0];
        goomba = sprites[1];
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (spriteRenderer.sprite == shep)
            {
                spriteRenderer.sprite = goomba;
            }
            else
            {
                spriteRenderer.sprite = shep;
            }
        }
        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
