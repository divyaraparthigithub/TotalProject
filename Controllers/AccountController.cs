using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Task20_consumewebapioftask11_.Models;

namespace Task20_consumewebapioftask11_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly ProductDbContext _productdb;

        public IEnumerable<string> Countries { get; private set; }
        public Dictionary<string, IEnumerable<string>> States { get; private set; }

        public AccountController(ProductDbContext productDb)
        {
            _productdb = productDb;
        }
        [HttpGet("CountriesList")]
        public IActionResult CountriesList()
        {
            try
            {
                var countries = _productdb.Country.ToList();
                return Ok(countries);   
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("GetStatesOnCountry")]
        public IActionResult GetStatesOnCountry(int countryid)
        {
            try
            {
                var states = _productdb.State
                    .Where(s => s.Countryid == countryid)
                    .Select(s => new { s.id, s.StateName })
                    .ToList();

                return Ok(states);
            }
            catch (Exception ex)
            {

                return NotFound();
            }

        }
        //[HttpGet("Register")]
        //public ActionResult Register(Register register)
        //{
        //    var model = new Register
        //    {
        //        Countries = GetCountries(),
        //        States = new Dictionary<string, IEnumerable<string>>()
        //    };

        //    return Ok(model);
        //}
        [HttpGet("Register")]
        public ActionResult<Registration> Register()
        {
            var model = new Registration
            {
                Country = (IEnumerable<SelectListItem>)CountriesList(),
                State = new Dictionary<string, IEnumerable<string>>()
            };

            return Ok(model);
        }
        
        

        [HttpPost("Register")]
        public async Task<ActionResult> Register(Registration user)
        {
            var users = new Register
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Password = user.Password,
                SelectedCountry = user.SelectedCountry,
                SelectedState = user.SelectedState
            };
               _productdb.Register.Add(users);
                await _productdb.SaveChangesAsync();                
                return Ok(users);
        }
        [HttpGet("GetCountries")]
        public IEnumerable<string> GetCountries()
        {
            return new List<string> { "India", "Australia" };
        }
        [HttpGet("GetStates")]
        public ActionResult<List<string>> GetStates(string selectedCountry)
        {
            var states = GetStatesForCountry(selectedCountry);

            return Ok(states);
        }
        //[HttpGet("GetStates")]
        //public List<string> GetStates(string selectedCountry)
        //{
        //    var states = GetStatesForCountry(selectedCountry);
        //    return states;
           
        //}
        [HttpGet("GetStatesForCountry")]
        private List<string> GetStatesForCountry(string selectedCountry)
        {
            switch (selectedCountry)

            {
                case "India":
                    return new List<string> { "Andhra Pradesh", "Telangana", "Karnataka" };
                case "Australia":
                    return new List<string> { "NewSothWhales", "Sydney", "Melbourne" };
                default:
                    return new List<string>();
            }
        }
        //[HttpPost("Logins")]
        //public IActionResult Logins(Register users)
        //{
        //    return Ok();
        //}
        [HttpPost("Logins")]
        public IActionResult Logins(Login user)
        {
            if (user != null)
            {
                var users = _productdb.Register.FirstOrDefault(u => u.Email == user.Email&&u.Password==user.Password);
                return Ok(user);
            }
            return Unauthorized();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(Login users)
        {
            if (users != null)
            {
                var user = await _productdb.Register.FirstOrDefaultAsync(u => u.Email == users.Email);

                if (user != null)
                {
                    string decryptedStoredPassword = DecryptPassword(user.Password);
                    bool passwordsMatch = decryptedStoredPassword == users.Password;

                   // return passwordsMatch;
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            else
            {
                return BadRequest("Invalid input");
            }     
            return Unauthorized();
        }
        private string DecryptPassword(string encryptedPassword)
        {
            
            byte[] decryptedBytes = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
       
        [HttpPut("UpdateEmailConfirmedStatus")]
        public IActionResult UpdateEmailConfirmedStatus(string Email, bool EmailConfirmed)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = _productdb.Register.FirstOrDefault(u => u.Email == Email);

                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }
                user.EmailConfirmed = EmailConfirmed;
                _productdb.SaveChanges();
                return Ok(new { Message = "EmailConfirmed status updated successfully" });
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> LoginAsync(Register register)
        //{            
        //    if (register != null)
        //    {
        //        //Product product = await _productdb.Product.FirstOrDefaultAsync(p => p.ProductName == customerDto.ProductName);
        //        var user =  await _productdb.Register.FirstOrDefaultAsync(u => u.Email == register.Email && u.Password == register.Password);
        //        return Ok(user);

        //    }
        //    else
        //    {
        //        return BadRequest("doesnt exists");
        //    }

        //    return Unauthorized();
        //}


        //[HttpGet("Registers")]
        //public async Task<IActionResult> Registers(Register register)
        //{
        //    //var registers = new Register
        //    //{
        //    //    Name = customerDto.Name,
        //    //    Address = customerDto.Address,
        //    //    Phone = customerDto.Phone,
        //    //    Email = customerDto.Email,
        //    //    ProductId = product.Id,
        //    //    GenderId = gender.Id
        //    //};

        //    Countries = GetCountries();
        //    States = new Dictionary<string, IEnumerable<string>>();


        //    _productdb.Register.Add(register);
        //    await _productdb.SaveChangesAsync();
        //    return Ok(register);
        //}
       
        }
    }

