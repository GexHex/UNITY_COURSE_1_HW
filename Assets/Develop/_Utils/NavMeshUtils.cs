using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils
{
    public static float GetPathLenght(NavMeshPath path)
    {
        float pathLenght = 0;
        if (path.corners.Length > 1)
            for (int i = 1; i < path.corners.Length; i++)
                pathLenght += Vector3.Distance(path.corners[i - 1], path.corners[i]);

        return pathLenght;
    }

    public static bool TryGetPath(Vector3 sourcePosition, Transform targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
    {

        if (NavMesh.CalculatePath(sourcePosition, targetPosition.position, queryFilter, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
        {         
            return true;
        }

        return false;
    }
}