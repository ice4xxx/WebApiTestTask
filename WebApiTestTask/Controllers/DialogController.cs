using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTestTask.Models;

namespace WebApiTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogController : ControllerBase
    {
        /// <summary>
        /// Returns GUIDs of the dialogs by client GUIDs
        /// </summary>
        /// <param name="clientGuids">Client GUIDs to find dialogs that contain all of these users</param>
        /// <response code="200">Returns GUIDs of the dialogs.</response>
        /// <response code="400">If clientGuids was missing or invalid.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("getByClientGuid")]
        public ActionResult<List<Guid>> GetByClientGuid([FromQuery] List<Guid> clientGuids)
        {
            if (clientGuids.Count == 0)
            {
                return BadRequest("One of the parameters specified was missing or invalid");
            }

            List<RGDialogsClients> dialogsClientsList = RGDialogsClients.Init();

            var resultDialog = dialogsClientsList.GroupBy(dialog => dialog.IDRGDialog).Where(dialogGroup =>
            {
                Dictionary<Guid, bool> clientGuidHashTable = new Dictionary<Guid, bool>();

                foreach (var dialogClient in dialogGroup)
                {
                    clientGuidHashTable.TryAdd(dialogClient.IDClient, true);
                }

                foreach (var clientGuid in clientGuids)
                {
                    if (!clientGuidHashTable.TryGetValue(clientGuid, out bool value))
                    {
                        return false;
                    }
                }

                return true;
            }).Select(groupDialog => groupDialog.First().IDRGDialog);


            return Ok(resultDialog);
        }
    }
}
