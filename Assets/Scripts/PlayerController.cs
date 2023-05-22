using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRigid;

    //Move Variables
    private float horizontal;
    public float speed = 8f;
    private bool isFacingRight = true;

    //Jump Variables

    public float jumpForce;
    public LayerMask whatIsGround;
    [SerializeField] private float checkDistance;
    [SerializeField] private bool isGrounded;



    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        Jump();
    }

    private void FixedUpdate()
    {
        myRigid.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, myRigid.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        Ray ray;
        ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, checkDistance, whatIsGround))
        {
            Debug.Log("is hitting ground");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {

            myRigid.velocity = Vector3.up * jumpForce;
        }
    }
}
