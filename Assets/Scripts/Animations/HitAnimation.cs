using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    public Rigidbody LeftHand, RightHand;
    
    public void PlayHitAnimation(GameObject hitObject)
    {
        Rigidbody hand = ChooseHand();
        hand.AddForceTowards(hitObject.transform, 80);
    }

    private Rigidbody ChooseHand()
    {
        int handIndex = Random.Range(0, 2);
        return handIndex == 0 ? LeftHand : RightHand;
    }
}