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

    public int pressureAdded;
    public int pressureSubstracted;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.gameIsRunning)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            Flip();
            Jump();
        }
     
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameIsRunning)
        {
            myRigid.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, myRigid.velocity.y);
        }
          
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BloodCell")
        {
            BloodPressure.Instance.pressureLevel += pressureAdded;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            BloodPressure.Instance.pressureLevel -= pressureSubstracted;
            Destroy(other.gameObject);
        }



    }
}
