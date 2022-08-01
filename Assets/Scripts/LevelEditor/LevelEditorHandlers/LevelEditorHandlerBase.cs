using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class LevelEditorHandlerBase
    {
        private LevelEditorHandlerBase[] _subHandlers;
        protected LevelEditorHandlerBase[] _SubHandlers
        {
            get
            {
                if (_subHandlers == null)
                {
                    _subHandlers = GetSubHandlers();
                }

                return _subHandlers;
            }
        }

        public abstract void Handle(Event e);

        protected virtual LevelEditorHandlerBase[] GetSubHandlers() => null;
    }
}