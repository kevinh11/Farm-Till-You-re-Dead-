using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 moveVec = Vector3.zero;
    [SerializeField]
    private float moveSpeed;
    private Vector3[] groundEdges;

    [SerializeField]
    private GameObject ground;

    private Rigidbody rb;
    public Animator playerAnim;
    public Transform playerTrans;
    public float ro_speed = 100f;
    private Quaternion initialRotation;
    private Quaternion targetRotation; 
    // private IEnumerator dieproses;
    public Transform models;

    void Start()
    {
        this.groundEdges = this.GetGroundEdges();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerTrans = transform;
        initialRotation = playerTrans.rotation;
        targetRotation = playerTrans.rotation;
        // dieproses = die(2.0f, 2.0f);
        // StartCoroutine(dieproses); 
    }

    Vector3[] GetGroundEdges()
    {
        Vector3[] edges = new Vector3[4];

        float groundXPos = ground.transform.position.x;
        float groundYPos = ground.transform.position.y;
        float groundZPos = ground.transform.position.z;

        float length = ground.GetComponent<MeshRenderer>().bounds.size.z;
        float width = ground.GetComponent<MeshRenderer>().bounds.size.x;

        float halfLength = length / 2;
        float halfWidth = width / 2;

        edges[0] = new Vector3(groundXPos - halfWidth, groundYPos, groundZPos - halfLength); 
        edges[1] = new Vector3(groundXPos + halfWidth, groundYPos, groundZPos - halfLength); 
        edges[2] = new Vector3(groundXPos - halfWidth, groundYPos, groundZPos + halfLength); 
        edges[3] = new Vector3(groundXPos + halfWidth, groundYPos, groundZPos + halfLength); 

        return edges;
    }

    bool CanMoveInDirection(Vector3 direction)
    {
        Vector3 newPosition = this.transform.position + direction * Time.deltaTime * moveSpeed;

        if (newPosition.x < this.groundEdges[0].x) return false;
        if (newPosition.x > this.groundEdges[1].x) return false;
        if (newPosition.z < this.groundEdges[0].z) return false;
        if (newPosition.z > this.groundEdges[2].z) return false;

        return true;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveVec = Vector3.zero;

        if (Input.GetKey(KeyCode.W) && CanMoveInDirection(Vector3.forward))
        {
            moveVec += Vector3.forward;
            SetTargetRotation(Quaternion.Euler(0, 0, 0));
        }
        else if (Input.GetKey(KeyCode.S) && CanMoveInDirection(Vector3.back))
        {
            moveVec += Vector3.back;
            SetTargetRotation(Quaternion.Euler(0, 180, 0)); 
        }
        else if (Input.GetKey(KeyCode.D) && CanMoveInDirection(Vector3.right))
        {
            moveVec += Vector3.right;
            SetTargetRotation(Quaternion.Euler(0, 90, 0)); 
        }
        else if (Input.GetKey(KeyCode.A) && CanMoveInDirection(Vector3.left))
        {
            moveVec += Vector3.left;
            SetTargetRotation(Quaternion.Euler(0, -90, 0)); 
        }

        playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, targetRotation, Time.deltaTime * ro_speed);

        // Trigger animations
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetTrigger("Walk");
            playerAnim.ResetTrigger("idle1");
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("Walk");
            playerAnim.SetTrigger("idle1");
        }

        MovePlayer();
        this.transform.position += moveVec * Time.deltaTime * moveSpeed;
    }

    void MovePlayer()
    {
        // Apply the movement vector to the Rigidbody
        rb.MovePosition(transform.position + moveVec * moveSpeed * Time.deltaTime);
    }

    void SetTargetRotation(Quaternion rotation)
    {
        targetRotation = rotation;
    }

    // private IEnumerator die(float phase, float seconds){
    //     for(int i = 0; i < phase; i++){
    //         if(i == phase -1){
    //             models.rotation = Quaternion.Euler(0, 0, 90);
               
    //         }
    //          yield return new WaitForSeconds(seconds);
    //     }
    // }
}
