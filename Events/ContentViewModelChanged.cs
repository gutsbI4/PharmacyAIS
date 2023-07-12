using PharmacyAIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Events
{
    public class ContentViewModelChanged
    {
        public ViewModelBase NewContentViewModel { get; set; }
    }
}
