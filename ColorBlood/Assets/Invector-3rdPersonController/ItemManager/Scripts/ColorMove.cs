using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector.vItemManager
{
	public enum MoveColorEffect {
		RED = 0,
		BLUE = 1,
		GREEN = 2,
		YELLOW = 3
	}
	public class ColorMove : MonoBehaviour {
		public MoveColorEffect colorAttack;
        public MoveColorEffect colorDefense;
    }
}
