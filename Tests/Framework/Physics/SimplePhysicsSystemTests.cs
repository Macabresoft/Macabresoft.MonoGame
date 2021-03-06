﻿namespace Macabresoft.Macabre2D.Tests.Framework.Physics {

    using Macabresoft.Macabre2D.Framework;
    using Microsoft.Xna.Framework;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public static class SimplePhysicsSystemTests {

        [Test]
        [Category("Unit Tests")]
        [TestCase(-2f, 0f, 1f, 0f, true, true, TestName = "Raycast to Circle Collider - Collision")]
        [TestCase(-2f, 0f, 1f, 0f, false, false, TestName = "Raycast to Circle Collider - Different Layers")]
        [TestCase(-2f, 2f, 1f, 0f, true, false, TestName = "Raycast to Circle Collider - No Collision")]
        [TestCase(-2f, 2f, 1f, 0f, false, false, TestName = "Raycast to Circle Collider - No Collision / Different Layers")]
        public static void RaycastCircleTest(float raycastX, float raycastY, float directionX, float directionY, bool layersCompatible, bool raycastHit) {
            var game = Substitute.For<IGame>();
            var scene = new GameScene();
            var project = Substitute.For<IGameProject>();
            var layerSettings = new LayerSettings();
            var gameSettings = Substitute.For<IGameSettings>();
            var raycastLayer = Layers.Layer12;

            gameSettings.Layers.Returns(layerSettings);
            project.Settings.Returns(gameSettings);

            if (!layersCompatible) {
                raycastLayer = Layers.Layer13;
            }

            var physicsSystem = scene.AddSystem<SimplePhysicsSystem>();
            var circleEntity = scene.AddChild();
            using var circleBody = circleEntity.AddComponent<SimplePhysicsBodyComponent>();
            circleEntity.SetWorldPosition(Vector2.Zero);
            circleBody.Collider = new CircleCollider(1f);
            circleEntity.Layers = Layers.Layer12;
            scene.Initialize(game);
            
            var result = physicsSystem.TryRaycast(new Vector2(raycastX, raycastY), new Vector2(directionX, directionY), 5f, raycastLayer, out var hit);
            Assert.AreEqual(raycastHit, result);
        }

        [Test]
        [Category("Unit Tests")]
        [TestCase(0f, 0.6499903f, 0f, -1f, 0.666667f, true, TestName = "Raycast to Line Collider - Collision #1")]
        public static void RaycastLineTest(float raycastX, float raycastY, float directionX, float directionY, float distance, bool raycastHit) {
            var game = Substitute.For<IGame>();
            var scene = new GameScene();
            var project = Substitute.For<IGameProject>();
            var layerSettings = new LayerSettings();
            var gameSettings = Substitute.For<IGameSettings>();
            
            gameSettings.Layers.Returns(layerSettings);
            project.Settings.Returns(gameSettings);

            var physicsSystem = scene.AddSystem<SimplePhysicsSystem>();

            var lineEntity = scene.AddChild();
            using var lineBody = lineEntity.AddComponent<SimplePhysicsBodyComponent>();
            lineEntity.SetWorldPosition(Vector2.Zero);
            lineBody.Collider = new LineCollider(new Vector2(-1f, 0f), new Vector2(1f, 0f));
            lineEntity.Layers = Layers.Default;
            lineBody.Initialize(scene);

            scene.Initialize(game);
            physicsSystem.TimeStep = 1f;
            physicsSystem.Update(new FrameTime(new GameTime(new System.TimeSpan(0, 0, 1), new System.TimeSpan(0, 0, 1)), 1), new InputState());

            var result = physicsSystem.TryRaycast(new Vector2(raycastX, raycastY), new Vector2(directionX, directionY), distance, Layers.Default, out var hit);
            Assert.AreEqual(raycastHit, result);
            result = physicsSystem.TryRaycast(new Vector2(raycastX, raycastY), new Vector2(directionX, directionY), distance, Layers.Default, out hit);
            Assert.AreEqual(raycastHit, result);

            physicsSystem.Update(new FrameTime(new GameTime(new System.TimeSpan(0, 0, 2), new System.TimeSpan(0, 0, 1)), 1), new InputState());

            result = physicsSystem.TryRaycast(new Vector2(raycastX, raycastY), new Vector2(directionX, directionY), distance, Layers.Default, out hit);
            Assert.AreEqual(raycastHit, result);
        }
    }
}