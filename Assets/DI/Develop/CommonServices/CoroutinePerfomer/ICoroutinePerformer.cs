using System.Collections;
using UnityEngine;

namespace DI.Game.Develop.CommonServices.CoroutinePerfomer
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}
