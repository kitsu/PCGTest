using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Display;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Director
{
    class GameManager
    {
        IViewManager _viewMan;
        IMapView mapView;
        MapViewController mapCtrl;

        public GameManager(IViewManager viewMan)
        {
            _viewMan = viewMan;
            ShowStartScreen();
        }

        /// <summary>
        /// Initialize a title screen pair
        /// </summary>
        void ShowStartScreen()
        {
            // FIXME create title screen with start menu
            CreateMapView();
        }

        /// <summary>
        /// Initialize a game map pair
        /// </summary>
        void CreateMapView()
        {
            // Setup map view and controller
            mapView = _viewMan.CreateMapView();
            mapCtrl = new MapViewController();
            var vp = mapCtrl.AddViewport(new Rect(0, 0, mapView.Width, mapView.Height));
            // Bind events
            mapCtrl.UpdateTileKeys += mapView.UpdateTileKeys;
            mapCtrl.AddTileKey += mapView.AddTileKey;
            mapCtrl.RemoveTileKey += mapView.RemoveTileKey;
            vp.UpdateMap += mapView.UpdateMap;
            // Initialize map
            mapCtrl.Initialize();
        }
    }
}
