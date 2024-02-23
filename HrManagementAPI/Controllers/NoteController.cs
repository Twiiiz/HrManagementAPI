using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HrManagementAPI.Controllers
{
    [Route("api/submissions/{sub-id}/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public NoteController()
        {
            
        }

        //[HttpGet]
        //[Route("hr/{hr-id}")]
        //public async Task<IActionResult> GetSubmissionNotes([FromRoute(Name = "sub-id")] int subId, [FromRoute(Name = "hr-id")] int hrId)
        //{

        //}

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetSubmissionNote([FromRoute(Name = "id")] int noteId)
        //{

        //}

        //[HttpPost]
        //[Route("")]
        //public async Task<IActionResult> CreateSubmissionNote([FromBody])
        //{

        //}

        //[HttpPut]
        //[Route("{id}")]
        //public async Task<IActionResult> ReplaceSubmissionNote([FromRoute(Name = "id")] int noteId, [FromQuery][Required] int hrId)
        //{

        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<IActionResult> DeleteSubmissionNote([FromRoute(Name = "id")] int noteId, [FromQuery][Required] int hrId)
        //{

        //}
    }
}
