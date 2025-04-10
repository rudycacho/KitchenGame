using System;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour
{
    public Transform destinationTransform;
    
    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.destination = destinationTransform.position;
    }

    private void Update() {
        if(KitchenGameManager.Instance.IsGamePlaying()) {
            agent.isStopped = false;
            agent.destination = destinationTransform.position;
        }

        if (agent.remainingDistance < 0.5 && KitchenGameManager.Instance.IsGamePlaying()) {
            agent.isStopped = true;
            KitchenGameManager.Instance.RatEnding();
        }
    }
}
