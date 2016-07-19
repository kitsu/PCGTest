using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Director;

namespace PCGTest.Display
{
    public interface IView
    {
        void Update(int elapsed);
        void Draw(int elapsed);
        void LoadContent(object contentSource);
    }

    public interface IViewManager
    {
        void Add(IView view);
        void Remove(IView view);
        void Update(int elapsed);
        void Draw(int elapsed);
        void LoadContent(object contentSource);
        IView CreateTitleView(TitleViewController mapCtrl);
        IView CreateMapView(MapViewController mapCtrl);
    }

    public interface IMapView
    {
        int Width { get; }
        int Height { get; }
        void UpdateMap(int[,] map);
        void AddTileKey(KeyValuePair<int, string> key);
        void RemoveTileKey(int key);
    }
}
