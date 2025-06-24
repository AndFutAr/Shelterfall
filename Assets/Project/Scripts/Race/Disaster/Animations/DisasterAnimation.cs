using System.Collections;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public abstract class DisasterAnimation : MonoBehaviour
    {
        public abstract IEnumerator Animate();
    }
}