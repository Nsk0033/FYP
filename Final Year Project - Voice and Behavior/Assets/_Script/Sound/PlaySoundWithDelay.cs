using UnityEngine;

namespace SmallHedge.SoundManager
{
    public class PlaySoundWithDelay : StateMachineBehaviour
    {
        [SerializeField] private SoundType sound;
        [SerializeField, Range(0, 1)] private float volume = 1;
        [SerializeField] private float delay = 0.5f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MonoBehaviour component = animator.GetComponent<MonoBehaviour>();
            if (component != null)
            {
                InvokeHelper helper = component.gameObject.AddComponent<InvokeHelper>();
                helper.InvokeMethod(() => PlayDelayedSound(), delay);
            }
        }

        private void PlayDelayedSound()
        {
            SoundManager.PlaySound(sound, null, volume);
        }
    }
}
