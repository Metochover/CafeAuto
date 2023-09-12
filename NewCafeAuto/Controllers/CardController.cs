using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCafeAuto.DTO.CardDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;

        public CardController(NewCafeAutoContext newCafeAutoContext, IMapper mapper)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("addCard")]
        public async Task<IActionResult> addCard(AddCardDTO addCardDTO)
        {
            Cards cards = new Cards();
            
            cards.HasBillingCard = addCardDTO.HasBillingCard;
            cards.BillingCardBrand = addCardDTO.BillingCardBrand;
            cards.BillingCardLast4 = addCardDTO.BillingCardLast4;
            cards.BillingCardExpMonth = addCardDTO.BillingCardExpMonth;
            cards.BillingCardExpYear = addCardDTO.BillingCardExpYear;
            cards.TosAcceptedByIp = addCardDTO.TosAcceptedByIp;
            cards.UserId = addCardDTO.UserId;

            this.newCafeAutoContext.Add(cards);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<CardDTO>(cards));
        }

        [HttpPost("deleteCard")]
        public async Task<IActionResult> deleteCard(int id)
        {
            var card = await newCafeAutoContext.Cards.FirstOrDefaultAsync(e => e.Id == id);

            if (card == null) 
            {
                return BadRequest("Card not found");
            }
            
            this.newCafeAutoContext.Remove(card);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok("Card deleted");
        }
    }
}
