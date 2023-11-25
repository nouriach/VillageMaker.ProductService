using System.Text.Json;
using AutoMapper;
using VillageMaker.ProductService.Data.Interfaces;
using VillageMaker.ProductService.Domain.DTOs;
using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }
    
    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.MakerPublished:
                AddMaker(message);
                break;
            default:
                break;
        }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        Console.WriteLine("--> Determining Event");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        switch (eventType.Event)
        {
            case "Maker_Published":
                Console.WriteLine("---> Maker Published Event detected.");
                return EventType.MakerPublished;
            default:
                Console.WriteLine("---> Could not determine Event Type.");
                return EventType.Undetermined;
        }
    }

    private void AddMaker(string makerPublishedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetService<IProductRepo>();

            var makerPublishedDto = JsonSerializer.Deserialize<MakerPublishedDto>(makerPublishedMessage);

            try
            {
                var maker = _mapper.Map<Maker>(makerPublishedDto);
                if (!repo.ExternalMakerExists(maker.ExternalId))
                {
                    repo.CreateMaker(maker);
                    repo.SaveChanges();
                    Console.WriteLine("---> Maker added");
                }
                else
                {
                    Console.WriteLine("---> Maker already exists in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not add Maker to DB: {ex.Message}");
            }
        }
    }
}