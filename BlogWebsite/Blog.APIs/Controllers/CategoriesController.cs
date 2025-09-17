using Blog.Core.DTos;
using Blog.Core.Interfaces;
using Blog.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.APIs.Controllers
{
    [Route("api/[controller]")] //http://localhost:5016/api/controller
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]///api/controller
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Categories = await _categoryService.GetAllAsync();
                if (Categories == null || !Categories.Any())
                {
                    return NotFound(new 
                    {
                        StatusCode =StatusCodes.Status404NotFound,  
                        Message="no Categories Found",
                        Data=new List<Category>()
                    });
                }
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = " Categories retrived successfuly",
                    Data = Categories
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while retriving data",
                    Data = ex.Message
                });
            }
        }

        [HttpGet("{id}")]///api/controller/id
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Categories = await _categoryService.GetByIdAsync(id);
                if (Categories == null )
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $" Category with id {id} not Found",
                    });
                }
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = " Categories retrived successfuly",
                    Data = Categories
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while retriving data",
                    Data = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDTo categoryDTo)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return BadRequest(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Invalid Category Data",
                        Errors=ModelState.Values.SelectMany(v=>v.Errors).Select(e=>e.ErrorMessage)
                    });
                }
                var Category = await _categoryService.CreateAsync(categoryDTo);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    message = "Category created successfuly",
                    Data = Category,
                    StatusCode = StatusCodes.Status201Created,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while Creating data",
                    Error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public  async Task<IActionResult> Edit(int id,CategoryDTo categoryDTo) {
            try
            {
                var OldCategory= await _categoryService.GetByIdAsync(id);
                if(OldCategory == null)
                {
                    return BadRequest( new 
                    {
                        message=$"Category with id {id} Not found",
                        StatusCode = StatusCodes.Status400BadRequest,
                    });
                }
                if (await _categoryService.UpdateAsync(id,categoryDTo))
                {
                    return Ok(new
                    {
                        message = "Category Updated Successfully",
                        StatusCode = StatusCodes.Status200OK,
                        Data = await _categoryService.GetByIdAsync(id)
                    });
                }
                else { 
                    return NotFound(new
                    {
                        message = $"Category with id {id} Not found",
                           StatusCode= StatusCodes.Status404NotFound,
                    });
                }

            }
            catch (Exception ex) 
            {
                return BadRequest( new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while Updateing data",
                    Error = ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> Edit(CategoryDTo categoryDTo) 
        {

            try
            {
                var OldCategory = await _categoryService.GetByIdAsync(categoryDTo.Id);
                if (OldCategory == null)
                {
                    return BadRequest(new
                    {
                        message = $"Category with id {categoryDTo.Id} Not found",
                        StatusCode = StatusCodes.Status400BadRequest,
                    });
                }
                if (await _categoryService.UpdateAsync(categoryDTo))
                {
                    return Ok(new
                    {
                        message = "Category Updated Successfully",
                        StatusCode = StatusCodes.Status200OK,
                        Data = await _categoryService.GetByIdAsync(categoryDTo.Id)
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        message = $"Category with id {categoryDTo.Id} Not found",
                        StatusCode = StatusCodes.Status404NotFound,
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while Updateing data",
                    Error = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Category = await _categoryService.GetByIdAsync(id);
                if (Category == null)
                {
                    return NotFound(new
                    {
                        message = $"Category with id {id} Not found",
                        StatusCode = StatusCodes.Status404NotFound,
                    });                    
                }
                if (await _categoryService.DeleteAsync(id))
                {
                    return Ok(new
                    {
                        message = "Category Deleted Successfully",
                        StatusCode = StatusCodes.Status200OK,
                        OldData = Category
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "Category Not Deleted ",
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = Category

                    });
                }

            }
            catch (Exception ex) 
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occured while Deleting data",
                    Error = ex.Message
                });
            }
        }
    }
}
