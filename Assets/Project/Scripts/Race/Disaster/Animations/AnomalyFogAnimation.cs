using System.Collections;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class AnomalyFogAnimation : DisasterAnimation
    {
        [SerializeField] private ParticleSystem _firefliesParticle1;
        [SerializeField] private ParticleSystem _firefliesParticle2;
        [SerializeField] private ParticleSystem _firefliesParticle3;

        public override IEnumerator Animate()
        {
            _firefliesParticle1.gameObject.SetActive(false);
            _firefliesParticle1.Play();
            yield return new WaitForSeconds(0.25f);

            RenderSettings.fog = true;

            yield return new WaitForSeconds(0.15f);
            _firefliesParticle2.gameObject.SetActive(false);
            _firefliesParticle2.Play();
            yield return new WaitForSeconds(0.55f);
            _firefliesParticle3.Play();
            yield return new WaitForSeconds(1.5f);

            RenderSettings.fog = false;
        }
    }
}