using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AgentController : MonoBehaviour
{
    public Transform target;        // A reference to the target Transform
    private NavMeshAgent agent;     // The NavMeshAgent component for pathfinding

    private bool isMoving = false;  // Prevent multiple triggers

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // When space is pressed, start the delayed movement if not already moving
        if (Input.GetKeyDown(KeyCode.Space) && target != null && !isMoving)
        {
            StartCoroutine(MoveAfterDelay(2f));  // 2 second delay
        }
    }

    // Coroutine to move the agent after a specified delay
    IEnumerator MoveAfterDelay(float delay)
    {   
        // Prevent multiple movements while waiting
        isMoving = true;
        Debug.Log("Waiting for " + delay + " seconds...");
        yield return new WaitForSeconds(delay);
        // Move the agent to the target position
        agent.SetDestination(target.position);
        Debug.Log("Moving to target!");
        isMoving = false;
    }

    // Draw Gizmos to visualize the path in the editor
    void OnDrawGizmos()
    {   
        // Draw the line from the agent to the target
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.green;
            Vector3[] corners = agent.path.corners;
            for (int i = 0; i < corners.Length - 1; i++)
            {
                Gizmos.DrawLine(corners[i], corners[i + 1]);
            }
        }
    }
}
