using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Display;
using PCGTest.Simulation;
using PCGTest.Utilities.Geometry;

namespace PCGTest.Director
{
    public class GameManager
    {
        IViewManager _viewMan;
        Dictionary<IDisposable, IView> CVPairs;
        public SimulationManager SimMan;

        public GameManager(IViewManager viewMan)
        {
            _viewMan = viewMan;
            CVPairs = new Dictionary<IDisposable, IView>();
            CreateTitleView();
        }

        /// <summary>
        /// Initialize a title screen pair
        /// </summary>
        public void CreateTitleView()
        {
            var ctrl = new TitleViewController(this);
            var view = _viewMan.CreateTitleView(ctrl);
            CVPairs[ctrl] = view;
        }

        /// <summary>
        /// Initialize a game map pair
        /// </summary>
        public void CreateMapView()
        {
            // Setup map view and controller
            var ctrl = new MapViewController(this);
            var view = _viewMan.CreateMapView(ctrl);
            CVPairs[ctrl] = view;
        }

        public void Remove(IDisposable ctrl)
        {
            var view = CVPairs[ctrl];
            _viewMan.Remove(view);
            CVPairs.Remove(ctrl);
        }

        public void StartSim()
        {
            SimMan = new SimulationManager((int)DateTime.Now.Ticks);
        }
    }
}
