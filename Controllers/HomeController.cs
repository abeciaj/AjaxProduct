using System.Diagnostics;
using BitxifyProduct.Data;
using Microsoft.AspNetCore.Mvc;
using BitxifyProduct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace BitxifyProduct.Controllers;

public class HomeController : Controller
{
    private readonly ProductDbContext _context;
    private readonly ILogger<HomeController> _logger;
    public HomeController(ProductDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
    // READ
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.tblProduct.ToListAsync();
        return Json(products);
    }
    
    // CREATE
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductModel product)
    {
        if (product == null) return BadRequest();

        _context.tblProduct.Add(product);
        await _context.SaveChangesAsync();
        return Json(product);
    }

    // UPDATE
    [HttpPost]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductModel product)
    {
        var existing = await _context.tblProduct.FindAsync(product.Id);
        if (existing == null) return NotFound();

        existing.Name = product.Name;
        existing.Price = product.Price;

        await _context.SaveChangesAsync();
        return Json(existing);
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> DeleteProduct([FromBody] int id)
    {
        var product = await _context.tblProduct.FindAsync(id);
        if (product == null) return NotFound();

        _context.tblProduct.Remove(product);
        await _context.SaveChangesAsync();
        return Json(new { success = true });
    }
}