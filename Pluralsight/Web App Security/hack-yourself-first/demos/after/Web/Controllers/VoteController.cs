using System.Data.SqlClient;
using System.Linq;
﻿using System.Data;
﻿using System.Collections.Generic;
﻿using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
  public class VoteController : ApiController
  {
    private SupercarModelContext db = new SupercarModelContext();

    // POST api/Vote
    public HttpResponseMessage Post([FromBody]Vote vote)
    {
      if (!User.Identity.IsAuthenticated || (int)Membership.GetUser(User.Identity.Name).ProviderUserKey != vote.UserId)
      {
        return Request.CreateResponse(HttpStatusCode.Forbidden);
      }

      var userHasAlreadyVoted = db.Votes.Any(v => v.UserId == vote.UserId && v.SupercarId == vote.SupercarId);
      if (userHasAlreadyVoted)
      {
        return Request.CreateResponse(HttpStatusCode.Forbidden);
      }

      ValidateRequestHeader(Request);

      var connString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      const string sqlString = "INSERT INTO Vote(UserId, SupercarId, Comments) VALUES (@UserId, @SupercarId, @Comments)";

      using (var conn = new SqlConnection(connString))
      {
        var command = new SqlCommand(sqlString, conn);
        command.Parameters.Add("UserId", SqlDbType.Int).Value = vote.UserId;
        command.Parameters.Add("SupercarId", SqlDbType.Int).Value = vote.SupercarId;
        command.Parameters.Add("Comments", SqlDbType.NVarChar).Value = vote.Comments ?? string.Empty;
        command.Connection.Open();
        command.ExecuteNonQuery();
      }

      return Request.CreateResponse(HttpStatusCode.Created);
    }

    void ValidateRequestHeader(HttpRequestMessage request)
    {
      const string csrfCookieName = "__RequestVerificationToken";
      var cookieToken = string.Empty;
      var headerToken = string.Empty;

      var cookie = request.Headers.GetCookies(csrfCookieName).FirstOrDefault();
      if (cookie != null)
      {
        cookieToken = cookie[csrfCookieName].Value;
      }
      
      IEnumerable<string> tokenHeaders;
      if (request.Headers.TryGetValues("X-Csrf-Token", out tokenHeaders))
      {
        headerToken = tokenHeaders.First();
      }

      AntiForgery.Validate(cookieToken, headerToken);
    }
  }
}