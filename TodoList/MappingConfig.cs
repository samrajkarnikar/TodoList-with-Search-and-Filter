using AutoMapper;
using TodoList.DTOs;
using TodoList.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace TodoList
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Status, TodoListViewModel>().ReverseMap();
        }
    }
}
