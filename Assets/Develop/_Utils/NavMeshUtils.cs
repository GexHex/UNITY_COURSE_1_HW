using System.Collections.Generic;
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

    public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
    {
        if (NavMesh.CalculatePath(sourcePosition, targetPosition, queryFilter, pathToTarget) && pathToTarget.status != NavMeshPathStatus.PathInvalid)
        {         
            return true;
        }

        return false;
    }

    public static List<Vector3> GetRandomPoints(Vector3 center, float radius, int count)
    {
        List<Vector3> points = new();

        while (points.Count < count)
        {
            Vector3 random = center + Random.insideUnitSphere * radius;

            if (NavMesh.SamplePosition(random, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                points.Add(hit.position);
            }
        }

        return points;
    }
}