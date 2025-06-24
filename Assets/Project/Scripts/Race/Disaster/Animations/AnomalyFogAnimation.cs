using System.Collections;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class AnomalyFogAnimation : DisasterAnimation
    {
        public override IEnumerator Animate()
        {
            yield return new WaitForSeconds(3f);
        }
    }
}