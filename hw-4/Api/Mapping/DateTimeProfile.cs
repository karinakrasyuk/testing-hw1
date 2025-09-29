using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace Api.Mapping;

public class DateTimeProfile : Profile
{
    public DateTimeProfile()
    {
        CreateMap<Timestamp, DateTime>().ConstructUsing(x => x.ToDateTime());
        CreateMap<DateTime, Timestamp>().ConstructUsing(x => Timestamp.FromDateTime(x));
    }
}