﻿using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greggs.Products.Api.DataAccess;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IDataAccess<Product> _dataAccess;

    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        return _dataAccess.List(pageStart, pageSize);
    }

    [HttpGet("eur")]
    public IEnumerable<Product> GetInEur(int pageStart = 0, int pageSize = 5)
    {
        var products = _dataAccess.List(pageStart, pageSize);
        var exchangeRate = 1.11m; // Assuming this is a constant value

        return products.Select(p => new Product
        {
            Name = p.Name,
            PriceInPounds = p.PriceInPounds,
            PriceInEuros = p.PriceInPounds * exchangeRate
        });
    }
}