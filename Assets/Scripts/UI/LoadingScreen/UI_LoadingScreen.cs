using BLUE.ACubeStory.LoadingScreen;
using BLUE.UISystem;

namespace BLUE.ACubeStory.UI
{
    public class UI_LoadingScreen : UIBase
    {
        protected override void AwakeCustomActions()
        {
            EventRaiser_OnLoadingScreenRequested.OnRaisedStatic += OnLoadingScreenUIRequested;
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            EventRaiser_OnLoadingScreenRequested.OnRaisedStatic -= OnLoadingScreenUIRequested;

            base.DestroyCustomActions();
        }

        private void OnLoadingScreenUIRequested(object sender, EventRaiserArgs_OnLoadingScreenRequested e)
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