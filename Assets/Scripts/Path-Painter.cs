using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavMeshPathDrawer : MonoBehaviour
{
    public NavMeshAgent agent;        // Reference to the agent whose path to draw
    private LineRenderer line;        // The line renderer component

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (agent.hasPath)
        {
            line.positionCount = agent.path.corners.Length;
            line.SetPositions(agent.path.corners);
        }
        else
        {
            line.positionCount = 0;
        }
    }
}
