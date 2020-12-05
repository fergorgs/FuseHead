using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPoint : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera last_vcam = default;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera next_vcam = default;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            last_vcam.Priority = 1;
            next_vcam.Priority = 20;
        }
    }
}
