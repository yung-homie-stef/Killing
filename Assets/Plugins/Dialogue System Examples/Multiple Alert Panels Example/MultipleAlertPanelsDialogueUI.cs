using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.Examples
{

    /// <summary>
    /// This subclass of StandardDialogueUI adds support for a second alert panel. 
    /// Most of the methods are written to accept any subtitle panel (StandardUIAlertControls),
    /// so you can expand this subclass to support additional alert panels.
    /// </summary>
    public class MultipleAlertPanelsDialogueUI : StandardDialogueUI
    {

        // The UI elements for alert panel 2:
        public StandardUIAlertControls alertPanel2UIElements;

        // The queue for queued alerts going to alert panel 2:
        private Queue<QueuedUIAlert> m_alertPanel2Queue = new Queue<QueuedUIAlert>();

        public override void ShowAlert(string message, float duration)
        {
            if (message.Contains("[panel=2]"))
            {
                // If the message contains "[panel=2]", show it in panel 2:
                ShowAlertInOtherPanel(message.Replace("[panel=2]", ""), duration, alertPanel2UIElements, m_alertPanel2Queue);
            }
            else
            {
                // Otherwise show it in the default panel:
                base.ShowAlert(message, duration);
            }
        }

        /// <summary>
        /// Show an alert in an alternate alert panel.
        /// </summary>
        /// <param name="message">The alert message.</param>
        /// <param name="duration">The duration to show the alert, in seconds.</param>
        /// <param name="alertPanelUIElements">The alert panel UI elements.</param>
        /// <param name="alertQueue">The queue to use if we need to queue the alert.</param>
        public void ShowAlertInOtherPanel(string message, float duration, 
            StandardUIAlertControls alertPanelUIElements, Queue<QueuedUIAlert> alertQueue)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (alertPanelUIElements.dontQueueDuplicates)
            {
                if (alertPanelUIElements.isVisible && string.Equals(alertPanelUIElements.alertText.text, message)) return;
                foreach (var queuedItem in alertQueue)
                {
                    if (string.Equals(message, queuedItem.message)) return;
                }
            }
            if (alertPanelUIElements.allowForceImmediate && message.Contains("[f]"))
            {
                alertPanelUIElements.ShowMessage(message.Replace("[f]", string.Empty), duration);
            }
            else if (alertPanelUIElements.queueAlerts)
            {
                alertQueue.Enqueue(new QueuedUIAlert(message, duration));
            }
            else
            {
                alertPanelUIElements.ShowMessage(message, duration);
            }
        }

        /// <summary>
        /// Hides all alert panels.
        /// </summary>
        public override void HideAlert()
        {
            base.HideAlert();
            if (alertPanel2UIElements.isVisible) alertPanel2UIElements.Hide();
        }

        public override void Update()
        {
            base.Update();

            // If the alert in panel 2 is done, hide the panel:
            if (alertPanel2UIElements.isVisible && alertPanel2UIElements.IsDone) alertPanel2UIElements.Hide();

            // If an alert is waiting in the queue for panel 2, show it:
            if (alertPanel2UIElements.queueAlerts && m_alertPanel2Queue.Count > 0 &&
                !alertPanel2UIElements.isVisible && !(alertPanel2UIElements.waitForHideAnimation && alertPanel2UIElements.isHiding))
            {
                ShowNextQueuedAlertForPanel(alertPanel2UIElements, m_alertPanel2Queue);
            }
        }

        private void ShowNextQueuedAlertForPanel(StandardUIAlertControls alertPanelUIElements, Queue<QueuedUIAlert> alertQueue)
        {
            if (alertQueue.Count > 0)
            {
                var queuedAlert = alertQueue.Dequeue();
                alertPanelUIElements.ShowMessage(queuedAlert.message, queuedAlert.duration);
            }
        }

    }
}
