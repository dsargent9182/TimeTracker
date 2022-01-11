using AutoMapper;
using DataLayer.Model;
using TimeTracker.Models.Dto;
namespace TimeTracker.AutoMapper
{
	public class AssignmentProfile : Profile
	{
		public AssignmentProfile()
		{
			CreateMap<Assignment, AssignmentDto>();
			CreateMap<AssignmentDto, Assignment>();
		}
	}
}
