﻿namespace Macabresoft.Macabre2D.Tests.Framework.Physics {

    using Macabresoft.Macabre2D.Framework;
    using Microsoft.Xna.Framework;
    using NUnit.Framework;

    public static class RectangleColliderTests {

        [Test]
        [Category("Unit Tests")]
        [TestCase(0f, 0f, 1f, 0f, 0f, 0.5f, 0.5f, true, TestName = "Rectangle to Circle Collision - Circle Contains Square")]
        [TestCase(0f, 0f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Circle Collision - Square Contains Circle")]
        [TestCase(0.5f, 0f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Circle Collision - Right Collision")]
        [TestCase(-0.5f, 0f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Circle Collision - Left Collision")]
        [TestCase(3f, 3f, 1f, 0f, 0f, 1f, 1f, false, TestName = "Rectangle to Circle Collision - No Collision")]
        [TestCase(0f, -130f, 50f, 0f, -200f, 500f, 50f, true, TestName = "Rectangle to Circle Collision - Physics Test Failure")]
        public static void RectangleCollider_CollidesWithCircleTest(float x1, float y1, float r1, float x2, float y2, float w, float h, bool collisionOccured) {
            using var circleBody = new DynamicPhysicsBodyComponent();
            using var rectangleBody = new DynamicPhysicsBodyComponent();
            circleBody.Initialize(new GameEntity());
            rectangleBody.Initialize(new GameEntity());

            circleBody.Entity.SetWorldPosition(new Vector2(x1, y1));
            circleBody.Collider = new CircleCollider(r1);

            rectangleBody.Entity.SetWorldPosition(new Vector2(x2, y2));
            rectangleBody.Collider = new RectangleCollider(w, h);

            Assert.AreEqual(collisionOccured, circleBody.Collider.CollidesWith(rectangleBody.Collider, out var collision1));
            Assert.AreEqual(collisionOccured, rectangleBody.Collider.CollidesWith(circleBody.Collider, out var collision2));

            if (collisionOccured) {
                Assert.AreEqual(collision1.MinimumTranslationVector.Length(), collision2.MinimumTranslationVector.Length(), 0.0001f);
                Assert.AreEqual(collision1.FirstCollider, collision2.SecondCollider);
                Assert.AreEqual(collision1.SecondCollider, collision2.FirstCollider);
                Assert.AreEqual(collision1.FirstContainsSecond, collision2.SecondContainsFirst);
                Assert.AreEqual(collision1.SecondContainsFirst, collision2.FirstContainsSecond);

                var originalPosition = circleBody.Entity.Transform.Position;
                circleBody.Entity.SetWorldPosition(originalPosition + collision1.MinimumTranslationVector);
                Assert.False(circleBody.Collider.CollidesWith(rectangleBody.Collider, out collision1));
                circleBody.Entity.SetWorldPosition(originalPosition);

                rectangleBody.Entity.SetWorldPosition(rectangleBody.Entity.Transform.Position + collision2.MinimumTranslationVector);
                Assert.False(rectangleBody.Collider.CollidesWith(circleBody.Collider, out collision2));
            }
            else {
                Assert.Null(collision1);
                Assert.Null(collision2);
            }
        }

        [Test]
        [Category("Unit Tests")]
        [TestCase(0f, 0f, 1f, 1f, 0f, 0f, 0.5f, 0.5f, true, TestName = "Rectangle to Rectangle Collision - First Rectangle Contains Second Rectangle")]
        [TestCase(0f, 0f, 0.5f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Rectangle Collision - Second Rectangle Contains First Rectangle")]
        [TestCase(0.5f, 0f, 0.5f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Rectangle Collision - Right Collision")]
        [TestCase(-0.5f, 0f, 0.5f, 0.5f, 0f, 0f, 1f, 1f, true, TestName = "Rectangle to Rectangle Collision - Left Collision")]
        [TestCase(3f, 3f, 1f, 1f, 0f, 0f, 1f, 1f, false, TestName = "Rectangle to Rectangle Collision - No Collision")]
        [TestCase(0f, -130f, 100f, 100f, 0f, -200f, 500f, 100f, true, TestName = "Rectangle to Rectangle Collision - Physics Test Failure")]
        public static void RectangleCollider_RectangleCollidesWithRectangleTest(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2, bool collisionOccured) {
            using var rectangleBody1 = new DynamicPhysicsBodyComponent();
            using var rectangleBody2 = new DynamicPhysicsBodyComponent();
            rectangleBody1.Initialize(new GameEntity());
            rectangleBody2.Initialize(new GameEntity());

            rectangleBody1.Entity.SetWorldPosition(new Vector2(x1, y1));
            rectangleBody1.Collider = new RectangleCollider(w1, h1);

            rectangleBody2.Entity.SetWorldPosition(new Vector2(x2, y2));
            rectangleBody2.Collider = new RectangleCollider(w2, h2);

            Assert.AreEqual(collisionOccured, rectangleBody1.Collider.CollidesWith(rectangleBody2.Collider, out var collision1));
            Assert.AreEqual(collisionOccured, rectangleBody2.Collider.CollidesWith(rectangleBody1.Collider, out var collision2));

            if (collisionOccured) {
                Assert.AreEqual(collision1.MinimumTranslationVector.Length(), collision2.MinimumTranslationVector.Length(), 0.0001f);
                Assert.AreEqual(collision1.FirstCollider, collision2.SecondCollider);
                Assert.AreEqual(collision1.SecondCollider, collision2.FirstCollider);
                Assert.AreEqual(collision1.FirstContainsSecond, collision2.SecondContainsFirst);
                Assert.AreEqual(collision1.SecondContainsFirst, collision2.FirstContainsSecond);

                var originalPosition = rectangleBody1.Entity.Transform.Position;
                rectangleBody1.Entity.SetWorldPosition(originalPosition + collision1.MinimumTranslationVector);
                Assert.False(rectangleBody1.Collider.CollidesWith(rectangleBody2.Collider, out collision1));
                rectangleBody1.Entity.SetWorldPosition(originalPosition);

                rectangleBody2.Entity.SetWorldPosition(rectangleBody2.Entity.Transform.Position + collision2.MinimumTranslationVector);
                Assert.False(rectangleBody2.Collider.CollidesWith(rectangleBody1.Collider, out collision2));
            }
            else {
                Assert.Null(collision1);
                Assert.Null(collision2);
            }
        }
    }
}