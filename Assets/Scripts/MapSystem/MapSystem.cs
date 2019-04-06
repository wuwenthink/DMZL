using Common;

namespace Map
{
    public class MapSystem :System<MapSystem>
    {
        public override void Start ()
        {

            RegisterProxy(new MapBuildInteractProxy());
            RegisterProxy(new MapBuildLocationProxy());
            RegisterProxy( new MapBuildAddProcessProxy() );

        }
    }
}
