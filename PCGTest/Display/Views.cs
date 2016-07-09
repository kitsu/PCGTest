using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Display
{
    interface IView
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void LoadContent(ContentManager content);
    }
}
