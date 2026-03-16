using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;
    private Queue<CinemachineVirtualCamera> _queue;

    private void Awake()
    {
        _queue = new Queue<CinemachineVirtualCamera>(_cameras);
        SwitchCamera();
    }

    public void SwitchCamera()
    {
        CinemachineVirtualCamera nextCamera = _queue.Dequeue();

        foreach (CinemachineVirtualCamera item in _cameras)
        {
            item.gameObject.SetActive(false);
        }

        nextCamera.gameObject.SetActive(true);
        _queue.Enqueue(nextCamera);
    }
}