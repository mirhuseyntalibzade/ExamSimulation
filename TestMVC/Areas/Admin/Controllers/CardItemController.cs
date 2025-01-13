using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TestBL.DTOs.CardItemDTOs;
using TestBL.Exceptions;
using TestBL.Services.Abstractions;
using TestBL.Validators.CardItemValidator;
using TestCORE.Models;


namespace TestMVC.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize]
//[Authorize(Roles = "Admin")]
public class CardItemController : Controller
{
    readonly ICardItemService _service;
    readonly IMapper _mapper;
    public CardItemController(ICardItemService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        
        try
        {
            ICollection<GetCardItemDTO> cardItems = await _service.GetAllCardItemsAsync();
            return View(cardItems);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddCardItemDTO addCardItemDTO)
    {
        var validator = new AddCardItemValidator();
        var results = validator.Validate(addCardItemDTO);

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                ModelState.AddModelError("", failure.ErrorMessage);
            }
            return View();
        }
        try
        {
            await _service.AddCardItem(addCardItemDTO);
            return RedirectToAction("Index");
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> Delete(int Id)
    {
        try
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
        catch (NotFoundItemException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> SoftDelete(int Id)
    {
        try
        {
            await _service.SoftDeleteAsync(Id);
            return RedirectToAction("Index");
        }
        catch (NotFoundItemException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> RevertSoftDelete(int Id)
    {
        try
        {
            await _service.RevertSoftDeleteAsync(Id);
            return RedirectToAction("Index");
        }
        catch (NotFoundItemException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public async Task<IActionResult> Update(int Id)
    {

        try
        {
            GetCardItemDTO cardItem = await _service.GetCardItemByIdAsync(Id);
            UpdateCardItemDTO updateCardItem = _mapper.Map<UpdateCardItemDTO>(cardItem);
            return View(updateCardItem);
        }
        catch (NotFoundItemException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

        [HttpPost]
    public async Task<IActionResult> Update(UpdateCardItemDTO updateCardItemDTO)
    {
        var validator = new UpdateCardItemValidator();
        var results = validator.Validate(updateCardItemDTO);

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                ModelState.AddModelError("", failure.ErrorMessage);
            }
            return View();
        }
        try
        {
            await _service.UpdateAsync(updateCardItemDTO);
            return RedirectToAction("Index");
        }
        catch (NotFoundItemException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
