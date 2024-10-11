using UnityEngine;

public class PositionUpdater : MonoBehaviour
{
    float dist;
    public Transform target;


    void Update()
    {
        /*if (Vector2.Distance(target.position, transform.position) > 2f)
        {
            Destroy(gameObject);
        }*/
        dist = Vector3.Distance(transform.position, target.transform.position);

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector2 avancement = (target.position - transform.position);
        float scale = Mathf.Sqrt(Mathf.Pow(avancement.x, 2) + Mathf.Pow(avancement.y, 2));

        transform.localScale = new(dist / transform.parent.localScale.x, 0.3f, 1f);
    }
}