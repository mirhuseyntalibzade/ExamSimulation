using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBL.DTOs.CardItemDTOs;
using TestBL.Exceptions;
using TestBL.Services.Abstractions;
using TestCORE.Models;
using TestDAL.Repositories.Abstractions;

namespace TestBL.Services.Concretes;

public class CardItemService : ICardItemService
{
    readonly ICardItemRepository _repository;
    readonly IMapper _mapper;
    readonly IWebHostEnvironment _webHostEnvironment;
    public CardItemService(ICardItemRepository repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _repository = repository;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task AddCardItem(AddCardItemDTO addCardItemDTO)
    {
        CardItem cardItem = _mapper.Map<CardItem>(addCardItemDTO);
        string root = _webHostEnvironment.WebRootPath;
        string fileName = addCardItemDTO.Image.FileName;
        string filePath = root + "/uploads/carditem/" + fileName;
        
        using (FileStream stream = new FileStream(filePath.Replace("\\","/"), FileMode.Create))
        {
            await addCardItemDTO.Image.CopyToAsync(stream);
        }
        cardItem.ImageURL = fileName;
        cardItem.CreatedDate = DateTime.Now;
        await _repository.AddAsync(cardItem);
        int result = await _repository.SaveChangesAsync();
        if (result == 0)
        {
            throw new OperationNotValidException("Couldnt save changes. :(");
        }
    }

    public async Task DeleteAsync(int Id)
    {
        CardItem cardItem = await _repository.GetbyIdAsync(Id);
        if (cardItem is null)
        {
            throw new NotFoundItemException("Couldnt find item.");
        }
        _repository.Delete(cardItem);
        int result = await _repository.SaveChangesAsync();

        if (result == 0)
        {
            throw new OperationNotValidException("Couldnt save changes. :(");
        }
    }

    public async Task<ICollection<GetCardItemDTO>> GetAllCardItemsAsync()
    {

        ICollection<CardItem> cardItems = await _repository.GetAllAsync();
        return _mapper.Map<ICollection<GetCardItemDTO>>(cardItems);
    }

    public async Task<GetCardItemDTO> GetCardItemByIdAsync(int Id)
    {
        CardItem cardItem = await _repository.GetbyIdAsync(Id);
        if (cardItem is null)
        {
            throw new NotFoundItemException("Couldnt find item.");
        }
        return _mapper.Map<GetCardItemDTO>(cardItem);
    }

    public async Task RevertSoftDeleteAsync(int Id)
    {
        CardItem cardItem = await _repository.GetbyIdAsync(Id);
        if (cardItem is null)
        {
            throw new NotFoundItemException("Couldnt find item.");
        }
        if (!cardItem.isDeleted)
        {
            throw new OperationNotValidException("Item is already reverted. :(");
        }
        cardItem.isDeleted = false;
        cardItem.DeletedDate = null;
        _repository.Update(cardItem);
        int result = await _repository.SaveChangesAsync();

        if (result == 0)
        {
            throw new OperationNotValidException("Couldnt save changes. :(");
        }
    }

    public async Task SoftDeleteAsync(int Id)
    {
        CardItem cardItem = await _repository.GetbyIdAsync(Id);
        if (cardItem is null)
        {
            throw new NotFoundItemException("Couldnt find item.");
        }
        if (cardItem.isDeleted)
        {
            throw new OperationNotValidException("Item is already deleted. :(");
        }
        cardItem.isDeleted = true;
        cardItem.DeletedDate = DateTime.Now;
        _repository.Update(cardItem);
        int result = await _repository.SaveChangesAsync();

        if (result == 0)
        {
            throw new OperationNotValidException("Couldnt save changes. :(");
        }
    }

    public async Task UpdateAsync(UpdateCardItemDTO updateCardItemDTO)
    {
        CardItem cardItem = await _repository.GetByConditionAsync(c=>c.Id == updateCardItemDTO.Id);
        if (cardItem is null)
        {
            throw new NotFoundItemException("Couldnt find item.");
        }
        string root = _webHostEnvironment.WebRootPath;
        string fileName = updateCardItemDTO.Image.FileName;
        string filePath = root + "/uploads/carditem/" + fileName;

        using (FileStream stream = new FileStream(filePath.Replace("\\", "/"), FileMode.Create))
        {
            await updateCardItemDTO.Image.CopyToAsync(stream);
        }
        CardItem updatedItem = _mapper.Map<CardItem>(updateCardItemDTO);
        updatedItem.ImageURL = fileName;
        updatedItem.CreatedDate = cardItem.CreatedDate;
        updatedItem.UpdatedDate = DateTime.Now;
        _repository.Update(updatedItem);
        
        int result = await _repository.SaveChangesAsync();

        if (result == 0)
        {
            throw new OperationNotValidException("Couldnt save changes. :(");
        }
    }
}
