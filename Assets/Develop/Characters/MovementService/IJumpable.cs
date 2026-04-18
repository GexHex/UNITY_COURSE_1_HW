using UnityEngine.AI;

public interface IJumpable
{
    bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);

    void Jump(OffMeshLinkData offMeshLinkData);

    bool InJumpProcess { get; }
}