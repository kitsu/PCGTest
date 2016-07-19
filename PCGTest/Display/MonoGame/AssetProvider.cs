using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PCGTest.Display.MonoGame
{
    class AssetProvider
    {
        ContentManager _contentSource;
        Texture2D mapSprites;
        Texture2D decoSprites;
        Texture2D itemSprites;
        Texture2D charSprites;

        public AssetProvider(ContentManager contentSource)
        {
            _contentSource = contentSource;
        }

        /// <summary>
        /// Provide a new SpriteMap instance using the map assets.
        /// </summary>
        public SpriteMap GetMapSprites()
        {
            var image = mapSprites ?? (mapSprites = _contentSource.Load<Texture2D>("map"));
            return SpriteMap.FromJson("Content/map.json", image);
        }
    }
}
