using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBL.DTOs.CardItemDTOs;
using TestCORE.Models;

namespace TestBL.Profiles
{
    public class CardItemProfile : Profile
    {
        public CardItemProfile()
        {
            CreateMap<CardItem, AddCardItemDTO>().ReverseMap();
            CreateMap<CardItem, UpdateCardItemDTO>().ReverseMap();
            CreateMap<CardItem, GetCardItemDTO>().ReverseMap();
            CreateMap<GetCardItemDTO, UpdateCardItemDTO>().ReverseMap();
        }
    }
}
