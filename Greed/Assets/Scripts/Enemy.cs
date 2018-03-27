using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

    NavMeshAgent pathFinder;

    GameObject MainCharacter;
    Transform target;
    Player player;

    CameraShake camera;

    public float attackRange = 3f;
    public float attackTimer = 1f;
    public float damage = 1f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Use this for initialization
    void Start () {
        pathFinder = GetComponent<NavMeshAgent>();
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        target = MainCharacter.transform;
        player = MainCharacter.GetComponent<Player>();
        camera = MainCharacter.GetComponentInChildren<CameraShake>();

        StartCoroutine(UpdatePath());
	}
	
	// Update is called once per frame
	void Update () {

        attackTimer -= Time.deltaTime;

        if(target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if(distance <= attackRange && attackTimer <= 0)
            {
                player.loseChange(damage);
                attackTimer = 1f;
                StartCoroutine(camera.Shake(.05f, .0001f));
            }
        }

	}

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while(target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
            pathFinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
