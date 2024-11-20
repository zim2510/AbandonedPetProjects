using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NullpointerAPI.Data;
using NullpointerAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NullpointerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactFormController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ContactFormController(AppDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        [Route("/ContactFormData")]
        public async Task<IActionResult> GetContactFormData()
        {
            var contactFormHost = _config.GetValue<string>("ContactFormHost");
            var httpClient = new HttpClient();
            var responseString = await httpClient.GetStringAsync(contactFormHost);
            var contactFormData = JsonConvert.DeserializeObject<Dictionary<string, ContactFormDataViewModel>>(responseString).ToList();
            return Ok(contactFormData);
        }

        [HttpPost]
        [Route("/ContactFormData")]
        public async Task<IActionResult> PostContactFormData([FromBody] ContactFormDataViewModel data)
        {
            var contactFormHost = _config.GetValue<string>("ContactFormHost");
            var serializedData = JsonConvert.SerializeObject(data);
            var content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(contactFormHost, content);
            return Ok(response.StatusCode);
        }
    }
}
