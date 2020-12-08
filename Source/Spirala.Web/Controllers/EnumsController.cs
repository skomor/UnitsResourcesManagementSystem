

using System;
using System.Collections.Generic;
using System.Linq;
using Aut3.Data;
using Aut3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Aut3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]
    public class EnumsController
    {
        private readonly ApplicationDbContext _context;
        public EnumsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [ODataRoute("Enums/FamilyRelationsEnum")]
        public  IEnumerable<FamilyRelationsEnum> GetFamilyMember()
        {
             
           var enums =  Enum.GetValues(typeof(FamilyRelationsEnum));
           var enums2 = Enum.GetValues(typeof(FamilyRelationsEnum)).Cast<FamilyRelationsEnum>();
            return enums2 ;
        }

    }
}