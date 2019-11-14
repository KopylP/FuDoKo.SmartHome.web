
using FuDoKo.SmartHome.web.ViewModels;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Hubs
{
    public interface ITypedHubClient
    {
        Task UpdateSensor(SensorViewModel model);
    }
}
