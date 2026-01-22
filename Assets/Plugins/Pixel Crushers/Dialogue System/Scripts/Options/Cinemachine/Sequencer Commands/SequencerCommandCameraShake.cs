#if USE_CINEMACHINE
using Cinemachine;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Unity.Mathematics;
using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

public class SequencerCommandCameraShake : SequencerCommand
    {
        CinemachineBasicMultiChannelPerlin noise;
        float amplitudeOG;
        float frequencyOG;
        float shakeLength;
        float amplitudeNew;
        float frequencyNew;
        float elapsed = 0.0f;

        public void Awake()
        {
            elapsed = 0.0f;
            var _camGameObject = GetSubject(0);
            var _cam = _camGameObject.GetComponent<CinemachineVirtualCamera>();
            var _amplitude = GetParameterAsFloat(1);
            var _frequency = GetParameterAsFloat(2);
            var _shakeLength = GetParameterAsFloat(3);


            // Shake
            noise = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            amplitudeOG = noise.m_AmplitudeGain;
            amplitudeNew = _amplitude;
            frequencyOG = noise.m_FrequencyGain;
            frequencyNew = _frequency;
            shakeLength = _shakeLength;
            

            // Add your initialization code here. You can use the GetParameter***() and GetSubject()
            // functions to get information from the command's parameters. You can also use the
            // Sequencer property to access the SequencerCamera, CameraAngle, Speaker, Listener,
            // SubtitleEndTime, and other properties on the sequencer. If IsAudioMuted() is true, 
            // the player has muted audio.
            //
            // If your sequencer command only does something immediately and then finishes,
            // you can call Stop() here and remove the Update() method:
            //
            // Stop();
            //
            // If you want to use a coroutine, use a Start() method in place of or in addition to
            // this method.
        }

        public void Update()
        {
            if (elapsed <= shakeLength)
            {
                noise.m_AmplitudeGain = amplitudeNew;
                noise.m_FrequencyGain = frequencyNew;
                elapsed += Time.deltaTime;
                Debug.Log(elapsed);
            }
            else
            {
                noise.m_AmplitudeGain = amplitudeOG;
                noise.m_FrequencyGain = frequencyOG;

                elapsed = 0.0f;
                Stop();
            }

        }

    }

}

#endif