using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using louiseBank.Models;
using umbraco.NodeFactory;
using umbraco.interfaces;
using Eksponent.CropUp;

namespace louiseBank.Controllers
{
    public class CasesController : SurfaceController
    {

        public ActionResult Index(string category, string unit)
        {
            var allCases = new List<Case>();
            var casesContainerNode = new Node(1073);
            var listCases = casesContainerNode.ChildrenAsList.Where(x => x.ChildrenAsList.Any());

            foreach (var listCase in listCases)
            {
                var children = listCase.ChildrenAsList;

                foreach (var child in children)
                {
                    allCases.Add(GetCaseFromUmbracoNode(child));
                }
                
            }

            return PartialView("isotopeViewCases", new CasesResultView
                {
                    Cases = allCases
                });
        }


        private Case GetCaseFromUmbracoNode(INode customerCase)
        {
            //laver cropUp imageUrl
            var imageId = customerCase.GetProperty("images").Value.Split(',').Where(x => string.IsNullOrEmpty(x) == false).Select(x => int.Parse(x)).FirstOrDefault();
            var imageUrl = imageId > 0 ? Umbraco.Media(imageId).Url : "";

            if (string.IsNullOrEmpty(imageUrl) == false)
            {
                var cropUpSizeDesktop = new ImageSizeArguments { Width = 300, Height = 225, CropMode = CropUpMode.BestFit, Zoom = true };
                imageUrl = CropUp.GetUrl("~" + imageUrl, cropUpSizeDesktop);
            }

            //henter kategori
            var catNode = new Node(Convert.ToInt32(customerCase.GetProperty("kategorier").Value));


            //laver return objekt
            var p = new Case
                {
                    Id = 0,
                    Headline = customerCase.Name,
                    Customer = customerCase.Parent.Name,
                    ImageUrl = imageUrl,
                    Url = customerCase.NiceUrl,
                    Created = customerCase.CreateDate,
                    Modified = customerCase.UpdateDate,
                    CategoryId = catNode.Id,
                    CategoryName = catNode.Name,
                    SortOrder = customerCase.Parent.SortOrder
                };

            return p;
        }
    }
}