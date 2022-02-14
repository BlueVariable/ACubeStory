using System.Linq;
using BLUE.ACubeStory.Cube.StateRewinder;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewinderProvider : MonoBehaviour, IRewinderProvider
    {
        private CubeStateRewinderBase[] _rewinders;

        private CubeStateRewinderBase[] _Rewinders
        {
            get 
            {
                if (_rewinders == null)
                {
                    _rewinders = GetComponentsInChildren<CubeStateRewinderBase>();
                }

                return _rewinders;
            }
        }
        
        public CubeStateRewinderBase GetRewinderBase(ECubeState state)
        {
            CubeStateRewinderBase rewinder = _Rewinders.SingleOrDefault(i => i.GetRewinderState() == state);

            return rewinder;
        }
    }
}