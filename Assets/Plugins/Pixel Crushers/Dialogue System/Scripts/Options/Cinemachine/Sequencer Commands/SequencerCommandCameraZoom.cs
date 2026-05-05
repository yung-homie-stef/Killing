#if USE_CINEMACHINE
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using Cinemachine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

public class SequencerCommandCameraZoom : SequencerCommand
    {

        protected virtual IEnumerator Start()
        {
            var _camGameObject = GetSubject(0);
            var _cam = _camGameObject.GetComponent<CinemachineVirtualCamera>();
            var _zoomAmount = GetParameterAsFloat(1);
            var _zoomLength = GetParameterAsFloat(2);


            // Zoom
            var _originalZoom = _cam.m_Lens.FieldOfView;
            float elapsed = 0.0f;

            while (elapsed < _zoomLength)
            {
                _cam.m_Lens.FieldOfView = Mathf.Lerp(_originalZoom, _zoomAmount, elapsed / _zoomLength);
                yield return null;
                elapsed += DialogueTime.deltaTime;
            }

            Stop();

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


    }

}

#endif