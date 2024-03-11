using CarvedRock.Admin.Logic;
using CarvedRock.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Admin.Controllers;
public class ProductsController : Controller
{
    private readonly IProductLogic _logic;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductLogic productLogic, ILogger<ProductsController> logger)
    {
        _logic = productLogic;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _logic.GetAllProducts();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _logic.GetProductById(id);
        if (product == null)
        {
            _logger.LogWarning($"Details for id {id} not found");
            return View("NotFound");
        }
        return View(product);
    }

    public async Task<IActionResult> Create()
    {
        var newProduct = await _logic.InitializeProductModel();
        return View(newProduct);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            await _logic.AddNewProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            _logger.LogWarning($"No id passed for edit");
            return View("NotFound");
        }
        var model = await _logic.GetProductById(id.Value);
        await _logic.GetAvailableCategories(model);
        if (model == null)
        {
            _logger.LogWarning($"Product with id {id} not found for edit");
            return View("NotFound");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            await _logic.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            _logger.LogWarning($"No id passed for delete");
            return View("NotFound");
        }
        var model = await _logic.GetProductById(id.Value);
        if (model == null)
        {
            _logger.LogWarning($"Product with id {id} not found for delete");
            return View("NotFound");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }
}
