using Common;

namespace MainSpace
{
    class MainSystem :System<MainSystem>
    {
        public override void Start ()
        {
            RegisterProxy(new SceneProxy());
            RegisterProxy(new HintProxy());
        }
    }
}
