﻿using MediatR;
using TFA.Domain.Authentication;
using TFA.Domain.Monitoring;

namespace TFA.Domain.UseCases.SignOn;

public record SignOnCommand(string Login, string Password) : IRequest<IIdentity>, IMonitoredRequest
{
    private const string CounterName = "account.signedon";
    
    public void MonitorSuccess(DomainMetrics metrics) => 
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));

    public void MonitorFailure(DomainMetrics metrics) => 
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
}