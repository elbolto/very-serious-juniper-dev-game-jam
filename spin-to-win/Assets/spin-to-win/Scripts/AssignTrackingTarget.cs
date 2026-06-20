using Unity.Cinemachine;
using UnityEngine;

public class AssignTrackingTarget : MonoBehaviour
{
    void Start()
    {
        var player = FindAnyObjectByType<Player>().gameObject;
        var cmCamera = GetComponent<CinemachineCamera>();
        cmCamera.Target.TrackingTarget = player.transform; 
    }
}
