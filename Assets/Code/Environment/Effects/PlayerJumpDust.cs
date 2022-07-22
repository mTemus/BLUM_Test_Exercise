using UnityEngine;

public class PlayerJumpDust : MonoBehaviour
{
    public const string AnimationName_DustStart = "Start";
    public const string AnimationName_DustEnd = "End";

    public void PlayAnimation(string animationName)
    {
        GetComponent<Animator>().Play(animationName);
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
