using SBAccountAPI.Models.Entities;
using AutoMapper.Attributes;

namespace SBAccountAPI.Models
{
    [MapsFrom(typeof(Todo))]
    public class TodoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}