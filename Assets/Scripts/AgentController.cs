using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentController : MonoBehaviour
{
    public Transform target;    // Target to move towards
    public float moveSpeed = 5f;    // Speed of the agent
    private NavMeshAgent agent;   // Reference to the NavMeshAgent component
    private NavMeshPath navPath;    // Current path of the agent
    private CharacterController controller; // Reference to the CharacterController component (if used)
    private Vector3 inputDirection; // Direction based on input

    // Start is called before the first frame update
    void Start()
    {   
        // Initialize components
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;  // move manually
        agent.updateRotation = false;  // handle rotation manually

        // Initialize path and controller
        navPath = new NavMeshPath();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // WASD movement relative to facing
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDirection = transform.TransformDirection(input);

        // Move
        if (controller != null)
            // Use CharacterController for movement
            controller.SimpleMove(inputDirection.normalized * moveSpeed);
        else
            // Use NavMeshAgent for movement
            transform.Translate(moveSpeed * Time.deltaTime * inputDirection.normalized, Space.World);

        // Recalculate path
        if (target != null)
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, navPath);
    }


    
    // Get the current path of the agent
    public NavMeshPath GetCurrentPath()
    {   
        return navPath;
    }
}
