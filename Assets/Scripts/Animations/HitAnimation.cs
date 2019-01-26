using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    public Rigidbody LeftHand, RightHand;
    
    public void PlayHitAnimation(GameObject hitObject)
    {
        Rigidbody hand = ChooseHand(hitObject);
        hand.AddForceTowards(hitObject.transform, 100);
    }

    private Rigidbody ChooseHand(GameObject hitObject)
    {
        return new List<Rigidbody> {LeftHand, RightHand}
            .OrderByDescending(hand => Vector3.Distance(hitObject.transform.position, hand.transform.position))
            .FirstOrDefault();
    }
}