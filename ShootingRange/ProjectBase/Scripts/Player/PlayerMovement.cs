using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] PlayerInputAction inputActions;
    Vector2 moveInput;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.PlayerMoves.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();

        inputActions.PlayerMoves.Move.canceled += ctx => moveInput = new Vector2(0f, 0f);
    }

    private void Update()
    {
        Vector3 Move = new Vector3(moveInput.x, 0f, moveInput.y);

        transform.Translate(Move * speed * Time.deltaTime);
    }
}
