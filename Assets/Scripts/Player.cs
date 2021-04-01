using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 100f;

    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRadius = 1f;

    private bool ground = true;
    private bool canJump = true;
    
    private float meleeDamage = 25;

    private Rigidbody rb;

    void OnDrawGizmos()
    {
        if(attackPoint == null)return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.D)) Move(Vector3.right);
        if(Input.GetKey(KeyCode.A)) Move(Vector3.left);
        
        if(Input.GetKeyDown(KeyCode.Space)) Jump();
    
        if(Input.GetMouseButtonDown(0)) Attack();
    }

    private void Move(Vector3 dir)
    {        
        Vector3 lookAt = new Vector3(
            transform.eulerAngles.x,
            dir.x * 90 - 90,
            transform.eulerAngles.z
        );

        transform.eulerAngles = lookAt;

        transform.Translate(Vector3.right * speed * Time.deltaTime);        
    }

    private void Jump()
    {
        if(!canJump)return;
        

        if(!ground) canJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        ground = false;
    }
    
    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayers);
        Debug.Log(hitEnemies.Length);
        foreach(Collider collider in hitEnemies)
        {
            collider.GetComponent<Enemy>().ApplyDamage(meleeDamage);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            canJump = true;
            ground = true;
        }
    }
}