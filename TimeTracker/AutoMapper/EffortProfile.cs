using AutoMapper;
using DataLayer.Model;
using TimeTracker.Models.Dto;

namespace TimeTracker.AutoMapper
{
	public class EffortProfile : Profile
	{
		public EffortProfile()
		{
			CreateMap<EffortDto, Effort>();
			CreateMap<Effort, EffortDto>();
		}
	}
}
