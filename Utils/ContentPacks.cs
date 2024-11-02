using Panthera.Base;
using RoR2.ContentManagement;

namespace Panthera.Utils
{
    public class ContentPacks : IContentPackProvider
    {
        public ContentPack contentPack = new ContentPack();
        public string identifier => "com.Dexy.Panthera";

        public void Initialize()
        {
            ContentManager.collectContentPackProviders += ContentManager_collectContentPackProviders;
        }

        private void ContentManager_collectContentPackProviders(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
        {
            addContentPackProvider(this);
        }

        public System.Collections.IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
        {
            this.contentPack.identifier = this.identifier;
            contentPack.bodyPrefabs.Add(Prefab.bodyPrefabs.ToArray());
            contentPack.survivorDefs.Add(Prefab.SurvivorDefinitions.ToArray());
            contentPack.projectilePrefabs.Add(PantheraAssets.projectilePrefabs.ToArray());
            contentPack.skillFamilies.Add(Prefab.skillFamilies.ToArray());
            contentPack.skillDefs.Add(Prefab.skillDefs.ToArray());
            contentPack.entityStateTypes.Add(Prefab.entityStates.ToArray());
            contentPack.buffDefs.Add(Base.Buff.buffDefs.ToArray());
            contentPack.effectDefs.Add(PantheraAssets.effectDefs.ToArray());
            contentPack.masterPrefabs.Add(Prefab.masterPrefabs.ToArray());
            //contentPack.networkedObjectPrefabs.Add(Prefab.networkedObjectPrefabs.ToArray());
            //contentPack.networkSoundEventDefs.Add(PantheraAssets.networkSoundEventDefs.ToArray());
            //contentPack.unlockableDefs.Add(Unlockables.unlockableDefs.ToArray());

            args.ReportProgress(1f);
            yield break;
        }

        public System.Collections.IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
        {
            ContentPack.Copy(this.contentPack, args.output);
            args.ReportProgress(1f);
            yield break;
        }

        public System.Collections.IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
        {
            args.ReportProgress(1f);
            yield break;
        }
    }
}