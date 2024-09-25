using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 moveVec = Vector3.zero; // Reset moveVec at the start of each frame
    [SerializeField]
    private float moveSpeed;
    private Vector3[] groundEdges;

    [SerializeField]
    private GameObject ground;

    void Start()
    {
        this.groundEdges = this.GetGroundEdges();
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

        // Calculate the corners based on the ground object's center and dimensions
        edges[0] = new Vector3(groundXPos - halfWidth, groundYPos, groundZPos - halfLength); // bottom-left corner
        edges[1] = new Vector3(groundXPos + halfWidth, groundYPos, groundZPos - halfLength); // bottom-right corner
        edges[2] = new Vector3(groundXPos - halfWidth, groundYPos, groundZPos + halfLength); // top-left corner
        edges[3] = new Vector3(groundXPos + halfWidth, groundYPos, groundZPos + halfLength); // top-right corner

        return edges;
    }

    bool CanMoveInDirection(Vector3 direction)
    {
        Vector3 newPosition = this.transform.position + direction * Time.deltaTime * moveSpeed;

        // Check x bounds (left and right)
        if (newPosition.x < this.groundEdges[0].x && direction.x < 0) // Moving left
        {
            return false;
        }
        if (newPosition.x > this.groundEdges[1].x && direction.x > 0) // Moving right
        {
            return false;
        }

        // Check z bounds (forward and back)
        if (newPosition.z < this.groundEdges[0].z && direction.z < 0) // Moving back
        {
            return false;
        }
        if (newPosition.z > this.groundEdges[2].z && direction.z > 0) // Moving forward
        {
            return false;
        }

        // If none of the conditions are hit, the movement is valid
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset moveVec at the beginning of the frame
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

        // Move the character
        this.transform.position += moveVec * Time.deltaTime * moveSpeed;
    }
}
