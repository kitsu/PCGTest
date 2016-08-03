using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PCGTest.Display;

namespace PCGTest.Director
{
    /// <summary>
    /// Controller implementing start screen and menu
    /// </summary>
    public class TitleViewController: IDisposable
    {
        // Other members
        GameManager _parent;

        public TitleViewController(GameManager parent)
        {
            _parent = parent;
        }

        public void Initialize(ITitleView view)
        {
            view.WhenSelected.Subscribe(ItemSelected);
        }

        public void ItemSelected(string item)
        {
            // Transition to selected state
            _parent.Remove(this);
            // This code is for when start/load are picked
            _parent.StartSim();
            _parent.CreateMapView();
        }

        public void Dispose()
        {
        }
    }
}
