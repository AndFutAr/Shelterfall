using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class EarthquakeAnimation : DisasterAnimation
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ParticleSystem _beamUp1;
        [SerializeField] private ParticleSystem _groundFire1;
        [SerializeField] private ParticleSystem _beamUp2;
        [SerializeField] private ParticleSystem _beamUp3;
        [SerializeField] private ParticleSystem _groundFire2;
        [SerializeField] private ParticleSystem _beamUp4;
        [SerializeField] private ParticleSystem _groundFire3;
        [SerializeField] private ParticleSystem _beamUp5;
        [SerializeField] private ParticleSystem _groundFire4;
        [SerializeField] private ParticleSystem _beamUp6;
        
        public override IEnumerator Animate()
        {
            PlayParticle(_beamUp1);
            _camera.DOShakePosition(0.4f, Vector3.one * 3.5f);
            
            yield return new WaitForSeconds(0.2f);
            PlayParticle(_groundFire1);
            
            yield return new WaitForSeconds(0.05f);
            PlayParticle(_beamUp2);
            _camera.DOShakePosition(0.4f, Vector3.one * 2.5f);
            
            yield return new WaitForSeconds(0.55f);
            PlayParticle(_beamUp3);
            _camera.DOShakePosition(0.2f, Vector3.one * 1.5f);
            
            yield return new WaitForSeconds(0.2f);
            PlayParticle(_groundFire2);
            
            yield return new WaitForSeconds(0.05f);
            PlayParticle(_beamUp4);
            _camera.DOShakePosition(0.3f, Vector3.one * 4.5f);
            
            yield return new WaitForSeconds(0.20f);
            
            PlayParticle(_groundFire3);
            yield return new WaitForSeconds(0.45f);
            
            PlayParticle(_beamUp5);
            _camera.DOShakePosition(0.25f, Vector3.one * 4.5f);
            yield return new WaitForSeconds(0.2f);
            PlayParticle(_groundFire4);
            
            yield return new WaitForSeconds(1f);
            PlayParticle(_beamUp6);

            StopParticle(_beamUp1);
            StopParticle(_groundFire1);
            StopParticle(_beamUp2);

            yield return new WaitForSeconds(0.4f);
            StopParticle(_beamUp3);
            StopParticle(_groundFire2);
            StopParticle(_beamUp4);

            yield return new WaitForSeconds(0.3f);
            StopParticle(_groundFire3);
            StopParticle(_beamUp5);

            yield return new WaitForSeconds(0.2f);
            StopParticle(_groundFire4);
            StopParticle(_beamUp6);
        }

        private void PlayParticle(ParticleSystem particle)
        {
            particle.gameObject.SetActive(true);
            particle.Play();   
        }
        
        private void StopParticle(ParticleSystem particle)
        {
            particle.Stop();   
            particle.gameObject.SetActive(false);
        }
    }
}