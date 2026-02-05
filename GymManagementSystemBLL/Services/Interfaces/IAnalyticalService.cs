using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementSystemBLL.ViewModels.Analytical;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IAnalyticalService
    {
        public AnalyticalViewModel? GetAnalytical();
    }
}
