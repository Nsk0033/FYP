using UnityEngine;

namespace SmallHedge.SoundManager
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Sounds SO", fileName = "Sounds SO")]
    public class SoundsSO : ScriptableObject
    {
        public SoundList[] sounds;
    }
}

