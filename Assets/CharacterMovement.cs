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

    void Start()
    {
        this.groundEdges = this.GetGroundEdges();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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

        if (newPosition.x < this.groundEdges[0].x) 
        {
            return false;
        }
        if (newPosition.x > this.groundEdges[1].x) 
        {
            return false;
        }

        if (newPosition.z < this.groundEdges[0].z ) 
        {
            return false;
        }
        if (newPosition.z > this.groundEdges[2].z ) 
        {
            return false;
        }

        return true;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveVec = new Vector3(moveHorizontal, 0.0f, moveVertical);

        moveVec = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            if (CanMoveInDirection(Vector3.forward))
            {
                moveVec += Vector3.forward;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (CanMoveInDirection(Vector3.back))
            {
                moveVec += Vector3.back;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (CanMoveInDirection(Vector3.right))
            {
                moveVec += Vector3.right;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (CanMoveInDirection(Vector3.left))
            {
                moveVec += Vector3.left;
            }
        }

        MovePlayer();
        this.transform.position += moveVec * Time.deltaTime * moveSpeed;
    }

    void MovePlayer()
    {
        // Apply the movement vector to the Rigidbody
        rb.MovePosition(transform.position + moveVec * moveSpeed * Time.deltaTime);
    }
}