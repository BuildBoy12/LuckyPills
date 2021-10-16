// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LuckyPills
{
    using System.IO;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.Loader;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <summary>
        /// Gets the configured effect config settings.
        /// </summary>
        [YamlIgnore]
        public Configs.Effects EffectConfigs { get; private set; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the directory path where the configuration file should reside.
        /// </summary>
        public string FolderPath { get; set; } = Path.Combine(Paths.Configs, "LuckyPills");

        /// <summary>
        /// Gets or sets the name of the file to store the configs in.
        /// </summary>
        public string FileName { get; set; } = "global.yml";

        /// <summary>
        /// Loads the effect configs.
        /// </summary>
        public void LoadEffects()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string filePath = Path.Combine(FolderPath, FileName);
            if (!File.Exists(filePath))
            {
                Log.Warn("Config not found, generating.");
                EffectConfigs = new Configs.Effects();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(EffectConfigs));
                return;
            }

            EffectConfigs = Loader.Deserializer.Deserialize<Configs.Effects>(File.ReadAllText(filePath));
            File.WriteAllText(filePath, Loader.Serializer.Serialize(EffectConfigs));
        }
    }
}