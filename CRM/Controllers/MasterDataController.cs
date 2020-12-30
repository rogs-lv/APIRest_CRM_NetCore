using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Helpers;
using CRM.Models;
using CRM.Service.Interface;
using CRM.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService masterService;
        private readonly IUriService uriService;
        public MasterDataController(IMasterDataService masterDtService, IUriService uriService) {
            this.masterService  = masterDtService;
            this.uriService     = uriService;
        }
        
        [HttpGet]
        [Route("GetItem")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Response<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetItem(string idItem, string whsCode, string priceList) {
            var item = masterService.GetItem(idItem, whsCode, priceList);
            if (item != null)
                return Ok(new Response<Item>(item));
            else
                return NotFound("No se encontraron artículos");
        }
        
        [HttpGet]
        [Route("GetAllItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResponse<List<Item>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status200OK)]
        public IActionResult GetAllItems([FromQuery] PaginationFilter filter) {
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.WhsCode, filter.PriceList);
            int countRows = 0;
            var route = Request.Path.Value;
            List<Item> listItem = masterService.GetAllItems(ref countRows, filter);
            if (listItem.Count() > 0)
            {
                var pagedResponse = PaginationHelper.CreatePagedReponse<Item>(listItem, validFilter, countRows, uriService, route);
                return Ok(pagedResponse);
                // return Ok(new PagedResponse<List<Item>>(listItem, validFilter.PageNumber, validFilter.PageSize));
            }
            else
                return new EmptyResult();
        }
    }
}
