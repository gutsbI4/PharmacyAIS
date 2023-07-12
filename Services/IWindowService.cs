using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Services
{
    public interface IWindowService
    {
        void OpenWindow<TViewModel>(TViewModel viewModel);
    }
}
