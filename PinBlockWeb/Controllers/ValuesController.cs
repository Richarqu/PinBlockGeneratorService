using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PinBlockWeb.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ApiController
    {
       


        [HttpPost]
        [Route("PINBlockReq")]
        public async Task<PinBlockRes> PINBlock(PinBlockRequest pinBlockRequest)
        {
            PinBlockRes pinBlockRes = new PinBlockRes();
            try
            {
                Utils utl = new Utils();
                var pinBlock = utl.GetPINdata(pinBlockRequest.PAN, pinBlockRequest.PIN, pinBlockRequest.ZPK, pinBlockRequest.COMP1, pinBlockRequest.COMP2);
                pinBlockRes.ResponseCode = "00";
                pinBlockRes.ResponseDescription = "Success";
                pinBlockRes.PinBlock = pinBlock;

                return pinBlockRes;
            }
            catch (Exception ex)
            {
                pinBlockRes.ResponseCode = "04";
                pinBlockRes.ResponseDescription = "Error Ex." + " " + ex.Message;
                pinBlockRes.PinBlock = ex.StackTrace;
                //ex
                return pinBlockRes;
            }
        }

    }
}
