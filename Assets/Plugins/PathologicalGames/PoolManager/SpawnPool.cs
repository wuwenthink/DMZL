using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace PathologicalGames
{

    [AddComponentMenu("Path-o-logical/PoolManager/SpawnPool")]
    public sealed class SpawnPool : MonoBehaviour, IList<Transform>
    {
        #region Inspector Parameters
        //缓存池的唯一名称
        public string poolName = "";
        //勾选后实例化的游戏对象的缩放比例将全是1，不勾选择用Prefab默认的。
        public bool matchPoolScale = false;
        //勾选后实例化的游戏对象的Layer将用Prefab默认的
        public bool matchPoolLayer = false;
        //勾选后实例化的对象将没有父节点，通通在最上层，建议不要勾选。
        public bool dontReparent = false;
        //是否切换场景不释放
        public bool dontDestroyOnLoad
		{
			get
			{
				return this._dontDestroyOnLoad;
			}
			
			set
			{
				this._dontDestroyOnLoad = value;
				
				if (this.group != null)
					Object.DontDestroyOnLoad(this.group.gameObject);
			}
		}
        public bool _dontDestroyOnLoad = false;
        //是否打印日志信息
        public bool logMessages = false;   
        public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();
        public Dictionary<object, bool> prefabsFoldOutStates = new Dictionary<object, bool>();
        #endregion Inspector Parameters

        #region Public Code-only Parameters
        //粒子特效最大入池时间
        public float maxParticleDespawnTime = 300;
       //空游戏对象，实例化出来的gameObject都在此对象上
        public Transform group { get; private set; }
        //预制物小池字典
        public PrefabsDict prefabs = new PrefabsDict();
        // Keeps the state of each individual foldout item during the editor session
        public Dictionary<object, bool> _editorListItemStates = new Dictionary<object, bool>();
        //只读型预制物小池字典
        public Dictionary<string, PrefabPool> prefabPools
        {
            get
            {
                var dict = new Dictionary<string, PrefabPool>();

                for (int i = 0; i < this._prefabPools.Count; i++)
                    dict[this._prefabPools[i].prefabGO.name] = this._prefabPools[i];

                return dict;
            }
        }
        #endregion Public Code-only Parameters

        #region Private Properties
        //预制物小池List
        private List<PrefabPool> _prefabPools = new List<PrefabPool>();
        //已出池对象List
        internal List<Transform> _spawned = new List<Transform>();
        #endregion Private Properties

        #region Constructor and Init
        private void Awake()
        {
            if (this._dontDestroyOnLoad) Object.DontDestroyOnLoad(this.gameObject);
            this.group = this.transform;
            if (this.poolName == "")
            {
                this.poolName = this.group.name.Replace("Pool", "");
                this.poolName = this.poolName.Replace("(Clone)", "");
            }

            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0}: Initializing..", this.poolName));

            for (int i = 0; i < this._perPrefabPoolOptions.Count; i++)
            {
                if (this._perPrefabPoolOptions[i].prefab == null)
                {
                    Debug.LogWarning(string.Format("Initialization Warning: Pool '{0}' " +
                              "contains a PrefabPool with no prefab reference. Skipping.",
                               this.poolName));
                    continue;
                }
                this._perPrefabPoolOptions[i].inspectorInstanceConstructor();
                this.CreatePrefabPool(this._perPrefabPoolOptions[i]);
            }
            PoolManager.Pools.Add(this);
        }
        //实例化对象代理
		public delegate GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot);
        //销毁对象代理
		public delegate void DestroyDelegate(GameObject instance);

        //实例化对象代理 Demo：InstanceHandlerDelegateExample.cs.
		public InstantiateDelegate instantiateDelegates;
        //销毁对象 Demo：InstanceHandlerDelegateExample.cs.
        public DestroyDelegate destroyDelegates;
		//根据原始备份对象
		internal GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			if (this.instantiateDelegates != null)
			{
				return this.instantiateDelegates(prefab, pos, rot);
			}
			else
			{
				return InstanceHandler.InstantiatePrefab(prefab, pos, rot);
			}
		}	
		//销毁某对象
		internal void DestroyInstance(GameObject instance)
		{
			if (this.destroyDelegates != null)
			{
				this.destroyDelegates(instance);
			}
			else
			{
				InstanceHandler.DestroyInstance(instance);
			}
		}
        private void OnDestroy()
        {
            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0}: Destroying...", this.poolName));

			if (PoolManager.Pools.ContainsValue(this))
				PoolManager.Pools.Remove(this);

            this.StopAllCoroutines();
            this._spawned.Clear();

			// Clean-up
            foreach (PrefabPool pool in this._prefabPools) 
			{
				pool.SelfDestruct();
			}
            // Probably overkill, and may not do anything at all, but...
            this._prefabPools.Clear();
            this.prefabs._Clear();
        }

        //创建prefabPool到SpawnPool
		public void CreatePrefabPool(PrefabPool prefabPool)
		{
			bool isAlreadyPool = this.GetPrefabPool(prefabPool.prefab) == null ? false : true;
			if (isAlreadyPool)
				throw new System.Exception(string.Format
            	(
					"Prefab '{0}' is already in  SpawnPool '{1}'. Prefabs can be in more than 1 SpawnPool but " +
					"cannot be in the same SpawnPool twice.",
					prefabPool.prefab, 
					this.poolName
				));
			
			prefabPool.spawnPool = this;	
			this._prefabPools.Add(prefabPool);
			this.prefabs._Add(prefabPool.prefab.name, prefabPool.prefab);		
			if (prefabPool.preloaded != true)
			{
				if (this.logMessages)
					Debug.Log(string.Format
					(
						"SpawnPool {0}: Preloading {1} {2}",
						this.poolName,
						prefabPool.preloadAmount,
						prefabPool.prefab.name
					));			
				prefabPool.PreloadInstances();
			}
		}

        //GameStart期间，将一个现存的GameObject添加到池中，（使用前提是池与池中预制物皆存在）参数依次为：实例，池中预制物名称，初始时是否出池，是否将池节点作为父节点。
        public void Add(Transform instance, string prefabName, bool despawn, bool parent)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                {
                    Debug.LogError("Unexpected Error: PrefabPool.prefabGO is null");
                    return;
                }

                if (this._prefabPools[i].prefabGO.name == prefabName)
                {
                    this._prefabPools[i].AddUnpooled(instance, despawn);

                    if (this.logMessages)
                        Debug.Log(string.Format(
                                "SpawnPool {0}: Adding previously unpooled instance {1}",
                                                this.poolName,
                                                instance.name));

                    if (parent) 
					{
						var worldPositionStays = !(instance is RectTransform);
						instance.SetParent(this.group, worldPositionStays);
					}

                    // New instances are active and must be added to the internal list 
                    if (!despawn) this._spawned.Add(instance);

                    return;
                }
            }

            // Log an error if a PrefabPool with the given name was not found
            Debug.LogError(string.Format("SpawnPool {0}: PrefabPool {1} not found.",
                                         this.poolName,
                                         prefabName));

        }
        #endregion Constructor and Init

        #region List Overrides
        public void Add(Transform item)
        {
            string msg = "Use SpawnPool.Spawn() to properly add items to the pool.";
            throw new System.NotImplementedException(msg);
        }
        public void Remove(Transform item)
        {
            string msg = "Use Despawn() to properly manage items that should " +
                         "remain in the pool but be deactivated.";
            throw new System.NotImplementedException(msg);
        }

        #endregion List Overrides

        #region Pool Functionality
        //从池中取出一个对象，参数分别为：原始Prefab备份，位置，旋转，父节点
        public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot, Transform parent)
        {
            Transform inst;
			bool worldPositionStays;

            #region Use from Pool
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == prefab.gameObject)
                {
                    inst = this._prefabPools[i].SpawnInstance(pos, rot);
                    if (inst == null) return null;
					worldPositionStays = !(inst is RectTransform);

					if (parent != null)  // User explicitly provided a parent
					{
						inst.SetParent(parent, worldPositionStays);
					}
                    else if (!this.dontReparent && inst.parent != this.group)  // Auto organize?
					{
						inst.SetParent(this.group, worldPositionStays);
					}
                    this._spawned.Add(inst);				
	                inst.gameObject.BroadcastMessage(
						"OnSpawned",
						this,
						SendMessageOptions.DontRequireReceiver
					);
                    return inst;
                }
            }
            #endregion Use from Pool


            #region New PrefabPool
            // The prefab wasn't found in any PrefabPools above. Make a new one
            PrefabPool newPrefabPool = new PrefabPool(prefab);
            this.CreatePrefabPool(newPrefabPool);

            // Spawn the new instance (Note: prefab already set in PrefabPool)
            inst = newPrefabPool.SpawnInstance(pos, rot);
			worldPositionStays = !(inst is RectTransform);
			if (parent != null)  // User explicitly provided a parent
			{
				inst.SetParent(parent, worldPositionStays);
			}
			else if (!this.dontReparent && inst.parent != this.group)  // Auto organize?
			{
				// If a new instance was created, it won't be grouped
				inst.SetParent(this.group, worldPositionStays);
			}

            // New instances are active and must be added to the internal list 
            this._spawned.Add(inst);
            #endregion New PrefabPool

            // Notify instance it was spawned so it can manage it's state
            inst.gameObject.BroadcastMessage(
				"OnSpawned",
				this,
				SendMessageOptions.DontRequireReceiver
			);

            // Done!
            return inst;
        }
        //从池中取一个不重设父节点的对象
        public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
        {
            Transform inst = this.Spawn(prefab, pos, rot, null);
            if (inst == null) return null;
            return inst;
        }
        //从池中取出一个位置0，无旋转的对象
        public Transform Spawn(Transform prefab)
        {
            return this.Spawn(prefab, Vector3.zero, Quaternion.identity);
        }
        //从池中取出一个位置0，无旋转，有父节点对象
        public Transform Spawn(Transform prefab, Transform parent)
        {
            return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
        }
		
		#region GameObject Overloads
        //以GameObject作为参数的重载
		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			return Spawn(prefab.transform, pos, rot, parent);
		}
		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			return Spawn(prefab.transform, pos, rot);
		}
		public Transform Spawn(GameObject prefab)
		{
			return Spawn(prefab.transform);
		}
		public Transform Spawn(GameObject prefab, Transform parent)
		{
			return Spawn(prefab.transform, parent);
		}
		#endregion GameObject Overloads
		
		//通过原始名称从池中取出一个对象
        public Transform Spawn(string prefabName)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab);
        }
        public Transform Spawn(string prefabName, Transform parent)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, parent);
        }
        public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, pos, rot);
        }
        public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot, 
                               Transform parent)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, pos, rot, parent);
        }

        //取音效资源
        public AudioSource Spawn(AudioSource prefab,
                            Vector3 pos, Quaternion rot)
        {
            return this.Spawn(prefab, pos, rot, null);  // parent = null
        }
        public AudioSource Spawn(AudioSource prefab)
        {
            return this.Spawn
            (
                prefab, 
                Vector3.zero, Quaternion.identity,
                null  // parent = null
            );
        }
		public AudioSource Spawn(AudioSource prefab, Transform parent)
        {
            return this.Spawn
            (
                prefab, 
                Vector3.zero, 
				Quaternion.identity,
                parent
            );
        }
        public AudioSource Spawn(AudioSource prefab,
                            	 Vector3 pos, Quaternion rot,
                            	 Transform parent)
        {
            // Instance using the standard method before doing audio stuff
            Transform inst = Spawn(prefab.transform, pos, rot, parent);

            // Can happen if limit was used
            if (inst == null) return null;

            // Get the emitter and start it
            var src = inst.GetComponent<AudioSource>();
            src.Play();

            this.StartCoroutine(this.ListForAudioStop(src));

            return src;
        }
        //取粒子特效
        public ParticleSystem Spawn(ParticleSystem prefab,
                                    Vector3 pos, Quaternion rot)
        {
            return Spawn(prefab, pos, rot, null);  // parent = null

        }
        public ParticleSystem Spawn(ParticleSystem prefab,
                                    Vector3 pos, Quaternion rot,
                                    Transform parent)
        {
            // Instance using the standard method before doing particle stuff
            Transform inst = this.Spawn(prefab.transform, pos, rot, parent);

            // Can happen if limit was used
            if (inst == null) return null;

            // Get the emitter and start it
            var emitter = inst.GetComponent<ParticleSystem>();
            //emitter.Play(true);  // Seems to auto-play on activation so this may not be needed

            this.StartCoroutine(this.ListenForEmitDespawn(emitter));

            return emitter;
        }

        //将一个对象入池相关函数
        public void Despawn(Transform instance)
        {
            bool despawned = false;
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i]._spawned.Contains(instance))
                {
                    despawned = this._prefabPools[i].DespawnInstance(instance);
                    break;
                }  
                else if (this._prefabPools[i]._despawned.Contains(instance))
                {
                    Debug.LogError(
                        string.Format("SpawnPool {0}: {1} has already been despawned. " +
                                       "You cannot despawn something more than once!",
                                        this.poolName,
                                        instance.name));
                    return;
                }
            }
            if (!despawned)
            {
                Debug.LogError(string.Format("SpawnPool {0}: {1} not found in SpawnPool",
                               this.poolName,
                               instance.name));
                return;
            }
            this._spawned.Remove(instance);
        }
        public void Despawn(Transform instance, Transform parent)
        {
			bool worldPositionStays = !(instance is RectTransform);
			instance.SetParent(parent, worldPositionStays);
            this.Despawn(instance);
        }
        public void Despawn(Transform instance, float seconds)
        {
            this.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, false, null));
        }
        public void Despawn(Transform instance, float seconds, Transform parent)
        {
            this.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, true, parent));
        }
        //将全部对象入池
        public void DespawnAll()
        {
            var spawned = new List<Transform>(this._spawned);
            for (int i = 0; i < spawned.Count; i++)
                this.Despawn(spawned[i]);
        }
        private IEnumerator DoDespawnAfterSeconds(Transform instance, float seconds, bool useParent, Transform parent)
        {
            GameObject go = instance.gameObject;
            while (seconds > 0)
            {
                yield return null;
                if (!go.activeInHierarchy)
                    yield break;             
                seconds -= Time.deltaTime;
            }
            if (useParent)
                this.Despawn(instance, parent);
            else
                this.Despawn(instance);
        }

        //某对象是否在池中
        public bool IsSpawned(Transform instance)
        {
            return this._spawned.Contains(instance);
        }
        #endregion Pool Functionality

        #region Utility Functions
        //根据原始备份获得池脚本
        public PrefabPool GetPrefabPool(Transform prefab)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                    Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null",
                                                 this.poolName));

                if (this._prefabPools[i].prefabGO == prefab.gameObject)
                    return this._prefabPools[i];
            }

            // Nothing found
            return null;
        }
        public PrefabPool GetPrefabPool(GameObject prefab)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                    Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null",
                                                 this.poolName));

                if (this._prefabPools[i].prefabGO == prefab)
                    return this._prefabPools[i];
            }

            // Nothing found
            return null;
        }
        //根据原始备份获取池脚本所挂对象
        public Transform GetPrefab(Transform instance)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
                if (this._prefabPools[i].Contains(instance))
                    return this._prefabPools[i].prefab;

            // Nothing found
            return null;
        }
        public GameObject GetPrefab(GameObject instance)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
                if (this._prefabPools[i].Contains(instance.transform))
                    return this._prefabPools[i].prefabGO;

            // Nothing found
            return null;
        }


        private IEnumerator ListForAudioStop(AudioSource src)
        {
            yield return null;

			GameObject srcGameObject = src.gameObject;
            while (src.isPlaying)
			{
                yield return null;
			}

			if (!srcGameObject.activeInHierarchy)
			{
				src.Stop();
				yield break;
			}

            this.Despawn(src.transform);
        }
        // ParticleSystem (Shuriken) Version...
        private IEnumerator ListenForEmitDespawn(ParticleSystem emitter)
        {
            // Wait for the delay time to complete
            // Waiting the extra frame seems to be more stable and means at least one 
            //  frame will always pass
            yield return new WaitForSeconds(emitter.startDelay + 0.25f);

            // Do nothing until all particles die or the safecount hits a max value
            float safetimer = 0;   // Just in case! See Spawn() for more info
			GameObject emitterGO = emitter.gameObject;
			while (emitter.IsAlive(true) && emitterGO.activeInHierarchy)
            {
                safetimer += Time.deltaTime;
                if (safetimer > this.maxParticleDespawnTime)
                    Debug.LogWarning
                    (
                        string.Format
                        (
                            "SpawnPool {0}: " +
                                "Timed out while listening for all particles to die. " +
                                "Waited for {1}sec.",
                            this.poolName,
                            this.maxParticleDespawnTime
                        )
                    );

                yield return null;
            }

            // Turn off emit before despawning
			if (emitterGO.activeInHierarchy)
			{
				this.Despawn(emitter.transform);
				emitter.Clear(true);
			}
        }

        #endregion Utility Functions

        public override string ToString()
        {
            var name_list = new List<string>();
            foreach (Transform item in this._spawned)
                name_list.Add(item.name);
            return System.String.Join(", ", name_list.ToArray());
        }
        public Transform this[int index]
        {
            get { return this._spawned[index]; }
            set { throw new System.NotImplementedException("Read-only."); }
        }

        //禁用（请使用isSpawned）
        public bool Contains(Transform item)
        {
            string message = "Use IsSpawned(Transform instance) instead.";
            throw new System.NotImplementedException(message);
        }
        public void CopyTo(Transform[] array, int arrayIndex)
        {
            this._spawned.CopyTo(array, arrayIndex);
        }
        public int Count
        {
            get { return this._spawned.Count; }
        }
        public IEnumerator<Transform> GetEnumerator()
        {
            for (int i = 0; i < this._spawned.Count; i++)
                yield return this._spawned[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this._spawned.Count; i++)
                yield return this._spawned[i];
        }

        //未实现的接口
        public int IndexOf(Transform item) { throw new System.NotImplementedException(); }
        public void Insert(int index, Transform item) { throw new System.NotImplementedException(); }
        public void RemoveAt(int index) { throw new System.NotImplementedException(); }
        public void Clear() { throw new System.NotImplementedException(); }
        public bool IsReadOnly { get { throw new System.NotImplementedException(); } }
        bool ICollection<Transform>.Remove(Transform item) { throw new System.NotImplementedException(); }
    }


    //预制物池
    [System.Serializable]
    public class PrefabPool
    {

        #region Public Properties Available in the Editor
        //原始备份Transform与GameObject
        public Transform prefab;
        internal GameObject prefabGO;  
        //预加载数量
        public int preloadAmount = 1;
        //如果勾选表示缓存池所有的gameobject可以“异步”加载
        public bool preloadTime = false;
        //每几帧加载一个
        public int preloadFrames = 2;
        //延迟多久开始加载
        public float preloadDelay = 0;
        //是否开启对象实例化的限制功能
        public bool limitInstances = false;
        //限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准
        public int limitAmount = 100;
        //如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。
        public bool limitFIFO = false;  // Keep after limitAmount for auto-inspector
        //是否自动清理
        public bool cullDespawned = false;
        //缓存池自动清理，但是始终保留几个对象不清理
        public int cullAbove = 50;
        //每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时
        public int cullDelay = 60;
        //每次自动清理几个游戏对象
        public int cullMaxPerPass = 5;
        //是否打印日志
        public bool _logMessages = false;  // Used by the inspector
        public bool logMessages            // Read-only
        {
            get
            {
                if (forceLoggingSilent) return false;

                if (this.spawnPool.logMessages)
                    return this.spawnPool.logMessages;
                else
                    return this._logMessages;
            }
        }
        private bool forceLoggingSilent = false;
        //所在的对象池引用
        public SpawnPool spawnPool;
        #endregion Public Properties Available in the Editor


        #region Constructor and Self-Destruction
        //使用现存Prefab创建
        public PrefabPool(Transform prefab)
        {
            this.prefab = prefab;
            this.prefabGO = prefab.gameObject;
        }
        public PrefabPool() { }

        //Awake时调用
        internal void inspectorInstanceConstructor()
        {
            this.prefabGO = this.prefab.gameObject;
            this._spawned = new List<Transform>();
            this._despawned = new List<Transform>();
        }

        //将池设置为初始状态（Spawn脚本销毁时调用）
        internal void SelfDestruct()
        {
			if (this.logMessages)
				Debug.Log(string.Format(
					"SpawnPool {0}: Cleaning up PrefabPool for {1}...", this.spawnPool.poolName, this.prefabGO.name
				));
            foreach (Transform inst in this._despawned)
                if (inst != null && this.spawnPool != null)  
					this.spawnPool.DestroyInstance(inst.gameObject);

            foreach (Transform inst in this._spawned)
				if (inst != null && this.spawnPool != null)
					this.spawnPool.DestroyInstance(inst.gameObject);

            this._spawned.Clear();
            this._despawned.Clear();

			this.prefab = null;
			this.prefabGO = null;
			this.spawnPool = null;
        }
        #endregion Constructor and Self-Destruction


        #region Pool Functionality
        private bool cullingActive = false;
        //已经出池的对象
        internal List<Transform> _spawned = new List<Transform>();
        public List<Transform> spawned { get { return new List<Transform>(this._spawned); } }
        //已经入池的对象
        internal List<Transform> _despawned = new List<Transform>();
        public List<Transform> despawned { get { return new List<Transform>(this._despawned); } }
        //池中对象总数量
        public int totalCount
        {
            get
            {
                int count = 0;
                count += this._spawned.Count;
                count += this._despawned.Count;
                return count;
            }
        }

        //Used to make PreloadInstances() a one-time event. Read-only.
        private bool _preloaded = false;
        internal bool preloaded
        {
            get { return this._preloaded; }
            private set { this._preloaded = value; }
        }

        //将一个对象入池
        internal bool DespawnInstance(Transform xform)
        {
            return DespawnInstance(xform, true);
        }
        internal bool DespawnInstance(Transform xform, bool sendEventMessage)
        {
            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0} ({1}): Despawning '{2}'",
                                       this.spawnPool.poolName,
                                       this.prefab.name,
                                       xform.name));

            // Switch to the despawned list
            this._spawned.Remove(xform);
            this._despawned.Add(xform);

            // Notify instance of event OnDespawned for custom code additions.
            //   This is done before handling the deactivate and enqueue incase 
            //   there the user introduces an unforseen issue.
            if (sendEventMessage)
                xform.gameObject.BroadcastMessage(
					"OnDespawned",
					this.spawnPool,
                    SendMessageOptions.DontRequireReceiver
				);

            // Deactivate the instance and all children
			xform.gameObject.SetActive(false);

            // Trigger culling if the feature is ON and the size  of the 
            //   overall pool is over the Cull Above threashold.
            //   This is triggered here because Despawn has to occur before
            //   it is worth culling anyway, and it is run fairly often.
            if (!this.cullingActive &&   // Cheap & Singleton. Only trigger once!
                this.cullDespawned &&    // Is the feature even on? Cheap too.
                this.totalCount > this.cullAbove)   // Criteria met?
            {
                this.cullingActive = true;
                this.spawnPool.StartCoroutine(CullDespawned());
            }
            return true;
        }
        //清理入池对象（自动清理时调用此函数）
        internal IEnumerator CullDespawned()
        {
            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING TRIGGERED! " +
                                          "Waiting {2}sec to begin checking for despawns...",
                                        this.spawnPool.poolName,
                                        this.prefab.name,
                                        this.cullDelay));
            yield return new WaitForSeconds(this.cullDelay);
            while (this.totalCount > this.cullAbove)
            {
                for (int i = 0; i < this.cullMaxPerPass; i++)
                {
                    if (this.totalCount <= this.cullAbove)
                        break;  // The while loop will stop as well independently

                    // Destroy the last item in the list
                    if (this._despawned.Count > 0)
                    {
                        Transform inst = this._despawned[0];
                        this._despawned.RemoveAt(0);
						this.spawnPool.DestroyInstance(inst.gameObject);

                        if (this.logMessages)
                            Debug.Log(string.Format("SpawnPool {0} ({1}): " +
                                                    "CULLING to {2} instances. Now at {3}.",
                                                this.spawnPool.poolName,
                                                this.prefab.name,
                                                this.cullAbove,
                                                this.totalCount));
                    }
                    else if (this.logMessages)
                    {
                        Debug.Log(string.Format("SpawnPool {0} ({1}): " +
                                                    "CULLING waiting for despawn. " +
                                                    "Checking again in {2}sec",
                                                this.spawnPool.poolName,
                                                this.prefab.name,
                                                this.cullDelay));

                        break;
                    }
                }

                // Check again later
                yield return new WaitForSeconds(this.cullDelay);
            }

            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0} ({1}): CULLING FINISHED! Stopping",
                                        this.spawnPool.poolName,
                                        this.prefab.name));
            this.cullingActive = false;
            yield return null;
        }
        //从池中取出一个对象（优先出空闲对象，没有则新建）
        internal Transform SpawnInstance(Vector3 pos, Quaternion rot)
        {
            // Handle FIFO limiting if the limit was used and reached.
            //   If first-in-first-out, despawn item zero and continue on to respawn it
            if (this.limitInstances && this.limitFIFO &&
                this._spawned.Count >= this.limitAmount)
            {
                Transform firstIn = this._spawned[0];

                if (this.logMessages)
                {
                    Debug.Log(string.Format
                    (
                        "SpawnPool {0} ({1}): " +
                            "LIMIT REACHED! FIFO=True. Calling despawning for {2}...",
                        this.spawnPool.poolName,
                        this.prefab.name,
                        firstIn
                    ));
                }

                this.DespawnInstance(firstIn);

                // Because this is an internal despawn, we need to re-sync the SpawnPool's
                //  internal list to reflect this
                this.spawnPool._spawned.Remove(firstIn);
            }

            Transform inst;

            // If nothing is available, create a new instance
            if (this._despawned.Count == 0)
            {
                // This will also handle limiting the number of NEW instances
                inst = this.SpawnNew(pos, rot);
            }
            else
            {
                // Switch the instance we are using to the spawned list
                // Use the first item in the list for ease
                inst = this._despawned[0];
                this._despawned.RemoveAt(0);
                this._spawned.Add(inst);

                // This came up for a user so this was added to throw a user-friendly error
                if (inst == null)
                {
                    var msg = "Make sure you didn't delete a despawned instance directly.";
                    throw new MissingReferenceException(msg);
                }

                if (this.logMessages)
                    Debug.Log(string.Format("SpawnPool {0} ({1}): respawning '{2}'.",
                                            this.spawnPool.poolName,
                                            this.prefab.name,
                                            inst.name));

                // Get an instance and set position, rotation and then 
                //   Reactivate the instance and all children
                inst.position = pos;
                inst.rotation = rot;
				inst.gameObject.SetActive(true);

            }
			
			//
			// NOTE: OnSpawned message broadcast was moved to main Spawn() to ensure it runs last
			//
			
            return inst;
        }
        //从池中新建一个对象取出
        public Transform SpawnNew() { return this.SpawnNew(Vector3.zero, Quaternion.identity); }
        public Transform SpawnNew(Vector3 pos, Quaternion rot)
        {
            // Handle limiting if the limit was used and reached.
            if (this.limitInstances && this.totalCount >= this.limitAmount)
            {
                if (this.logMessages)
                {
                    Debug.Log(string.Format
                    (
                        "SpawnPool {0} ({1}): " +
                                "LIMIT REACHED! Not creating new instances! (Returning null)",
                            this.spawnPool.poolName,
                            this.prefab.name
                    ));
                }

                return null;
            }

            // Use the SpawnPool group as the default position and rotation
            if (pos == Vector3.zero) pos = this.spawnPool.group.position;
            if (rot == Quaternion.identity) rot = this.spawnPool.group.rotation;

			GameObject instGO = this.spawnPool.InstantiatePrefab(this.prefabGO, pos, rot);
			Transform inst = instGO.transform;

			this.nameInstance(inst);  // Adds the number to the end

            if (!this.spawnPool.dontReparent)
			{
				// The SpawnPool group is the parent by default
				// This will handle RectTransforms as well
				var worldPositionStays = !(inst is RectTransform);
				inst.SetParent(this.spawnPool.group, worldPositionStays);
			}

            if (this.spawnPool.matchPoolScale)
                inst.localScale = Vector3.one;

            if (this.spawnPool.matchPoolLayer)
                this.SetRecursively(inst, this.spawnPool.gameObject.layer);

            // Start tracking the new instance
            this._spawned.Add(inst);

            if (this.logMessages)
                Debug.Log(string.Format("SpawnPool {0} ({1}): Spawned new instance '{2}'.",
                                        this.spawnPool.poolName,
                                        this.prefab.name,
                                        inst.name));

            return inst;
        }

        //递归设置
        private void SetRecursively(Transform xform, int layer)
        {
            xform.gameObject.layer = layer;
            foreach (Transform child in xform)
                SetRecursively(child, layer);
        }

        //SpawnPool 调用，将一个对象加入已存在的池中（PreRuntimePoolItem调过来）
        internal void AddUnpooled(Transform inst, bool despawn)
        {
            this.nameInstance(inst);   // Adds the number to the end

            if (despawn)
            {
                // Deactivate the instance and all children
				inst.gameObject.SetActive(false);

                // Start Tracking as despawned
                this._despawned.Add(inst);
            }
            else
                this._spawned.Add(inst);
        }

        //预加载游戏对象
        internal void PreloadInstances()
        {
            // If this has already been run for this PrefabPool, there is something
            //   wrong!
            if (this.preloaded)
            {
                Debug.Log(string.Format("SpawnPool {0} ({1}): " +
                                          "Already preloaded! You cannot preload twice. " +
                                          "If you are running this through code, make sure " +
                                          "it isn't also defined in the Inspector.",
                                        this.spawnPool.poolName,
                                        this.prefab.name));

                return;
            }

			this.preloaded = true;

            if (this.prefab == null)
            {
                Debug.LogError(string.Format("SpawnPool {0} ({1}): Prefab cannot be null.",
                                             this.spawnPool.poolName,
                                             this.prefab.name));

                return;
            }

            // Protect against preloading more than the limit amount setting
            //   This prevents an infinite loop on load if FIFO is used.
            if (this.limitInstances && this.preloadAmount > this.limitAmount)
            {
                Debug.LogWarning
                (
                    string.Format
                    (
                        "SpawnPool {0} ({1}): " +
                            "You turned ON 'Limit Instances' and entered a " +
                            "'Limit Amount' greater than the 'Preload Amount'! " +
                            "Setting preload amount to limit amount.",
                         this.spawnPool.poolName,
                         this.prefab.name
                    )
                );

                this.preloadAmount = this.limitAmount;
            }

            // Notify the user if they made a mistake using Culling
            //   (First check is cheap)
            if (this.cullDespawned && this.preloadAmount > this.cullAbove)
            {
                Debug.LogWarning(string.Format("SpawnPool {0} ({1}): " +
                    "You turned ON Culling and entered a 'Cull Above' threshold " +
                    "greater than the 'Preload Amount'! This will cause the " +
                    "culling feature to trigger immediatly, which is wrong " +
                    "conceptually. Only use culling for extreme situations. " +
                    "See the docs.",
                    this.spawnPool.poolName,
                    this.prefab.name
                ));
            }

            if (this.preloadTime)
            {
                if (this.preloadFrames > this.preloadAmount)
                {
                    Debug.LogWarning(string.Format("SpawnPool {0} ({1}): " +
                        "Preloading over-time is on but the frame duration is greater " +
                        "than the number of instances to preload. The minimum spawned " +
                        "per frame is 1, so the maximum time is the same as the number " +
                        "of instances. Changing the preloadFrames value...",
                        this.spawnPool.poolName,
                        this.prefab.name
                    ));

                    this.preloadFrames = this.preloadAmount;
                }

                this.spawnPool.StartCoroutine(this.PreloadOverTime());
            }
            else
            {
                // Reduce debug spam: Turn off this.logMessages then set it back when done.
                this.forceLoggingSilent = true;

                Transform inst;
                while (this.totalCount < this.preloadAmount) // Total count will update
                {
                    // Preload...
                    // This will parent, position and orient the instance
                    //   under the SpawnPool.group
                    inst = this.SpawnNew();
                    this.DespawnInstance(inst, false);
                }

                // Restore the previous setting
                this.forceLoggingSilent = false;
            }
        }
        //延迟X秒后开始预加载
        private IEnumerator PreloadOverTime()
        {
            yield return new WaitForSeconds(this.preloadDelay);

            Transform inst;

            // subtract anything spawned by other scripts, just in case
            int amount = this.preloadAmount - this.totalCount;
            if (amount <= 0)
                yield break;

            // Doesn't work for Windows8...
            //  This does the division and sets the remainder as an out value.
            //int numPerFrame = System.Math.DivRem(amount, this.preloadFrames, out remainder);
            int remainder = amount % this.preloadFrames;
            int numPerFrame = amount / this.preloadFrames;

            // Reduce debug spam: Turn off this.logMessages then set it back when done.
            this.forceLoggingSilent = true;

            int numThisFrame;
            for (int i = 0; i < this.preloadFrames; i++)
            {
                // Add the remainder to the *last* frame
                numThisFrame = numPerFrame;
                if (i == this.preloadFrames - 1)
                {
                    numThisFrame += remainder;
                }

                for (int n = 0; n < numThisFrame; n++)
                {
                    // Preload...
                    // This will parent, position and orient the instance
                    //   under the SpawnPool.group
                    inst = this.SpawnNew();
                    if (inst != null)
                        this.DespawnInstance(inst, false);

                    yield return null;
                }

                // Safety check in case something else is making instances. 
                //   Quit early if done early
                if (this.totalCount > this.preloadAmount)
                    break;
            }

            // Restore the previous setting
            this.forceLoggingSilent = false;
        }

        #endregion Pool Functionality


        #region Utilities
        //某个GameObject是否在池中
        public bool Contains(Transform transform)
        {
            if (this.prefabGO == null)
                Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null",
                                             this.spawnPool.poolName));

            bool contains;

            contains = this.spawned.Contains(transform);
            if (contains)
                return true;

            contains = this.despawned.Contains(transform);
            if (contains)
                return true;

            return false;
        }
        //给从池中取出的对象重命名
        private void nameInstance(Transform instance)
        {
            instance.name += (this.totalCount + 1).ToString("#000");
        }
        #endregion Utilities

    }



    public class PrefabsDict : IDictionary<string, Transform>
    {
        #region Public Custom Memebers
        public override string ToString()
        {
            // Get a string[] array of the keys for formatting with join()
            var keysArray = new string[this._prefabs.Count];
            this._prefabs.Keys.CopyTo(keysArray, 0);

            // Return a comma-sperated list inside square brackets (Pythonesque)
            return string.Format("[{0}]", System.String.Join(", ", keysArray));
        }
        #endregion Public Custom Memebers


        #region Internal Dict Functionality
        internal void _Add(string prefabName, Transform prefab)
        {
            this._prefabs.Add(prefabName, prefab);
        }

        internal bool _Remove(string prefabName)
        {
            return this._prefabs.Remove(prefabName);
        }

        internal void _Clear()
        {
            this._prefabs.Clear();
        }
        #endregion Internal Dict Functionality


        #region Dict Functionality
        // Internal (wrapped) dictionary
        private Dictionary<string, Transform> _prefabs = new Dictionary<string, Transform>();
        public int Count { get { return this._prefabs.Count; } }
        public bool ContainsKey(string prefabName)
        {
            return this._prefabs.ContainsKey(prefabName);
        }
        public bool TryGetValue(string prefabName, out Transform prefab)
        {
            return this._prefabs.TryGetValue(prefabName, out prefab);
        }

        #region Not Implimented

        public void Add(string key, Transform value)
        {
            throw new System.NotImplementedException("Read-Only");
        }

        public bool Remove(string prefabName)
        {
            throw new System.NotImplementedException("Read-Only");
        }

        public bool Contains(KeyValuePair<string, Transform> item)
        {
            string msg = "Use Contains(string prefabName) instead.";
            throw new System.NotImplementedException(msg);
        }

        public Transform this[string key]
        {
            get
            {
                Transform prefab;
                try
                {
                    prefab = this._prefabs[key];
                }
                catch (KeyNotFoundException)
                {
                    string msg = string.Format("A Prefab with the name '{0}' not found. " +
                                                "\nPrefabs={1}",
                                                key, this.ToString());
                    throw new KeyNotFoundException(msg);
                }

                return prefab;
            }
            set
            {
                throw new System.NotImplementedException("Read-only.");
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this._prefabs.Keys;
            }
        }


        public ICollection<Transform> Values
        {
            get
            {
                return this._prefabs.Values;
            }
        }


        #region ICollection<KeyValuePair<string, Transform>> Members
        private bool IsReadOnly { get { return true; } }
        bool ICollection<KeyValuePair<string, Transform>>.IsReadOnly { get { return true; } }

        public void Add(KeyValuePair<string, Transform> item)
        {
            throw new System.NotImplementedException("Read-only");
        }

        public void Clear() { throw new System.NotImplementedException(); }

        private void CopyTo(KeyValuePair<string, Transform>[] array, int arrayIndex)
        {
            string msg = "Cannot be copied";
            throw new System.NotImplementedException(msg);
        }

        void ICollection<KeyValuePair<string, Transform>>.CopyTo(KeyValuePair<string, Transform>[] array, int arrayIndex)
        {
            string msg = "Cannot be copied";
            throw new System.NotImplementedException(msg);
        }

        public bool Remove(KeyValuePair<string, Transform> item)
        {
            throw new System.NotImplementedException("Read-only");
        }
        #endregion ICollection<KeyValuePair<string, Transform>> Members
        #endregion Not Implimented




        #region IEnumerable<KeyValuePair<string, Transform>> Members
        public IEnumerator<KeyValuePair<string, Transform>> GetEnumerator()
        {
            return this._prefabs.GetEnumerator();
        }
        #endregion



        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._prefabs.GetEnumerator();
        }
        #endregion

        #endregion Dict Functionality

    }

}


