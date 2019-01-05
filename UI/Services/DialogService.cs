﻿namespace Macabre2D.UI.Services {

    using Macabre2D.Framework;
    using Macabre2D.UI.Common;
    using Macabre2D.UI.Models;
    using Macabre2D.UI.ServiceInterfaces;
    using Macabre2D.UI.Views.Dialogs;
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Windows;
    using Unity;
    using Unity.Injection;
    using Unity.Resolution;

    public sealed class DialogService : IDialogService {
        private readonly IUnityContainer _container;
        private readonly Serializer _serializer;

        public DialogService(IUnityContainer container, Serializer serializer) {
            this._container = container;
            this._serializer = serializer;
        }

        public bool ShowCreateProjectDialog(out Project project, string initialDirectory = null) {
            var window = this._container.Resolve<CreateProjectDialog>();
            window.ViewModel.FilePath = initialDirectory;
            window.ShowDialog();
            var result = window.DialogResult.HasValue && window.DialogResult.Value;

            if (result) {
                project = window.ViewModel.Project;
                project.PathToProject = Path.Combine(window.ViewModel.FilePath, project.SafeName + FileHelper.ProjectExtension);
            }
            else {
                project = null;
            }

            return result;
        }

        public bool ShowFileBrowser(string filter, out string path, string initialDirectory = null) {
            var dialog = new OpenFileDialog() {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = filter,
                InitialDirectory = initialDirectory
            };

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value) {
                path = dialog.FileName;
                return true;
            }

            path = string.Empty;
            return false;
        }

        public bool ShowFolderBrowser(out string path, string initialDirectory = null) {
            var dialog = new System.Windows.Forms.FolderBrowserDialog() {
                SelectedPath = initialDirectory
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                path = dialog.SelectedPath;
                return true;
            }

            path = string.Empty;
            return false;
        }

        public SaveDiscardCancelResult ShowSaveDiscardCancelDialog() {
            var projectService = this._container.Resolve<IProjectService>();
            var sceneService = this._container.Resolve<ISceneService>();
            var result = SaveDiscardCancelResult.Save;

            if (projectService.HasChanges || sceneService.HasChanges) {
                var window = this._container.Resolve<SaveDiscardCancelDialog>();
                window.ShowDialog();
                result = window.Result;
            }

            return result;
        }

        public SceneAsset ShowSaveSceneWindow(Project project, Scene scene) {
            var window = this._container.Resolve<SaveSceneDialog>(new ParameterOverride("project", project));
            var result = window.ShowDialog();
            if (result.HasValue && result.Value) {
                var fileName = window.ViewModel.FileName + FileHelper.SceneExtension;
                FolderAsset parent;

                if (window.ViewModel.SelectedAsset.Type == AssetType.Folder && window.ViewModel.SelectedAsset is FolderAsset folder) {
                    parent = folder;
                }
                else {
                    parent = window.ViewModel.SelectedAsset.Parent;
                }

                var sceneAsset = new SceneAsset(fileName) {
                    Parent = parent
                };

                scene.SaveAsJson(sceneAsset.GetPath(), this._serializer);

                if (project.StartUpSceneAsset == null) {
                    project.StartUpSceneAsset = sceneAsset;
                }

                return sceneAsset;
            }

            return null;
        }

        public Asset ShowSelectAssetDialog(Project project, AssetType assetMask, AssetType selectableAssetMask) {
            Asset asset = null;
            var window = this._container.Resolve<SelectAssetDialog>(
                new DependencyOverride(typeof(Project), new InjectionParameter(project)),
                new ParameterOverride("assetMask", new InjectionParameter(assetMask)),
                new ParameterOverride("selectableAssetMask", new InjectionParameter(selectableAssetMask)));

            var result = window.ShowDialog();
            if (result.HasValue && result.Value && window.ViewModel.SelectedAsset is Asset selectedAsset) {
                asset = selectedAsset;
            }

            return asset;
        }

        public (Type Type, string Name) ShowSelectTypeAndNameDialog(Type type) {
            var window = this._container.Resolve<SelectTypeDialog>(new DependencyOverride(typeof(Type), new InjectionParameter(type)));
            window.ShowNameTextBox = true;
            var result = window.ShowDialog();

            if (result.HasValue && result.Value && window.ViewModel != null) {
                return (window.ViewModel.SelectedType, window.ViewModel.Name);
            }

            return (null, null);
        }

        public Type ShowSelectTypeDialog(Type type) {
            var window = this._container.Resolve<SelectTypeDialog>(new DependencyOverride(typeof(Type), new InjectionParameter(type)));
            var result = window.ShowDialog();
            return result.HasValue && result.Value ? window.ViewModel?.SelectedType : null;
        }

        public MessageBoxResult ShowYesNoCancelMessageBox(string title, string message) {
            return MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
        }

        public bool ShowYesNoMessageBox(string title, string message) {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}