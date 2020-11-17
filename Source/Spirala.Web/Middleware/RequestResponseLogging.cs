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
            List<string> listOfChangedValues = new List<string>();
            List<string> previousValues = new List<string>();
            List<string> newValues = new List<string>();
            IList<JToken> jSonObjectFromBody = null;
            var idInPost = "";

            context.Request.EnableBuffering();


            var body1 = await getBody(context);
            string address = context.Request.Path;
            string[] addressSplit = address.Split("/");
            string requestMethod = context.Request.Method;


            string nameOfController = "";
            if (addressSplit.Length > 2)
            {
                nameOfController = addressSplit[2];
            }


            var idInAddress = "";

            if (addressSplit.Length > 3)
            {
                idInAddress = addressSplit[3];
            }

            if (body1 != "")
            {
                jSonObjectFromBody = getJSonObjectFrom(body1);
            }

            if (idInAddress != "")
            {
                if (requestMethod == "PUT" && body1 != "")
                {
                    string dataBeforeChanged = await GetPreviousDataFromTable(nameOfController, db, idInAddress);
                    if (!dataBeforeChanged.IsNullOrEmpty())
                    {
                        IList<JToken> dataBeforeChangedAsJson = getJSonObjectFrom(dataBeforeChanged);
                        listOfChangedValues = getWhichValuesWereChanged(dataBeforeChangedAsJson, jSonObjectFromBody);
                        previousValues = getValuesFromJSONThatHavePathIn(dataBeforeChangedAsJson, listOfChangedValues);
                        newValues = getValuesFromJSONThatHavePathIn(jSonObjectFromBody, listOfChangedValues);
                    }
                }

                if (requestMethod == "DELETE" )
                {
                    string dataBeforeChanged = await GetPreviousDataFromTable(nameOfController, db, idInAddress);

                    if (!dataBeforeChanged.IsNullOrEmpty())
                    {
                        IList<JToken> dataBeforeChangedAsJson = getJSonObjectFrom(dataBeforeChanged);
                        listOfChangedValues = getWhichValuesWereChanged(dataBeforeChangedAsJson);
                        previousValues = getValuesFromJSONThatHavePathIn(dataBeforeChangedAsJson, listOfChangedValues);
                    }
                }

            }
            else
            {
                if (requestMethod == "POST" && body1 != "")
                {
                    listOfChangedValues = getWhichValuesWereChanged(jSonObjectFromBody);
                    newValues = getValuesFromJSONThatHavePathIn(jSonObjectFromBody, listOfChangedValues);
                    idInPost = GetIdFromBody(nameOfController,jSonObjectFromBody);

                }
            }


            await _next.Invoke(context);


            var nameOfEditor = context.User.Identity.Name;
            if (addressSplit[1] == "api")
            {
            }
            else if (addressSplit[1] == "odata" && listOfChangedValues.Count > 0)
            {
                if (context.Response.StatusCode == 204 && requestMethod == "PUT")
                {
                    var changedValuesAsString = String.Join(", ", listOfChangedValues);
                    var previousValuesAsString = String.Join(", ", previousValues);
                    var newValuesAsString = String.Join(", ", newValues);

                    RequestsResponsesLog toLog = new RequestsResponsesLog(new Guid(), nameOfEditor, nameOfController,
                        "PUT", new Guid(idInAddress), changedValuesAsString, previousValuesAsString,
                        newValuesAsString, DateTime.Now);
                    await db.RequestsResponsesLog.AddAsync(toLog);
                    await db.SaveChangesAsync();
                }
                if (context.Response.StatusCode == 200 && requestMethod == "DELETE")
                {
                    var changedValuesAsString = String.Join(", ", listOfChangedValues);
                    var previousValuesAsString = String.Join(", ", previousValues);

                    RequestsResponsesLog toLog = new RequestsResponsesLog(new Guid(), nameOfEditor, nameOfController,
                        "DELETE", new Guid(idInAddress), changedValuesAsString, previousValuesAsString,
                        " ", DateTime.Now);
                    await db.RequestsResponsesLog.AddAsync(toLog);
                    await db.SaveChangesAsync();
                }

                if (context.Response.StatusCode == 200 && requestMethod == "POST")
                {
                    var changedValuesAsString = String.Join(", ", listOfChangedValues);
                    var newValuesAsString = String.Join(", ", newValues);

                    RequestsResponsesLog toLog = new RequestsResponsesLog(new Guid(), nameOfEditor, nameOfController,
                        "POST", new Guid(idInPost), changedValuesAsString, " ", newValuesAsString, DateTime.Now);
                    await db.RequestsResponsesLog.AddAsync(toLog);
                    await db.SaveChangesAsync();
                }
            }
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

        private List<string> getWhichValuesWereChanged(IList<JToken> dataBeforeChangedAsJson)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < dataBeforeChangedAsJson.Count; i++)
            {
                output.Add(dataBeforeChangedAsJson[i].Path);
            }

            return output;
        }


        private IList<JToken> getJSonObjectFrom(string body1)
        {
            IList<JToken> listOfJSonObjectsFromBody;
            body1 = body1.Replace("\r\n", " ");
            if (body1 != "")
            {
                JObject input = JObject.Parse(body1);
                listOfJSonObjectsFromBody = input.Children().ToList();
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

        private async Task<string> GetPreviousDataFromTable(string nameOfController, ApplicationDbContext db,
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
        
        
         private string GetIdFromBody(string nameOfController,IList<JToken> jSonObjectFromBody)
        {
            switch (nameOfController)
            {
                case "Soldiers":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "SoldierId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
                case "Vehicles":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "VehicleId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
                case "MilitaryUnits":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "MilitaryUnitId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
                case "FamilyMembers":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "FamilyMemberId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
                case "RegistrationOfSoldiers":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "RegistrationOfSoldierId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
                case "FamilyRelationToSoldiers":
                {
                    for (int i = 0; i < jSonObjectFromBody.Count; i++)
                    {
                        if (jSonObjectFromBody[i].Path == "FamilyRelationToSoldierId")
                            return jSonObjectFromBody[i].First.ToString();
                    }
                    return null;
                }
            }

            return null;
        }
    }
}