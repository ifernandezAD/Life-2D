using UnityEngine;

public class SonicWaveMovement : MonoBehaviour
{
    public float speed;
    //public float scale;
    public Rigidbody myRigid;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myRigid.velocity = transform.right * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BloodCell" || other.gameObject.tag == "MedsEnemy" || other.gameObject.tag == "SmokeEnemy")
        {
            Destroy(other.gameObject);
        }
    }

}
