﻿namespace Macabresoft.Macabre2D.Editor.Library.Models.Content {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Macabresoft.Core;
    using Macabresoft.Macabre2D.Editor.Library.Services;

    /// <summary>
    /// Interface for a directory content node.
    /// </summary>
    public interface IContentDirectory : IContentNode {
        /// <summary>
        /// Gets the children of this directory.
        /// </summary>
        IReadOnlyCollection<ContentNode> Children { get; }

        /// <summary>
        /// Adds the child node.
        /// </summary>
        /// <param name="node">The child node to add.</param>
        void AddChild(ContentNode node);

        /// <summary>
        /// Finds a content node.
        /// </summary>
        /// <param name="splitContentPath">The split content path.</param>
        /// <param name="node">The found content node.</param>
        /// <returns>A value indicating whether or not the node was found.</returns>
        bool TryFindNode(string[] splitContentPath, out IContentNode node);

        /// <summary>
        /// Loads the child directories under this node.
        /// </summary>
        /// <param name="fileSystemService">The file service.</param>
        void LoadChildDirectories(IFileSystemService fileSystemService);

        /// <summary>
        /// Removes the child node.
        /// </summary>
        /// <param name="node">The child node to remove.</param>
        void RemoveChild(ContentNode node);
    }

    /// <summary>
    /// A directory content node.
    /// </summary>
    public class ContentDirectory : ContentNode, IContentDirectory {
        private readonly ObservableCollectionExtended<ContentNode> _children = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNode" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent content directory.</param>
        public ContentDirectory(string name, IContentDirectory parent) : base(name, parent) {
        }

        /// <inheritdoc />
        public IReadOnlyCollection<ContentNode> Children => this._children;

        /// <inheritdoc />
        public override string NameWithoutExtension => this.Name;

        /// <inheritdoc />
        public void AddChild(ContentNode node) {
            this._children.Add(node);
            node.Initialize(this);
        }

        /// <inheritdoc />
        public bool TryFindNode(string[] splitContentPath, out IContentNode node) {
            node = null;
            if (splitContentPath.Length == 1) {
                node = this.Children.FirstOrDefault(x => x.NameWithoutExtension == splitContentPath[0]);
            }
            else if (splitContentPath.Any()) {
                var children = this._children.OfType<IContentDirectory>().ToList();
                if (children.Any()) {
                    var newSplitPath = splitContentPath.TakeLast(splitContentPath.Length - 1).ToArray();
                    foreach (var child in this._children.OfType<IContentDirectory>()) {
                        if (child.TryFindNode(newSplitPath, out node)) {
                            break;
                        }
                    }
                }
            }
            
            return node != null;
        }

        /// <inheritdoc />
        public void LoadChildDirectories(IFileSystemService fileSystemService) {
            var currentDirectoryPath = this.GetFullPath();

            if (fileSystemService.DoesDirectoryExist(currentDirectoryPath)) {
                var directories = fileSystemService.GetDirectories(currentDirectoryPath).Where(x => Path.GetDirectoryName(x)?.StartsWith('.') == false);

                foreach (var directory in directories) {
                    this.LoadDirectory(fileSystemService, directory);
                }
            }
        }

        /// <inheritdoc />
        public void RemoveChild(ContentNode node) {
            this._children.Remove(node);
        }

        private void LoadDirectory(IFileSystemService fileSystemService, string path) {
            var node = new ContentDirectory(path, this);
            this.AddChild(node);
            node.LoadChildDirectories(fileSystemService);
        }
    }
}