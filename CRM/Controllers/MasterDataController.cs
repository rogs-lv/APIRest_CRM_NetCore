﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Entity;
using CRM.Helpers;
using CRM.Models;
using CRM.Service.Interface;
using CRM.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [EnableCors("CORS")]
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
        /// <summary>
        /// Get specific item
        /// </summary>
        /// <param name="idItem">Id Item</param>
        /// <param name="whsCode">Warehouse of item</param>
        /// <param name="priceList">Price list of item</param>
        /// <returns></returns>
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
        /// <summary>
        /// Get all items, specified pagination
        /// </summary>
        /// <param name="filter">filters of pagination</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResponse<List<Item>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
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
