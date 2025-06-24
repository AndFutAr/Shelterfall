using System.Collections;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class MeteorShowerAnimation : DisasterAnimation
    {
        public override IEnumerator Animate()
        {
            yield return new WaitForSeconds(3f);
        }
    }
}