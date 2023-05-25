using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody myRigid;

    //Move Variables
    private float horizontal;
    public float speed = 8f;
    public float initialSpeed;
    private bool isFacingRight = true;

    //Jump Variables

    public float jumpForce;
    public float initialJumpForce;
    public LayerMask whatIsGround;
    [SerializeField] private float checkDistance;
    [SerializeField] private bool isGrounded;

    //ShootVariables

    public GameObject sonicWavePrefab;
    public float sonicWaveSpeed;
    //public float sonicWaveScale;
    public float sonicWaveCadence;
    public float initialSonicWaveSpeed;
    //public float initialSonicWaveScale;
    public float initialSonicWaveCadence;


    public bool canShoot;
    public Transform shootOrigin;


    public int pressureAdded;
    public int pressureSubstracted;
    
    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        initialSpeed = speed;
        initialJumpForce = jumpForce;
        initialSonicWaveSpeed = sonicWaveSpeed;
        initialSonicWaveCadence = sonicWaveCadence;

        //initialSonicWaveScale = sonicWaveScale;
    }

    void Update()
    {
        if (GameManager.Instance.gameIsRunning)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            Flip();
            Jump();
            Shoot();
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

            transform.Rotate(0f, 180f, 0f);
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

        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {

            myRigid.velocity = Vector3.up * jumpForce;
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            GameObject sonicWaveClone = Instantiate(sonicWavePrefab, shootOrigin.position, shootOrigin.rotation);
            sonicWaveClone.GetComponent<SonicWaveMovement>().speed = sonicWaveSpeed;
            Destroy(sonicWaveClone, 5f);
            StartCoroutine(ShootCooldown());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BloodCell" || other.gameObject.tag == "SmokeEnemy")
        {
            BloodPressure.Instance.pressureLevel += pressureAdded;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "MedsEnemy")
        {
            BloodPressure.Instance.pressureLevel -= pressureSubstracted;
            Destroy(other.gameObject);
        }
    }

    public IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(sonicWaveCadence);
        canShoot = true;
        //yield return null;
    }

    
}
