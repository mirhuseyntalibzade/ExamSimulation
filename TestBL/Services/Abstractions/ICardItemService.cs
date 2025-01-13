using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBL.DTOs.CardItemDTOs;
using TestCORE.Models;

namespace TestBL.Services.Abstractions;

public interface ICardItemService
{
     public Task AddCardItem(AddCardItemDTO addCardItemDTO);
     public Task<ICollection<GetCardItemDTO>> GetAllCardItemsAsync();
     public Task<GetCardItemDTO> GetCardItemByIdAsync(int Id);
     public Task UpdateAsync( UpdateCardItemDTO updateCardItemDTO);
     public Task DeleteAsync(int Id);
     public Task SoftDeleteAsync(int Id);
     public Task RevertSoftDeleteAsync(int Id);
}
