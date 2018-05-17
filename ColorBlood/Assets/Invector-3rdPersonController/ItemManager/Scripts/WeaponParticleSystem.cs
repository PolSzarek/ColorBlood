using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Invector.vItemManager
{

    public class WeaponParticleSystem : MonoBehaviour
    {
        public List<MoveEffect> moves;

    }

    [System.Serializable]
    public class MoveEffect
    {
        public List<ColorAttackEffect> particles;

    }

    [System.Serializable]
    public class ColorAttackEffect {
        public GameObject redAttack;
        public GameObject blueAttack;
        public GameObject greenAttack;
        public GameObject yellowAttack;

        public GameObject getAttackEffect(int color) {
            MoveColorEffect effect = (MoveColorEffect) color;

            switch(effect) {
                case MoveColorEffect.RED:
                    return redAttack;
                case MoveColorEffect.BLUE:
                    return blueAttack;
                case MoveColorEffect.GREEN:
                    return greenAttack;
                case MoveColorEffect.YELLOW:
                    return yellowAttack;
            }
            return null;
        }
    }
}
