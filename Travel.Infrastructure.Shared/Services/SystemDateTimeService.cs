using System;
using Travel.Core.Application.Interfaces.Shared;

namespace Travel.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}