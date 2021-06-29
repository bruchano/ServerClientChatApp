using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pie.EntityFramework.Services
{
    public class ChatDataService : IChatDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public ChatDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
