using CommandLine;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

var mediator = BuildContainer().GetService<IMediator>();

await ProcessArgsAsync(mediator, args);

static Parser _createParser() => new(options =>
{
    options.CaseSensitive = false;
    options.HelpWriter = Console.Out;
    options.IgnoreUnknownArguments = true;
});

static ServiceProvider BuildContainer()
{
    IServiceCollection services = new ServiceCollection();

    services.AddCliServices();

    return services.BuildServiceProvider();
}

static async Task ProcessArgsAsync(IMediator mediator, string[] args)
{
    if (args.Length == 0 || args[0].StartsWith("-"))
    {
        args = new string[1] { "default" }.Concat(args).ToArray();
    }

    var verbs = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(type => type.GetCustomAttributes(typeof(VerbAttribute), true).Length > 0)
        .ToArray();

    var result = _createParser().ParseArguments(args, verbs);

    await mediator.Send(result!.Value);
}


