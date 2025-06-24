using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class AnomalyFogAnimation : DisasterAnimation
    {
        [SerializeField] private ParticleSystem _firefliesParticle1;
        [SerializeField] private ParticleSystem _firefliesParticle2;
        [SerializeField] private ParticleSystem _firefliesParticle3;
        [SerializeField] private float _fogDensity;

        public override IEnumerator Animate()
        {
            _firefliesParticle1.gameObject.SetActive(true);
            _firefliesParticle1.Play();
            RenderSettings.fog = true;
            DOVirtual
                .Float(0f, _fogDensity, 1.5f, value => RenderSettings.fogDensity = value)
                .SetEase(Ease.InQuad);
            
            yield return new WaitForSeconds(0.4f);
            _firefliesParticle2.gameObject.SetActive(true);
            _firefliesParticle2.Play();
            yield return new WaitForSeconds(0.75f);
            _firefliesParticle2.gameObject.SetActive(true);
            _firefliesParticle3.Play();
            yield return new WaitForSeconds(1.0f);
            DOVirtual
                .Float(_fogDensity, 0f, 1.5f, value => RenderSettings.fogDensity = value)
                .SetEase(Ease.InQuad);

            yield return new WaitForSeconds(0.5f);
            _firefliesParticle1.Stop();
            _firefliesParticle1.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(0.5f);
            _firefliesParticle2.Stop();
            _firefliesParticle2.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1f);
            _firefliesParticle3.gameObject.SetActive(false);
            RenderSettings.fog = false;
        }
    }
}