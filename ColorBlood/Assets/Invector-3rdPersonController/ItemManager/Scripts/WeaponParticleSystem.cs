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
            AttackColorEffect effect = (AttackColorEffect) color;

            switch(effect) {
                case AttackColorEffect.RED:
                    return redAttack;
                case AttackColorEffect.BLUE:
                    return blueAttack;
                case AttackColorEffect.GREEN:
                    return greenAttack;
                case AttackColorEffect.YELLOW:
                    return yellowAttack;
            }
            return null;
        }
    }
}
