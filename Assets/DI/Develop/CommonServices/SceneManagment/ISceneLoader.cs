using System.Collections;
using UnityEngine.SceneManagement;

namespace DI.Game.Develop.CommonServices.SceneManagment
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    }
}
