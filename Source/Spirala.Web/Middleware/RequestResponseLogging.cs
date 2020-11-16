using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Aut3.Data;
using Aut3.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Aut3.Middleware
{
    public class RequestResponseLogging
    {
        private readonly RequestDelegate _next;


        public RequestResponseLogging(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, ApplicationDbContext db)
        {
            context.Request.EnableBuffering();

            IList<JToken> jSonObjectFromBody = null;
            var body1 = await getBody(context);
            if (body1 != "")
            {
                jSonObjectFromBody = getJSonObjectFromBody(body1);
            }

            string address = context.Request.Path;
            string[] addressSplit = address.Split("/");
            string nameOfController;
            string idOfPutItem;
            List<string> listOfChangedValues = new List<string>();
            List<string> previousValues = new List<string>();
            List<string> newValues = new List<string>();
            if (addressSplit.Length > 3)
            {
                nameOfController = addressSplit[2];
                idOfPutItem = addressSplit[3];
                string dataBeforeChanged;

                if (context.Request.Method == "PUT" && body1 != "")
                {
                    IList<JToken> dataBeforeChangedAsJson;

                    dataBeforeChanged = await getPreviousDataFromTable(nameOfController, db, idOfPutItem);


                    if (!dataBeforeChanged.IsNullOrEmpty())
                    {
                        dataBeforeChangedAsJson = getJSonObjectFromBody(dataBeforeChanged);
                        listOfChangedValues = getWhichValuesWereChanged(dataBeforeChangedAsJson, jSonObjectFromBody);
                        previousValues = getValuesFromJSONThatHavePathIn(dataBeforeChangedAsJson, listOfChangedValues);
                        newValues = getValuesFromJSONThatHavePathIn(jSonObjectFromBody, listOfChangedValues);
                    }
                }
            }

            
            await _next.Invoke(context);

            
            var nameOfEditor = context.User.Identity.Name;
            if (addressSplit[1] == "api")
            {
            }
            else if (addressSplit[1] == "odata" && listOfChangedValues.Count > 0)
            {
                if (context.Response.StatusCode == 204 && context.Request.Method == "PUT")
                {
                    var changedValuesAsString = String.Join(", ", listOfChangedValues);
                    var previousValuesAsString = String.Join(", ", previousValues);
                    var newValuesAsString = String.Join(", ", newValues);

                    RequestsResponsesLog toLog = new RequestsResponsesLog(new Guid(), nameOfEditor, addressSplit[2],
                        "PUT", new Guid(addressSplit[3]), changedValuesAsString,previousValuesAsString,newValuesAsString,DateTime.Now);
                   await db.RequestsResponsesLog.AddAsync(toLog);
                     await db.SaveChangesAsync();
                }

                if (context.Response.StatusCode == 204 && context.Request.Method == "DELETE")
                {
                    
                }
            }

            /*var ok1 = context.User.Claims.GetType();
            var adress1 = context.Request.Path;

            var userIdentity1 = context.User.Identity;*/
        }

        private List<string> getValuesFromJSONThatHavePathIn(IList<JToken> jsonData, List<string> listOfPaths)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < jsonData.Count; i++)
            {
                for (int j = 0; j < listOfPaths.Count; j++)
                {
                    if (jsonData[i].Path == listOfPaths[j])
                    {
                        output.Add(jsonData[i].First.ToString());
                    }
                }
            }

            return output;
        }

        private List<string> getWhichValuesWereChanged(IList<JToken> dataBeforeChangedAsJson,
            IList<JToken> jSonObjectFromBody)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < dataBeforeChangedAsJson.Count; i++)
            {
                for (int j = 0; j < jSonObjectFromBody.Count; j++)
                {
                    if (dataBeforeChangedAsJson[i].Path == jSonObjectFromBody[j].Path)
                    {
                        if (dataBeforeChangedAsJson[i].First.ToString() != jSonObjectFromBody[j].First.ToString())
                        {
                            output.Add(dataBeforeChangedAsJson[i].Path);
                        }
                    }
                }
            }

            return output;
        }

        private IList<JToken> getJSonObjectFromBody(string body1)
        {
            IList<JToken> listOfJSonObjectsFromBody;
            body1 = body1.Replace("\r\n", " ");
            if (body1 != "")
            {
                JObject input = JObject.Parse(body1);
                listOfJSonObjectsFromBody = input.Children().ToList(); //this is closest to json I could get 
                return listOfJSonObjectsFromBody;
            }

            throw new Exception("body cant be null");
        }

        private async Task<string> getBody(HttpContext context)
        {
            string body1;
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                body1 = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            return body1;
        }

        private async Task<string> getPreviousDataFromTable(string nameOfController, ApplicationDbContext db,
            string idOfPutItem)
        {
            switch (nameOfController)
            {
                case "Soldiers":
                {
                    var res = db.Soldier.AsNoTracking().Where(a => a.SoldierId == new Guid(idOfPutItem))
                        .FirstOrDefault();
                    return ToStringAsJson(res);
                }
                case "Vehicles":
                {
                    var result = db.Vehicle.AsNoTracking().Where(a => a.VehicleId == new Guid(idOfPutItem))
                        .FirstOrDefault();
                    return ToStringAsJson(result);
                }
                case "MilitaryUnits":
                {
                    var result = db.MilitaryUnit.AsNoTracking().Where(a => a.MilitaryUnitId == new Guid(idOfPutItem))
                        .FirstOrDefault();
                    return ToStringAsJson(result);
                }
                case "FamilyMembers":
                {
                    var result = db.FamilyMember.AsNoTracking().Where(a => a.FamilyMemberId == new Guid(idOfPutItem))
                        .FirstOrDefault();
                    return ToStringAsJson(result);
                }
                case "RegistrationOfSoldiers":
                {
                    var result = db.RegistrationOfSoldier.AsNoTracking()
                        .Where(a => a.RegistrationOfSoldierId == new Guid(idOfPutItem))
                        .FirstOrDefault();
                    return ToStringAsJson(result);
                }
                case "FamilyRelationToSoldiers":
                {
                    var result =
                        db.FamilyRelationToSoldier.AsNoTracking()
                            .Where(a => a.FamilyRelationToSoldierId == new Guid(idOfPutItem))
                            .FirstOrDefault();
                    return ToStringAsJson(result);
                }
            }

            return null;
        }

        public string ToStringAsJson<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}