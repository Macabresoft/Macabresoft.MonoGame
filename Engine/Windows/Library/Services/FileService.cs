﻿namespace Macabre2D.Engine.Windows.Services {

    using Macabre2D.Engine.Windows.Common;
    using Macabre2D.Engine.Windows.ServiceInterfaces;
    using System.IO;
    using System.Reflection;

    public sealed class FileService : IFileService {

        public FileService() {
            this.ProjectDirectoryPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), "..", "..", "..", "..", "..", @"Project\");
        }

        public string ProjectDirectoryPath { get; }

        public string GetAutoSaveDirectory() {
            var autoSavePath = Path.Combine(this.ProjectDirectoryPath, FileHelper.ProjectAutoSaveFolder);
            if (!Directory.Exists(autoSavePath)) {
                var directory = Directory.CreateDirectory(autoSavePath);
                directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            return autoSavePath;
        }
    }
}