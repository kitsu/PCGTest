using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

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

        public void ItemSelected(string item)
        {
            // Transition to selected state
            _parent.Remove(this);
            _parent.CreateMapView();
        }

        public void Dispose()
        {
        }
    }
}
