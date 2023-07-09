using BLUE.UISystem;

namespace BLUE.ACubeStory.UI
{
    public class UI_Cutscene : UIBase
    {
        protected override void AwakeCustomActions()
        {
            EventRaiser_OnCutsceneUIRequested.OnRaisedStatic += OnCutsceneUIRequested;
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            EventRaiser_OnCutsceneUIRequested.OnRaisedStatic -= OnCutsceneUIRequested;

            base.DestroyCustomActions();
        }

        private void OnCutsceneUIRequested(object sender, EventRaiserArgs_OnCutsceneUIRequested e)
        {
            void onActivated()
            {
                e.ResultedCallback?.Invoke(true);
            }
            
            void onDeactivated()
            {
                e.ResultedCallback?.Invoke(true);
            }
            
            if (e.Activation)
            {
                bool canActivate = TryActivate(onActivated);

                if (!canActivate)
                {
                    e.ResultedCallback?.Invoke(false);
                }
            }
            else
            {
                bool canDeactivate = TryDeactivate(onDeactivated);

                if (!canDeactivate)
                {
                    e.ResultedCallback?.Invoke(false);
                }
            }
        }
    }
}