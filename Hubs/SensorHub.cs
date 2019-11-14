using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Hubs
{
    [Authorize]
    public class SensorHub: Hub<ITypedHubClient>
    {

    }
}
