using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject blockHitEffect; // assign in Inspector

    void OnCollisionEnter(Collision coll)
    {
        // Only trigger if hitting a castle block
        if (coll.gameObject.CompareTag("Castle"))
        {
            if (blockHitEffect != null)
            {
                // Spawn particle effect at contact point
                ContactPoint contact = coll.contacts[0];
                GameObject fx = Instantiate(blockHitEffect, contact.point, Quaternion.identity);
                Destroy(fx, 2f); // remove after 2 seconds
            }
        }
    }
}
