namespace UniBT.Examples.Scripts.Behavior
{
    public class IsAttacking : Conditional
    {

        Companion companion;

        protected override void OnAwake()
        {
            companion = gameObject.GetComponent<Companion>();
        }

        protected override bool IsUpdatable()
        {
            return companion.Attacking;
        }
    }
}