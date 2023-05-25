using UnityEngine;

public class SonicWaveMovement : MonoBehaviour
{
    public float speed;
    public float scale;
    public Rigidbody myRigid;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myRigid.AddForce(Vector3.right * speed * Time.deltaTime ,ForceMode.Impulse);
    }

}
