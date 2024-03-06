using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

/// <summary>
/// Contains the boss's logic
/// </summary>
public class Boss : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float targetDistanceToShoot;

    private float remainingDistance;
    private Vector3 target;
    private float Gravity = 9.8f;
    private bool isShooting;

    private void Update()
    {
        target = agent.destination;
        remainingDistance = agent.remainingDistance;

        if (remainingDistance < targetDistanceToShoot && remainingDistance > 0 && !isShooting)
            StartCoroutine(Shoot());
    }

    /// <summary>
    /// place the corresponding animations and send the projectile towards the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        isShooting = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.SetBool("ReadyToShoot", true);

        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().AddForce(CalculateTrajectory(gameObject.transform.position, target), ForceMode.VelocityChange);

        yield return new WaitForSeconds(2);
        animator.SetBool("ReadyToShoot", false);
        agent.isStopped = false;
        isShooting = false;
    }

    /// <summary>
    /// Calculate the trajectory that the projectile should have to reach the player
    /// </summary>
    /// <param name="initialPosition"></param>
    /// <param name="FinalPosition"></param>
    /// <returns></returns>
    Vector3 CalculateTrajectory(Vector3 initialPosition, Vector3 FinalPosition)
    {
        Vector2 DisXY = new Vector2(target.x - gameObject.transform.position.x, target.y - gameObject.transform.position.y);
        float DisX = Mathf.Sqrt(DisXY.x * DisXY.x + DisXY.y * DisXY.y);
        float MaxHeight = Mathf.Max(gameObject.transform.position.y, target.y);
        float airTime = Mathf.Sqrt((2f * MaxHeight) / Gravity);
        float VelY = MaxHeight / airTime;
        float VelXZ = DisX / airTime;
        float AngleXZ = Mathf.Atan2(DisXY.y, DisXY.x) * Mathf.Rad2Deg;
        float VelX = VelXZ * Mathf.Cos(AngleXZ * Mathf.Deg2Rad);
        float VelZ = VelXZ * Mathf.Sin(AngleXZ * Mathf.Deg2Rad);

        return new Vector3(VelX, VelY, VelZ);
    }
}