public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
	private readonly IDictionary<TKey, TValue> _dictionary;
	
	public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
	{
		_dictionary = dictionary;
	}
	
	#region IDictionary<TKey,TValue> Members
	
	void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
	{
		throw ReadOnlyException();
	}
	
	public bool ContainsKey(TKey key)
	{
		return _dictionary.ContainsKey(key);
	}
	
	public ICollection<TKey> Keys
	{
		get { return _dictionary.Keys; }
	}
	
	bool IDictionary<TKey, TValue>.Remove(TKey key)
	{
		throw ReadOnlyException();
	}
	
	public bool TryGetValue(TKey key, out TValue value)
	{
		return _dictionary.TryGetValue(key, out value);
	}
	
	public ICollection<TValue> Values
	{
		get { return _dictionary.Values; }
	}
	
	public TValue this[TKey key]
	{
		get
		{
			return _dictionary[key];
		}
	}
	
	TValue IDictionary<TKey, TValue>.this[TKey key]
	{
		get
		{
			return this[key];
		}
		set
		{
			throw ReadOnlyException();
		}
	}
	
	#endregion
	
	#region ICollection<KeyValuePair<TKey,TValue>> Members
	
	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyException();
	}
	
	void ICollection<KeyValuePair<TKey, TValue>>.Clear()
	{
		throw ReadOnlyException();
	}
	
	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return _dictionary.Contains(item);
	}
	
	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		_dictionary.CopyTo(array, arrayIndex);
	}
	
	public int Count
	{
		get { return _dictionary.Count; }
	}
	
	public bool IsReadOnly
	{
		get { return true; }
	}
	
	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyException();
	}
	
	#endregion
	
	#region IEnumerable<KeyValuePair<TKey,TValue>> Members
	
	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}
	
	#endregion
	
	#region IEnumerable Members
	
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	#endregion
	
	private static System.Exception ReadOnlyException()
	{
		return new System.NotSupportedException("This dictionary is read-only");
	}
}