using AbstractProject.Abstractions.Models;
using AbstractProject.Domain.Tables;
using AutoMapper;

namespace AbstractProject.BusinessLogic.Items.Queries.Items.GetItems.Profiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<ItemEntity, ItemResponse>();
        }
    }
}