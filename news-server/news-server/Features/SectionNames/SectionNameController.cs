﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news_server.Features.SectionNames.Models;
using System.Threading.Tasks;

namespace news_server.Features.SectionNames
{
    [Authorize(Roles = "admin, moderator")]
    public class SectionNameController: ApiController
    {
        private readonly ISectionService sectionService;

        public SectionNameController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }

        
        [HttpPost(nameof(AddSection))]
        public async Task<ActionResult> AddSection(AddSectionNameModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await sectionService.AddSection(model);

                if (result)
                {
                    return Ok();
                }
                else
                    ModelState.AddModelError("error", "Секция с таким именем уже существует");
            }           
            return BadRequest(ModelState);
        }

        [HttpPut(nameof(UpdateSection))]
        public async Task<ActionResult> UpdateSection(UpdateSectionModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await sectionService.UpdateSection(model);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpGet(nameof(GetNewsBySectionName))]
        public async Task<ActionResult> GetNewsBySectionName(string sectionName)
        {
            if (!string.IsNullOrEmpty(sectionName))
            {
                var result = await sectionService.GetNewsBySectionName(sectionName);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
