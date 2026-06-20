using Oxide.Plugins;

namespace Oxide.Plugins
{
    [Info("BlockTrainWagonLoot", "MrHawk", "1.0.0")]
    [Description("Removes the unlootable/bugged wagons")]

    public class BlockTrainWagonLoot : RustPlugin
    {
        private void OnServerInitialized()
        {
            foreach (var entity in BaseNetworkable.serverEntities)
            {
                if (
                    entity != null &&
                    entity.ShortPrefabName.Contains(
                        "trainwagonunloadableloot"
                    )
                )
                {
                    Puts(
                        $"Removing existing: {entity.ShortPrefabName}"
                    );

                    entity.Kill();
                }
            }
        }

        private void OnEntitySpawned(
            BaseNetworkable entity
        )
        {
            if (
                entity != null &&
                entity.ShortPrefabName.Contains(
                    "trainwagonunloadableloot"
                )
            )
            {
                Puts(
                    $"Blocking spawn: {entity.ShortPrefabName}"
                );

                timer.Once(0.1f, () =>
                {
                    if (
                        entity != null &&
                        !entity.IsDestroyed
                    )
                    {
                        entity.Kill();
                    }
                });
            }
        }
    }
}
