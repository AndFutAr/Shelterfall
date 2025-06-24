using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Race.Disaster.Animations
{
    public class MeteorShowerAnimation : DisasterAnimation
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ParticleSystem _lavaStream;
        [SerializeField] private ParticleSystem _lavaExplosion1;
        [SerializeField] private ParticleSystem _lavaExplosion2;
        [SerializeField] private ParticleSystem _lavaExplosion3;
        [SerializeField] private ParticleSystem _lavaExplosion4;
        [SerializeField] private ParticleSystem _lavaExplosion5;
        
        public override IEnumerator Animate()
        {
            _lavaExplosion1.gameObject.SetActive(true);
            _lavaExplosion1.Play();
            _camera.DOShakePosition(0.25f, Vector3.one * 4.5f);
            yield return new WaitForSeconds(0.5f);
            _lavaExplosion2.gameObject.SetActive(true);
            _lavaExplosion2.Play();
            _lavaStream.gameObject.SetActive(true);
            _lavaStream.Play();
            _camera.DOShakePosition(0.25f, Vector3.one * 8.5f);
            yield return new WaitForSeconds(0.55f);
            _lavaExplosion3.gameObject.SetActive(true);
            _lavaExplosion3.Play();
            yield return new WaitForSeconds(1.15f);
            _lavaExplosion4.gameObject.SetActive(true);
            _lavaExplosion4.Play();
            _camera.DOShakePosition(0.25f, Vector3.one * 3.5f);
            yield return new WaitForSeconds(0.25f);
            _lavaExplosion5.gameObject.SetActive(true);
            _lavaExplosion5.Play();
            yield return new WaitForSeconds(2.5f);
            _lavaStream.Stop();
            
            HideParticles();
        }

        private void HideParticles()
        {
            _lavaStream.gameObject.SetActive(false);
            _lavaExplosion1.gameObject.SetActive(false);
            _lavaExplosion2.gameObject.SetActive(false);
            _lavaExplosion3.gameObject.SetActive(false);
            _lavaExplosion4.gameObject.SetActive(false);
            _lavaExplosion5.gameObject.SetActive(false);
        }
    }
}