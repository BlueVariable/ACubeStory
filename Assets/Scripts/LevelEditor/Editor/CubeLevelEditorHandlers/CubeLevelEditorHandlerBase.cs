using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class CubeLevelEditorHandlerBase
    {
        private CubeLevelEditorHandlerBase[] _subHandlers;

        protected CubeLevelEditorHandlerBase[] _SubHandlers
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

        protected virtual CubeLevelEditorHandlerBase[] GetSubHandlers() => null;
    }
}