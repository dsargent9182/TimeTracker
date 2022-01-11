using AutoMapper;
using DataLayer.Model;
using TimeTracker.Models.Dto;

namespace TimeTracker.AutoMapper
{
	public class ContractProfile : Profile
	{
		public ContractProfile()
		{
			CreateMap<Contract, ContractDto>();
			CreateMap<ContractDto, Contract>();
		}
	}
}
