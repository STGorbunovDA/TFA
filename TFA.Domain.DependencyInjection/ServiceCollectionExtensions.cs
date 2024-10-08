﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TFA.Domain.Authentication;
using TFA.Domain.Authorization;
using TFA.Domain.Models;
using TFA.Domain.Monitoring;
using TFA.Domain.UseCases.CreateForum;
using TFA.Domain.UseCases.CreateTopic;

namespace TFA.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddForumDomain(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg
                .AddOpenBehavior(typeof(MonitoringPipelineBehavior<,>))
                .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
                .RegisterServicesFromAssemblyContaining<ForumDomain>());
        
        services
            .AddScoped<IIntentionResolver, ForumIntentionResolver>()
            .AddScoped<IIntentionResolver, TopicIntentionResolver>();

        services
            .AddScoped<IIntentionManager, IntentionManager>()
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IPasswordManager, PasswordManager>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ISymmetricDecryptor, AesSymmetricEncryptorDecryptor>()
            .AddScoped<ISymmetricEncryptor, AesSymmetricEncryptorDecryptor>();

        services.AddValidatorsFromAssemblyContaining<ForumDomain>(includeInternalTypes: true);

        services.AddSingleton<DomainMetrics>();

        return services;
    }
}