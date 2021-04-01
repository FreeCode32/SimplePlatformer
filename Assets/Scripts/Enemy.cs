using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public Transform player;
    public float visionRange = 5f;

    private int indexWaypoint = 1;

    private float health = 100;
    
    private Transform target = null;

    private Rigidbody rb;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyDamage(float ammount)
    {
        if(health <= 0) 
        {
            Destroy(this.gameObject);
            return;
        }

        health -= ammount;
    }

    void FixedUpdate()
    {
        float distanceToPlayer =  Mathf.Abs(player.position.x - transform.position.x);

        if(distanceToPlayer < visionRange && IsBetweenWaypoints(player))
            target = player;
        else
            target = waypoints[indexWaypoint];
        
        FollowTarget(target);
        ChangeIndex();
    }

    bool IsBetweenWaypoints(Transform target)
    {
        if(waypoints[0].position.x < target.position.x && waypoints[1].position.x > target.position.x)
        {
            return true;
        }

        return false;
    }

    void FollowTarget(Transform target)
    {
        if(target == null) return;

        float xDifference = target.position.x - transform.position.x;
        Vector3 normalizedDirection = Vector3.Normalize(new Vector3(xDifference, 0, 0));
        transform.Translate(normalizedDirection * Time.deltaTime * speed);        
    }

    void ChangeIndex()
    {
        float distance = Mathf.Abs(waypoints[indexWaypoint].position.x - transform.position.x);
        if(distance < 1f) indexWaypoint = indexWaypoint == 1 ? 0 : 1;
    }
}