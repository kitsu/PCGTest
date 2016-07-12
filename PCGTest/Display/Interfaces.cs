using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Display
{
    public interface IView
    {
        void Update(int ellapsed);
        void Draw(int ellapsed);
        void LoadContent(object contentSource);
    }

    public interface IViewManager
    {
        void Add(IView view);
        void Remove(IView view);
        void Update(int ellapsed);
        void Draw(int ellapsed);
        void LoadContent(object contentSource);
        IMapView CreateMapView();
    }

    public interface IMapView
    {
        int Width { get; }
        int Height { get; }
        void UpdateTileKeys(Dictionary<int, string> keys);
        void AddTileKey(int key, string value);
        void RemoveTileKey(int key);
        void UpdateMap(IEnumerable<int> map);
    }
}
