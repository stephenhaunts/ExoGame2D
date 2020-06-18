/*
MIT License

Copyright (c) 2020

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using GameEngine.Interfaces;
using GameEngine.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.SceneManagement
{
    public class Scene
    {
        private readonly List<IRenderNode> Layer1 = new List<IRenderNode>();
        private readonly List<IRenderNode> Layer2 = new List<IRenderNode>();
        private readonly List<IRenderNode> Layer3 = new List<IRenderNode>();
        private readonly List<IRenderNode> Layer4 = new List<IRenderNode>();
        private readonly List<IRenderNode> Layer5 = new List<IRenderNode>();

        public Scene()
        {
        }

        public void AddSpriteToLayer(RenderLayerEnum layer, IRenderNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrEmpty(node.Name))
            {
                throw new InvalidOperationException("Render node can not have an empty name.");
            }

            if (node.IsAssetOfType(typeof(Sprite))  || node.IsAssetOfType(typeof(AnimatedSprite)))
            {
                CollisionManager.AddSpriteToCollisionManager(node.GetSprite(), node.Name);
            }

            switch (layer)
            {
                case RenderLayerEnum.LAYER1:
                    Layer1.Add(node);
                    break;

                case RenderLayerEnum.LAYER2:
                    Layer2.Add(node);
                    break;

                case RenderLayerEnum.LAYER3:
                    Layer3.Add(node);
                    break;

                case RenderLayerEnum.LAYER4:
                    Layer4.Add(node);
                    break;

                case RenderLayerEnum.LAYER5:
                    Layer5.Add(node);
                    break;
                default:
                    break;
            }
        }

        public bool RemoveSpriteFromLayer(RenderLayerEnum layer, IRenderNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            switch (layer)
            {
                case RenderLayerEnum.LAYER1:
                    foreach (IRenderNode n in Layer1.ToArray())
                    {
                        if (n == node)
                        {
                            _ = Layer1.Remove(node);
                            return true;
                        }
                    }
                    break;

                case RenderLayerEnum.LAYER2:
                    foreach (IRenderNode n in Layer2.ToArray())
                    {
                        if (n == node)
                        {
                            _ = Layer2.Remove(node);
                            return true;
                        }
                    }
                    break;

                case RenderLayerEnum.LAYER3:
                    foreach (IRenderNode n in Layer3.ToArray())
                    {
                        if (n == node)
                        {
                            _ = Layer3.Remove(node);
                            return true;
                        }
                    }
                    break;

                case RenderLayerEnum.LAYER4:
                    foreach (IRenderNode n in Layer4.ToArray())
                    {
                        if (n == node)
                        {
                            _ = Layer4.Remove(node);
                            return true;
                        }
                    }
                    break;

                case RenderLayerEnum.LAYER5:
                    foreach (IRenderNode n in Layer5.ToArray())
                    {
                        if (n == node)
                        {
                            _ = Layer5.Remove(node);
                            return true;
                        }
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

        public void RenderScene(GameTime gameTime)
        {
            foreach (IRenderNode node in Layer1.ToArray())
            {
                node.Draw(gameTime);
            }

            foreach (IRenderNode node in Layer2.ToArray())
            {
                node.Draw(gameTime);
            }

            foreach (IRenderNode node in Layer3.ToArray())
            {
                node.Draw(gameTime);
            }

            foreach (IRenderNode node in Layer4.ToArray())
            {
                node.Draw(gameTime);
            }

            foreach (IRenderNode node in Layer5.ToArray())
            {
                node.Draw(gameTime);
            }
        }

        public void UpdateGameLogic(GameTime gameTime)
        {
            SoundEffectPlayer.ProcessSoundEvents();

            foreach (IRenderNode node in Layer1.ToArray())
            {
                node.Update(gameTime);
            }

            foreach (IRenderNode node in Layer2.ToArray())
            {
                node.Update(gameTime);
            }

            foreach (IRenderNode node in Layer3.ToArray())
            {
                node.Update(gameTime);
            }

            foreach (IRenderNode node in Layer4.ToArray())
            {
                node.Update(gameTime);
            }

            foreach (IRenderNode node in Layer5.ToArray())
            {
                node.Update(gameTime);
            }
        }
    }
}
