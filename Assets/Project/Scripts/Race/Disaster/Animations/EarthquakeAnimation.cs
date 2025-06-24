using System.Collections;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class EarthquakeAnimation : DisasterAnimation
    {
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
            yield return new WaitForSeconds(0.2f);
            
            PlayParticle(_groundFire1);
            yield return new WaitForSeconds(0.05f);

            PlayParticle(_beamUp2);
            yield return new WaitForSeconds(0.65f);
            
            PlayParticle(_beamUp3);
            yield return new WaitForSeconds(0.2f);
            PlayParticle(_groundFire2);
            yield return new WaitForSeconds(0.05f);
            
            PlayParticle(_beamUp4);
            yield return new WaitForSeconds(0.20f);
            PlayParticle(_groundFire3);
            yield return new WaitForSeconds(0.55f);
            
            PlayParticle(_beamUp5);
            yield return new WaitForSeconds(0.2f);
            PlayParticle(_groundFire4);
            yield return new WaitForSeconds(1.05f);
            PlayParticle(_beamUp6);

            StopParticle(_beamUp1);
            StopParticle(_groundFire1);
            StopParticle(_beamUp2);
            StopParticle(_beamUp3);
            StopParticle(_groundFire2);
            StopParticle(_beamUp4);
            StopParticle(_groundFire3);
            StopParticle(_beamUp5);
            StopParticle(_groundFire4);
            StopParticle(_beamUp6);


            yield return new WaitForSeconds(1.5f);
        }

        private void PlayParticle(ParticleSystem particle)
        {
            _beamUp1.gameObject.SetActive(true);
            _beamUp1.Play();   
        }
        
        private void StopParticle(ParticleSystem particle)
        {
            _beamUp1.Stop();   
            _beamUp1.gameObject.SetActive(false);
        }
    }
}